// ============================================
// MOTOPARTSSHOP - CUSTOM JAVASCRIPT
// ============================================

document.addEventListener('DOMContentLoaded', function() {
    
    // ----- SCROLL TO TOP BUTTON -----
    const scrollTopBtn = document.createElement('button');
    scrollTopBtn.id = 'scrollTop';
    scrollTopBtn.innerHTML = '↑';
    scrollTopBtn.setAttribute('aria-label', 'Scroll to top');
    document.body.appendChild(scrollTopBtn);

    window.addEventListener('scroll', function() {
        if (window.pageYOffset > 300) {
            scrollTopBtn.classList.add('show');
        } else {
            scrollTopBtn.classList.remove('show');
        }
    });

    scrollTopBtn.addEventListener('click', function() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    // ----- MOSTRAR/ESCONDER PASSWORD (MELHORADO) -----
    const passwordInputs = document.querySelectorAll('input[type="password"]');
    
    passwordInputs.forEach(input => {
        // Verificar se já tem botão (evitar duplicação)
        if (input.parentElement.classList.contains('password-wrapper')) {
            return;
        }

        // Criar wrapper
        const wrapper = document.createElement('div');
        wrapper.className = 'password-wrapper';
        wrapper.style.cssText = 'position: relative; display: block; width: 100%;';
        
        // Criar botão do olho
        const eyeButton = document.createElement('button');
        eyeButton.type = 'button';
        eyeButton.className = 'password-toggle';
        eyeButton.innerHTML = '👁️';
        eyeButton.setAttribute('aria-label', 'Mostrar/Esconder password');
        eyeButton.style.cssText = `
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            border: none;
            background: transparent;
            cursor: pointer;
            font-size: 1.2rem;
            padding: 5px 8px;
            z-index: 10;
            line-height: 1;
        `;
        
        // Ajustar padding do input
        input.style.paddingRight = '45px';
        
        // Inserir wrapper
        input.parentNode.insertBefore(wrapper, input);
        wrapper.appendChild(input);
        wrapper.appendChild(eyeButton);
        
        // Toggle visibility
        eyeButton.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            if (input.type === 'password') {
                input.type = 'text';
                eyeButton.innerHTML = '🙈';
            } else {
                input.type = 'password';
                eyeButton.innerHTML = '👁️';
            }
        });
    });

    // ----- ANIMAÇÃO AO SCROLL -----
    const animateOnScroll = function() {
        const elements = document.querySelectorAll('.card, .table, .alert');
        
        elements.forEach(element => {
            const position = element.getBoundingClientRect().top;
            const screenPosition = window.innerHeight / 1.3;
            
            if(position < screenPosition) {
                element.style.opacity = '1';
                element.style.transform = 'translateY(0)';
            }
        });
    };

    // Inicializar elementos para animação
    const elements = document.querySelectorAll('.card, .table, .alert');
    elements.forEach(element => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(30px)';
        element.style.transition = 'all 0.6s ease';
    });

    window.addEventListener('scroll', animateOnScroll);
    animateOnScroll();

    // ----- CONFIRMAÇÃO DE DELETE -----
    const deleteButtons = document.querySelectorAll('a[href*="Delete"]');
    deleteButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            if(!confirm('❌ Tens a certeza que queres eliminar este item?')) {
                e.preventDefault();
            }
        });
    });

    // ----- TOOLTIPS BOOTSTRAP -----
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }

    // ----- LOADING OVERLAY -----
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function(e) {
            // Evitar loading em forms inline (carrinho)
            if (this.classList.contains('d-inline') || this.classList.contains('form-inline')) {
                return;
            }
            
            const loadingOverlay = document.createElement('div');
            loadingOverlay.style.cssText = `
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background: rgba(0,0,0,0.7);
                display: flex;
                justify-content: center;
                align-items: center;
                z-index: 9999;
            `;
            loadingOverlay.innerHTML = '<div class="spinner"></div>';
            document.body.appendChild(loadingOverlay);
        });
    });

    // ----- CONTADOR ANIMADO PARA O DASHBOARD -----
    const counters = document.querySelectorAll('.display-4');
    counters.forEach(counter => {
        const text = counter.innerText;
        const target = parseFloat(text.replace(/[€,]/g, ''));
        
        if (!isNaN(target) && target > 0) {
            counter.innerText = '0';
            const increment = target / 100;
            
            const timer = setInterval(() => {
                const current = parseFloat(counter.innerText.replace(/[€,]/g, ''));
                if (current < target) {
                    const newValue = Math.ceil(current + increment);
                    counter.innerText = newValue;
                } else {
                    counter.innerText = target;
                    clearInterval(timer);
                }
            }, 20);
        }
    });

    // ----- SEARCH HIGHLIGHT -----
    const searchInput = document.querySelector('input[name="SearchString"]');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase();
            const tableRows = document.querySelectorAll('.table tbody tr');
            
            tableRows.forEach(row => {
                const text = row.innerText.toLowerCase();
                if (text.includes(searchTerm)) {
                    row.style.display = '';
                    row.style.backgroundColor = searchTerm ? 'rgba(231,76,60,0.05)' : '';
                } else {
                    row.style.display = searchTerm ? 'none' : '';
                }
            });
        });
    }

    // ----- PARTÍCULAS DE FUNDO (OPCIONAL) -----
    createParticles();
});

// Função para criar partículas decorativas
function createParticles() {
    const particlesContainer = document.createElement('div');
    particlesContainer.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        pointer-events: none;
        z-index: -1;
        overflow: hidden;
    `;
    
    for (let i = 0; i < 20; i++) {
        const particle = document.createElement('div');
        particle.style.cssText = `
            position: absolute;
            width: ${Math.random() * 10 + 5}px;
            height: ${Math.random() * 10 + 5}px;
            background: rgba(231,76,60,${Math.random() * 0.3});
            border-radius: 50%;
            top: ${Math.random() * 100}%;
            left: ${Math.random() * 100}%;
            animation: float ${Math.random() * 10 + 10}s infinite ease-in-out;
        `;
        particlesContainer.appendChild(particle);
    }
    
    document.body.appendChild(particlesContainer);
    
    // Adicionar animação de float
    const style = document.createElement('style');
    style.textContent = `
        @keyframes float {
            0%, 100% { transform: translateY(0) translateX(0); }
            25% { transform: translateY(-20px) translateX(10px); }
            50% { transform: translateY(-40px) translateX(-10px); }
            75% { transform: translateY(-20px) translateX(10px); }
        }
    `;
    document.head.appendChild(style);
}

// Console Easter Egg
console.log('%c🏍️ MotoPartsShop', 'color: #e74c3c; font-size: 30px; font-weight: bold;');
console.log('%cPowered by ASP.NET Core', 'color: #2c3e50; font-size: 16px;');