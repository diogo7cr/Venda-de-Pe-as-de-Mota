using MotoPartsShop.Models;

namespace MotoPartsShop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Verificar se já tem dados
            if (context.Categorias.Any())
            {
                return;
            }

            // ========== CATEGORIAS ==========
            var categorias = new[]
            {
                new Categoria { Nome = "Motor", Descricao = "Componentes do motor" },
                new Categoria { Nome = "Transmissão", Descricao = "Caixa de velocidades e embraiagem" },
                new Categoria { Nome = "Travões", Descricao = "Discos, pastilhas e sistemas" },
                new Categoria { Nome = "Suspensão", Descricao = "Amortecedores e forquilhas" },
                new Categoria { Nome = "Escape", Descricao = "Sistemas de escape" },
                new Categoria { Nome = "Elétrica", Descricao = "Bateria e sistema elétrico" },
                new Categoria { Nome = "Carenagem", Descricao = "Painéis e proteções" },
                new Categoria { Nome = "Rodas e Pneus", Descricao = "Jantes, pneus e aros" },
                new Categoria { Nome = "Iluminação", Descricao = "Faróis e luzes" },
                new Categoria { Nome = "Filtros", Descricao = "Filtros de ar, óleo e combustível" },
                new Categoria { Nome = "Refrigeração", Descricao = "Radiadores e líquidos" },
                new Categoria { Nome = "Combustível", Descricao = "Tanques e bombas" },
                new Categoria { Nome = "Direção", Descricao = "Guiador e manípulos" },
                new Categoria { Nome = "Acessórios", Descricao = "Espelhos, suportes, etc." }
            };
            context.Categorias.AddRange(categorias);
            context.SaveChanges();

            // ========== MARCAS ==========
            var marcas = new[]
            {
                new Marca { Nome = "Honda", PaisOrigem = "Japão" },
                new Marca { Nome = "Yamaha", PaisOrigem = "Japão" },
                new Marca { Nome = "Kawasaki", PaisOrigem = "Japão" },
                new Marca { Nome = "Suzuki", PaisOrigem = "Japão" },
                new Marca { Nome = "Ducati", PaisOrigem = "Itália" },
                new Marca { Nome = "BMW", PaisOrigem = "Alemanha" },
                new Marca { Nome = "KTM", PaisOrigem = "Áustria" },
                new Marca { Nome = "Harley-Davidson", PaisOrigem = "EUA" },
                new Marca { Nome = "Aprilia", PaisOrigem = "Itália" },
                new Marca { Nome = "Triumph", PaisOrigem = "Reino Unido" },
                new Marca { Nome = "MV Agusta", PaisOrigem = "Itália" },
                new Marca { Nome = "Indian", PaisOrigem = "EUA" },
                new Marca { Nome = "Royal Enfield", PaisOrigem = "Índia" },
                new Marca { Nome = "Husqvarna", PaisOrigem = "Suécia" },
                new Marca { Nome = "Beta", PaisOrigem = "Itália" }
            };
            context.Marcas.AddRange(marcas);
            context.SaveChanges();

            // ========== MODELOS (150+ modelos) ==========
            var modelos = new List<Modelo>
            {
                // Honda
                new Modelo { Nome = "CBR600RR", AnoLancamento = 2021, Cilindrada = 600, MarcaId = 1 },
                new Modelo { Nome = "CBR1000RR-R", AnoLancamento = 2020, Cilindrada = 1000, MarcaId = 1 },
                new Modelo { Nome = "CB500F", AnoLancamento = 2022, Cilindrada = 500, MarcaId = 1 },
                new Modelo { Nome = "CRF450R", AnoLancamento = 2023, Cilindrada = 450, MarcaId = 1 },
                new Modelo { Nome = "Africa Twin", AnoLancamento = 2022, Cilindrada = 1100, MarcaId = 1 },
                new Modelo { Nome = "CB650R", AnoLancamento = 2021, Cilindrada = 650, MarcaId = 1 },
                new Modelo { Nome = "NC750X", AnoLancamento = 2023, Cilindrada = 750, MarcaId = 1 },
                new Modelo { Nome = "CB1000R", AnoLancamento = 2022, Cilindrada = 1000, MarcaId = 1 },
                new Modelo { Nome = "CBR650R", AnoLancamento = 2021, Cilindrada = 650, MarcaId = 1 },
                new Modelo { Nome = "CB125R", AnoLancamento = 2023, Cilindrada = 125, MarcaId = 1 },

                // Yamaha
                new Modelo { Nome = "YZF-R1", AnoLancamento = 2022, Cilindrada = 1000, MarcaId = 2 },
                new Modelo { Nome = "YZF-R6", AnoLancamento = 2020, Cilindrada = 600, MarcaId = 2 },
                new Modelo { Nome = "MT-07", AnoLancamento = 2023, Cilindrada = 700, MarcaId = 2 },
                new Modelo { Nome = "MT-09", AnoLancamento = 2022, Cilindrada = 900, MarcaId = 2 },
                new Modelo { Nome = "MT-10", AnoLancamento = 2021, Cilindrada = 1000, MarcaId = 2 },
                new Modelo { Nome = "Tracer 900", AnoLancamento = 2023, Cilindrada = 900, MarcaId = 2 },
                new Modelo { Nome = "Ténéré 700", AnoLancamento = 2022, Cilindrada = 700, MarcaId = 2 },
                new Modelo { Nome = "YZF-R3", AnoLancamento = 2023, Cilindrada = 300, MarcaId = 2 },
                new Modelo { Nome = "XSR900", AnoLancamento = 2022, Cilindrada = 900, MarcaId = 2 },
                new Modelo { Nome = "FZ6", AnoLancamento = 2020, Cilindrada = 600, MarcaId = 2 },

                // Kawasaki
                new Modelo { Nome = "Ninja ZX-10R", AnoLancamento = 2023, Cilindrada = 1000, MarcaId = 3 },
                new Modelo { Nome = "Ninja 650", AnoLancamento = 2022, Cilindrada = 650, MarcaId = 3 },
                new Modelo { Nome = "Z900", AnoLancamento = 2023, Cilindrada = 900, MarcaId = 3 },
                new Modelo { Nome = "Z650", AnoLancamento = 2022, Cilindrada = 650, MarcaId = 3 },
                new Modelo { Nome = "Versys 650", AnoLancamento = 2023, Cilindrada = 650, MarcaId = 3 },
                new Modelo { Nome = "Ninja 400", AnoLancamento = 2022, Cilindrada = 400, MarcaId = 3 },
                new Modelo { Nome = "Z1000", AnoLancamento = 2020, Cilindrada = 1000, MarcaId = 3 },
                new Modelo { Nome = "Vulcan S", AnoLancamento = 2023, Cilindrada = 650, MarcaId = 3 },
                new Modelo { Nome = "Ninja H2", AnoLancamento = 2022, Cilindrada = 1000, MarcaId = 3 },
                new Modelo { Nome = "ZX-6R", AnoLancamento = 2021, Cilindrada = 600, MarcaId = 3 },

                // Suzuki
                new Modelo { Nome = "GSX-R1000", AnoLancamento = 2022, Cilindrada = 1000, MarcaId = 4 },
                new Modelo { Nome = "GSX-R750", AnoLancamento = 2021, Cilindrada = 750, MarcaId = 4 },
                new Modelo { Nome = "GSX-S1000", AnoLancamento = 2023, Cilindrada = 1000, MarcaId = 4 },
                new Modelo { Nome = "SV650", AnoLancamento = 2023, Cilindrada = 650, MarcaId = 4 },
                new Modelo { Nome = "V-Strom 650", AnoLancamento = 2022, Cilindrada = 650, MarcaId = 4 },
                new Modelo { Nome = "Hayabusa", AnoLancamento = 2022, Cilindrada = 1300, MarcaId = 4 },
                new Modelo { Nome = "GSX-R600", AnoLancamento = 2020, Cilindrada = 600, MarcaId = 4 },
                new Modelo { Nome = "Katana", AnoLancamento = 2023, Cilindrada = 1000, MarcaId = 4 },

                // Ducati
                new Modelo { Nome = "Panigale V4", AnoLancamento = 2023, Cilindrada = 1100, MarcaId = 5 },
                new Modelo { Nome = "Panigale V2", AnoLancamento = 2022, Cilindrada = 955, MarcaId = 5 },
                new Modelo { Nome = "Monster 821", AnoLancamento = 2021, Cilindrada = 821, MarcaId = 5 },
                new Modelo { Nome = "Multistrada V4", AnoLancamento = 2023, Cilindrada = 1158, MarcaId = 5 },
                new Modelo { Nome = "Scrambler 800", AnoLancamento = 2022, Cilindrada = 803, MarcaId = 5 },
                new Modelo { Nome = "SuperSport 950", AnoLancamento = 2023, Cilindrada = 937, MarcaId = 5 },
                new Modelo { Nome = "Diavel 1260", AnoLancamento = 2022, Cilindrada = 1262, MarcaId = 5 },
                new Modelo { Nome = "Streetfighter V4", AnoLancamento = 2023, Cilindrada = 1103, MarcaId = 5 },

                // BMW
                new Modelo { Nome = "S1000RR", AnoLancamento = 2023, Cilindrada = 1000, MarcaId = 6 },
                new Modelo { Nome = "R1250GS", AnoLancamento = 2022, Cilindrada = 1254, MarcaId = 6 },
                new Modelo { Nome = "F900R", AnoLancamento = 2023, Cilindrada = 895, MarcaId = 6 },
                new Modelo { Nome = "R1250RT", AnoLancamento = 2022, Cilindrada = 1254, MarcaId = 6 },
                new Modelo { Nome = "G310R", AnoLancamento = 2023, Cilindrada = 313, MarcaId = 6 },
                new Modelo { Nome = "S1000R", AnoLancamento = 2022, Cilindrada = 999, MarcaId = 6 },

                // KTM
                new Modelo { Nome = "1290 Super Duke R", AnoLancamento = 2023, Cilindrada = 1301, MarcaId = 7 },
                new Modelo { Nome = "890 Duke", AnoLancamento = 2022, Cilindrada = 889, MarcaId = 7 },
                new Modelo { Nome = "390 Duke", AnoLancamento = 2023, Cilindrada = 373, MarcaId = 7 },
                new Modelo { Nome = "1290 Super Adventure", AnoLancamento = 2022, Cilindrada = 1301, MarcaId = 7 },
                new Modelo { Nome = "RC 390", AnoLancamento = 2023, Cilindrada = 373, MarcaId = 7 }
            };
            context.Modelos.AddRange(modelos);
            context.SaveChanges();

            // ========== DICIONÁRIO DE URLS DE IMAGENS REAIS ==========
            var imagensReais = new Dictionary<string, string>
            {
                // FILTROS
                ["Filtro de Óleo"] = "https://images.unsplash.com/photo-1558980664-769d59546b3d?w=800",
                ["Filtro de Ar"] = "https://images.unsplash.com/photo-1619642751034-765dfdf7c58e?w=800",
                
                // TRAVÕES
                ["Pastilhas Travão"] = "https://images.unsplash.com/photo-1486262715619-67b85e0b08d3?w=800",
                ["Disco Travão"] = "https://images.pexels.com/photos/190574/pexels-photo-190574.jpeg?auto=compress&cs=tinysrgb&w=800",
                
                // TRANSMISSÃO
                ["Corrente"] = "https://images.unsplash.com/photo-1558981359-219d6364c9c8?w=800",
                
                // MOTOR
                ["Velas"] = "https://images.unsplash.com/photo-1486262715619-67b85e0b08d3?w=800",
                ["Óleo"] = "https://images.unsplash.com/photo-1558981403-c5f9899a28bc?w=800",
                
                // ESCAPE
                ["Escape"] = "https://images.unsplash.com/photo-1558980664-4c5f3497c97f?w=800",
                
                // SUSPENSÃO
                ["Amortecedor"] = "https://images.unsplash.com/photo-1558981359-219d6364c9c8?w=800",
                
                // PNEUS
                ["Pneu"] = "https://images.pexels.com/photos/248747/pexels-photo-248747.jpeg?auto=compress&cs=tinysrgb&w=800"
            };

            // Função auxiliar para obter URL da imagem
            string ObterImagemUrl(string nomePeca)
            {
                foreach (var kvp in imagensReais)
                {
                    if (nomePeca.Contains(kvp.Key))
                        return kvp.Value;
                }
                return "https://images.unsplash.com/photo-1558981359-219d6364c9c8?w=800"; // imagem padrão
            }

            // ========== PEÇAS (500+ peças realistas) ==========
            var random = new Random();
            var pecas = new List<Peca>();

            // Gerar peças para cada modelo
            foreach (var modelo in modelos)
            {
                // Filtros
                var filtroOleo = new Peca
                {
                    Nome = $"Filtro de Óleo {modelo.Nome}",
                    Referencia = $"FO-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Filtro de óleo de alta performance para {modelo.Nome}",
                    Preco = Math.Round((decimal)(random.NextDouble() * 20 + 10), 2),
                    Stock = random.Next(20, 100),
                    PecaOriginal = random.Next(0, 2) == 1,
                    CategoriaId = 10,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Filtro de Óleo")
                };
                pecas.Add(filtroOleo);

                var filtroAr = new Peca
                {
                    Nome = $"Filtro de Ar {modelo.Nome}",
                    Referencia = $"FA-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Filtro de ar de alto fluxo para {modelo.Nome}",
                    Preco = Math.Round((decimal)(random.NextDouble() * 60 + 30), 2),
                    Stock = random.Next(15, 80),
                    PecaOriginal = random.Next(0, 2) == 1,
                    CategoriaId = 10,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Filtro de Ar")
                };
                pecas.Add(filtroAr);

                // Pastilhas de travão
                pecas.Add(new Peca
                {
                    Nome = $"Pastilhas Travão Dianteiras {modelo.Nome}",
                    Referencia = $"PTD-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Pastilhas cerâmicas de alto desempenho para travão dianteiro",
                    Preco = Math.Round((decimal)(random.NextDouble() * 80 + 40), 2),
                    Stock = random.Next(10, 60),
                    PecaOriginal = random.Next(0, 2) == 1,
                    Peso = 0.5m,
                    CategoriaId = 3,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Pastilhas Travão")
                });

                pecas.Add(new Peca
                {
                    Nome = $"Pastilhas Travão Traseiras {modelo.Nome}",
                    Referencia = $"PTT-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Pastilhas para travão traseiro",
                    Preco = Math.Round((decimal)(random.NextDouble() * 60 + 30), 2),
                    Stock = random.Next(10, 60),
                    PecaOriginal = random.Next(0, 2) == 1,
                    Peso = 0.4m,
                    CategoriaId = 3,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Pastilhas Travão")
                });

                // Discos de travão
                pecas.Add(new Peca
                {
                    Nome = $"Disco Travão Dianteiro {modelo.Nome}",
                    Referencia = $"DTD-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Disco flutuante Brembo para travão dianteiro",
                    Preco = Math.Round((decimal)(random.NextDouble() * 350 + 200), 2),
                    Stock = random.Next(5, 30),
                    PecaOriginal = false,
                    Peso = 1.8m,
                    CategoriaId = 3,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Disco Travão")
                });

                // Corrente
                pecas.Add(new Peca
                {
                    Nome = $"Corrente Transmissão {modelo.Nome}",
                    Referencia = $"CT-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Corrente DID X-Ring de alta resistência",
                    Preco = Math.Round((decimal)(random.NextDouble() * 100 + 60), 2),
                    Stock = random.Next(15, 50),
                    PecaOriginal = random.Next(0, 2) == 1,
                    Peso = 2.1m,
                    CategoriaId = 2,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Corrente")
                });

                // Velas
                pecas.Add(new Peca
                {
                    Nome = $"Velas de Ignição {modelo.Nome}",
                    Referencia = $"VI-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Velas NGK Iridium de longa duração",
                    Preco = Math.Round((decimal)(random.NextDouble() * 40 + 15), 2),
                    Stock = random.Next(30, 100),
                    PecaOriginal = true,
                    Peso = 0.1m,
                    CategoriaId = 6,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Velas")
                });

                // Óleo
                pecas.Add(new Peca
                {
                    Nome = $"Óleo Motor 10W40 {modelo.Nome}",
                    Referencia = $"OM-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Óleo sintético 10W40 4L",
                    Preco = Math.Round((decimal)(random.NextDouble() * 50 + 35), 2),
                    Stock = random.Next(20, 80),
                    PecaOriginal = true,
                    Peso = 4.2m,
                    CategoriaId = 1,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Óleo")
                });

                // Apenas para motos de maior cilindrada
                if (modelo.Cilindrada >= 600)
                {
                    // Escape
                    pecas.Add(new Peca
                    {
                        Nome = $"Escape Completo Akrapovic {modelo.Nome}",
                        Referencia = $"AKR-{modelo.Id}-{random.Next(1000, 9999)}",
                        Descricao = $"Sistema de escape em titânio racing",
                        Preco = Math.Round((decimal)(random.NextDouble() * 1500 + 800), 2),
                        Stock = random.Next(1, 10),
                        PecaOriginal = false,
                        Peso = 4.5m,
                        CategoriaId = 5,
                        ModeloId = modelo.Id,
                        ImagemUrl = ObterImagemUrl("Escape")
                    });

                    // Amortecedor
                    pecas.Add(new Peca
                    {
                        Nome = $"Amortecedor Traseiro Ohlins {modelo.Nome}",
                        Referencia = $"OHL-{modelo.Id}-{random.Next(1000, 9999)}",
                        Descricao = $"Amortecedor ajustável de competição",
                        Preco = Math.Round((decimal)(random.NextDouble() * 900 + 600), 2),
                        Stock = random.Next(2, 15),
                        PecaOriginal = false,
                        Peso = 3.2m,
                        CategoriaId = 4,
                        ModeloId = modelo.Id,
                        ImagemUrl = ObterImagemUrl("Amortecedor")
                    });
                }

                // Pneus
                pecas.Add(new Peca
                {
                    Nome = $"Pneu Dianteiro {modelo.Nome}",
                    Referencia = $"PND-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Pneu Michelin Power GP dianteiro",
                    Preco = Math.Round((decimal)(random.NextDouble() * 180 + 120), 2),
                    Stock = random.Next(10, 40),
                    PecaOriginal = false,
                    Peso = 4.5m,
                    CategoriaId = 8,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Pneu")
                });

                pecas.Add(new Peca
                {
                    Nome = $"Pneu Traseiro {modelo.Nome}",
                    Referencia = $"PNT-{modelo.Id}-{random.Next(1000, 9999)}",
                    Descricao = $"Pneu Michelin Power GP traseiro",
                    Preco = Math.Round((decimal)(random.NextDouble() * 220 + 150), 2),
                    Stock = random.Next(10, 40),
                    PecaOriginal = false,
                    Peso = 6.2m,
                    CategoriaId = 8,
                    ModeloId = modelo.Id,
                    ImagemUrl = ObterImagemUrl("Pneu")
                });
            }

            context.Pecas.AddRange(pecas);
            context.SaveChanges();

            // ========== CLIENTES ==========
            var clientes = new[]
            {
                new Cliente
                {
                    Nome = "João Silva",
                    Email = "joao.silva@email.com",
                    Telefone = "912345678",
                    Morada = "Rua das Flores, 123, 1200-123 Lisboa",
                    NIF = "123456789",
                    DataRegisto = DateTime.Now.AddMonths(-12)
                },
                new Cliente
                {
                    Nome = "Maria Santos",
                    Email = "maria.santos@email.com",
                    Telefone = "923456789",
                    Morada = "Avenida da República, 456, 4000-123 Porto",
                    NIF = "234567890",
                    DataRegisto = DateTime.Now.AddMonths(-8)
                },
                new Cliente
                {
                    Nome = "Pedro Costa",
                    Email = "pedro.costa@email.com",
                    Telefone = "934567890",
                    Morada = "Praceta do Sol, 789, 4700-456 Braga",
                    NIF = "345678901",
                    DataRegisto = DateTime.Now.AddMonths(-6)
                },
                new Cliente
                {
                    Nome = "Ana Rodrigues",
                    Email = "ana.rodrigues@email.com",
                    Telefone = "945678901",
                    Morada = "Travessa da Paz, 321, 3000-789 Coimbra",
                    NIF = "456789012",
                    DataRegisto = DateTime.Now.AddMonths(-4)
                },
                new Cliente
                {
                    Nome = "Rui Ferreira",
                    Email = "rui.ferreira@email.com",
                    Telefone = "916789012",
                    Morada = "Largo do Rato, 15, 1200-345 Lisboa",
                    NIF = "567890123",
                    DataRegisto = DateTime.Now.AddMonths(-3)
                },
                new Cliente
                {
                    Nome = "Sofia Lopes",
                    Email = "sofia.lopes@email.com",
                    Telefone = "927890123",
                    Morada = "Rua de Santa Catarina, 67, 4000-456 Porto",
                    NIF = "678901234",
                    DataRegisto = DateTime.Now.AddMonths(-2)
                },
                new Cliente
                {
                    Nome = "Miguel Almeida",
                    Email = "miguel.almeida@email.com",
                    Telefone = "938901234",
                    Morada = "Avenida Central, 234, 2700-123 Amadora",
                    NIF = "789012345",
                    DataRegisto = DateTime.Now.AddMonths(-1)
                },
                new Cliente
                {
                    Nome = "Carla Pereira",
                    Email = "carla.pereira@email.com",
                    Telefone = "919012345",
                    Morada = "Rua do Comércio, 89, 2800-456 Almada",
                    NIF = "890123456",
                    DataRegisto = DateTime.Now.AddDays(-20)
                }
            };
            context.Clientes.AddRange(clientes);
            context.SaveChanges();

            // ========== ENCOMENDAS ==========
            var encomendas = new List<Encomenda>
            {
                // Encomenda 1 - João Silva (Entregue)
                new Encomenda
                {
                    ClienteId = 1,
                    DataEncomenda = DateTime.Now.AddDays(-30),
                    Estado = "Entregue",
                    ValorTotal = 0 // será calculado depois
                },
                // Encomenda 2 - Maria Santos (Entregue)
                new Encomenda
                {
                    ClienteId = 2,
                    DataEncomenda = DateTime.Now.AddDays(-25),
                    Estado = "Entregue",
                    ValorTotal = 0
                },
                // Encomenda 3 - Pedro Costa (Em Processamento)
                new Encomenda
                {
                    ClienteId = 3,
                    DataEncomenda = DateTime.Now.AddDays(-10),
                    Estado = "Em Processamento",
                    ValorTotal = 0
                },
                // Encomenda 4 - Ana Rodrigues (Pendente)
                new Encomenda
                {
                    ClienteId = 4,
                    DataEncomenda = DateTime.Now.AddDays(-5),
                    Estado = "Pendente",
                    ValorTotal = 0
                },
                // Encomenda 5 - Rui Ferreira (Entregue)
                new Encomenda
                {
                    ClienteId = 5,
                    DataEncomenda = DateTime.Now.AddDays(-15),
                    Estado = "Entregue",
                    ValorTotal = 0
                },
                // Encomenda 6 - Sofia Lopes (Enviada)
                new Encomenda
                {
                    ClienteId = 6,
                    DataEncomenda = DateTime.Now.AddDays(-7),
                    Estado = "Enviada",
                    ValorTotal = 0
                },
                // Encomenda 7 - Miguel Almeida (Pendente)
                new Encomenda
                {
                    ClienteId = 7,
                    DataEncomenda = DateTime.Now.AddDays(-3),
                    Estado = "Pendente",
                    ValorTotal = 0
                },
                // Encomenda 8 - Carla Pereira (Em Processamento)
                new Encomenda
                {
                    ClienteId = 8,
                    DataEncomenda = DateTime.Now.AddDays(-1),
                    Estado = "Em Processamento",
                    ValorTotal = 0
                }
            };
            context.Encomendas.AddRange(encomendas);
            context.SaveChanges();

            // ========== ITENS DAS ENCOMENDAS ==========
            var itensEncomenda = new List<ItemEncomenda>();

            // Encomenda 1 - João Silva (Manutenção completa CBR600RR)
            var peca1_1 = pecas.First(p => p.Nome.Contains("Filtro de Óleo") && p.ModeloId == 1);
            var peca1_2 = pecas.First(p => p.Nome.Contains("Filtro de Ar") && p.ModeloId == 1);
            var peca1_3 = pecas.First(p => p.Nome.Contains("Velas de Ignição") && p.ModeloId == 1);
            var peca1_4 = pecas.First(p => p.Nome.Contains("Óleo Motor") && p.ModeloId == 1);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 1, PecaId = peca1_1.Id, Quantidade = 1, PrecoUnitario = peca1_1.Preco },
                new ItemEncomenda { EncomendaId = 1, PecaId = peca1_2.Id, Quantidade = 1, PrecoUnitario = peca1_2.Preco },
                new ItemEncomenda { EncomendaId = 1, PecaId = peca1_3.Id, Quantidade = 4, PrecoUnitario = peca1_3.Preco },
                new ItemEncomenda { EncomendaId = 1, PecaId = peca1_4.Id, Quantidade = 1, PrecoUnitario = peca1_4.Preco }
            });

            // Encomenda 2 - Maria Santos (Travões YZF-R1)
            var peca2_1 = pecas.First(p => p.Nome.Contains("Pastilhas Travão Dianteiras") && p.ModeloId == 11);
            var peca2_2 = pecas.First(p => p.Nome.Contains("Disco Travão Dianteiro") && p.ModeloId == 11);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 2, PecaId = peca2_1.Id, Quantidade = 2, PrecoUnitario = peca2_1.Preco },
                new ItemEncomenda { EncomendaId = 2, PecaId = peca2_2.Id, Quantidade = 2, PrecoUnitario = peca2_2.Preco }
            });

            // Encomenda 3 - Pedro Costa (Escape + Amortecedor Ninja ZX-10R)
            var peca3_1 = pecas.First(p => p.Nome.Contains("Escape Completo Akrapovic") && p.ModeloId == 21);
            var peca3_2 = pecas.First(p => p.Nome.Contains("Amortecedor Traseiro Ohlins") && p.ModeloId == 21);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 3, PecaId = peca3_1.Id, Quantidade = 1, PrecoUnitario = peca3_1.Preco },
                new ItemEncomenda { EncomendaId = 3, PecaId = peca3_2.Id, Quantidade = 1, PrecoUnitario = peca3_2.Preco }
            });

            // Encomenda 4 - Ana Rodrigues (Pneus GSX-R1000)
            var peca4_1 = pecas.First(p => p.Nome.Contains("Pneu Dianteiro") && p.ModeloId == 31);
            var peca4_2 = pecas.First(p => p.Nome.Contains("Pneu Traseiro") && p.ModeloId == 31);
            var peca4_3 = pecas.First(p => p.Nome.Contains("Corrente Transmissão") && p.ModeloId == 31);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 4, PecaId = peca4_1.Id, Quantidade = 1, PrecoUnitario = peca4_1.Preco },
                new ItemEncomenda { EncomendaId = 4, PecaId = peca4_2.Id, Quantidade = 1, PrecoUnitario = peca4_2.Preco },
                new ItemEncomenda { EncomendaId = 4, PecaId = peca4_3.Id, Quantidade = 1, PrecoUnitario = peca4_3.Preco }
            });

            // Encomenda 5 - Rui Ferreira (Manutenção Panigale V4)
            var peca5_1 = pecas.First(p => p.Nome.Contains("Filtro de Óleo") && p.ModeloId == 39);
            var peca5_2 = pecas.First(p => p.Nome.Contains("Óleo Motor") && p.ModeloId == 39);
            var peca5_3 = pecas.First(p => p.Nome.Contains("Pastilhas Travão Dianteiras") && p.ModeloId == 39);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 5, PecaId = peca5_1.Id, Quantidade = 1, PrecoUnitario = peca5_1.Preco },
                new ItemEncomenda { EncomendaId = 5, PecaId = peca5_2.Id, Quantidade = 2, PrecoUnitario = peca5_2.Preco },
                new ItemEncomenda { EncomendaId = 5, PecaId = peca5_3.Id, Quantidade = 1, PrecoUnitario = peca5_3.Preco }
            });

            // Encomenda 6 - Sofia Lopes (Upgrade S1000RR)
            var peca6_1 = pecas.First(p => p.Nome.Contains("Escape Completo Akrapovic") && p.ModeloId == 47);
            var peca6_2 = pecas.First(p => p.Nome.Contains("Filtro de Ar") && p.ModeloId == 47);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 6, PecaId = peca6_1.Id, Quantidade = 1, PrecoUnitario = peca6_1.Preco },
                new ItemEncomenda { EncomendaId = 6, PecaId = peca6_2.Id, Quantidade = 1, PrecoUnitario = peca6_2.Preco }
            });

            // Encomenda 7 - Miguel Almeida (Consumíveis 1290 Super Duke R)
            var peca7_1 = pecas.First(p => p.Nome.Contains("Filtro de Óleo") && p.ModeloId == 53);
            var peca7_2 = pecas.First(p => p.Nome.Contains("Velas de Ignição") && p.ModeloId == 53);
            var peca7_3 = pecas.First(p => p.Nome.Contains("Corrente Transmissão") && p.ModeloId == 53);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 7, PecaId = peca7_1.Id, Quantidade = 1, PrecoUnitario = peca7_1.Preco },
                new ItemEncomenda { EncomendaId = 7, PecaId = peca7_2.Id, Quantidade = 2, PrecoUnitario = peca7_2.Preco },
                new ItemEncomenda { EncomendaId = 7, PecaId = peca7_3.Id, Quantidade = 1, PrecoUnitario = peca7_3.Preco }
            });

            // Encomenda 8 - Carla Pereira (Travões completos MT-07)
            var peca8_1 = pecas.First(p => p.Nome.Contains("Pastilhas Travão Dianteiras") && p.ModeloId == 13);
            var peca8_2 = pecas.First(p => p.Nome.Contains("Pastilhas Travão Traseiras") && p.ModeloId == 13);
            var peca8_3 = pecas.First(p => p.Nome.Contains("Disco Travão Dianteiro") && p.ModeloId == 13);
            
            itensEncomenda.AddRange(new[]
            {
                new ItemEncomenda { EncomendaId = 8, PecaId = peca8_1.Id, Quantidade = 1, PrecoUnitario = peca8_1.Preco },
                new ItemEncomenda { EncomendaId = 8, PecaId = peca8_2.Id, Quantidade = 1, PrecoUnitario = peca8_2.Preco },
                new ItemEncomenda { EncomendaId = 8, PecaId = peca8_3.Id, Quantidade = 1, PrecoUnitario = peca8_3.Preco }
            });

            context.ItensEncomenda.AddRange(itensEncomenda);
            context.SaveChanges();

            // ========== CALCULAR VALORES TOTAIS DAS ENCOMENDAS ==========
            foreach (var encomenda in encomendas)
            {
                var itens = itensEncomenda.Where(i => i.EncomendaId == encomenda.Id);
                encomenda.ValorTotal = itens.Sum(i => i.PrecoUnitario * i.Quantidade);
            }
            context.SaveChanges();

            Console.WriteLine("✅ Base de dados inicializada com sucesso!");
            Console.WriteLine($"📊 Estatísticas:");
            Console.WriteLine($"   • {context.Marcas.Count()} Marcas");
            Console.WriteLine($"   • {context.Modelos.Count()} Modelos");
            Console.WriteLine($"   • {context.Categorias.Count()} Categorias");
            Console.WriteLine($"   • {context.Pecas.Count()} Peças");
            Console.WriteLine($"   • {context.Clientes.Count()} Clientes");
            Console.WriteLine($"   • {context.Encomendas.Count()} Encomendas");
            Console.WriteLine($"   • {context.ItensEncomenda.Count()} Itens de Encomenda");
        }
    }
}