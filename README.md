# MotoPartsShop - Plataforma de E-commerce para Peças de Motocicleta

## Descrição do Projeto

MotoPartsShop é uma aplicação web desenvolvida em ASP.NET Core para uma loja online de peças de motocicleta. O projeto permite a gestão de peças, categorias, marcas e modelos, além de funcionalidades de e-commerce como carrinho de compras, pedidos, favoritos, avaliações e notificações de stock. Inclui sistema de autenticação de usuários com ASP.NET Identity e suporte a múltiplos idiomas (Português, Inglês, Espanhol e Francês). Este projeto foi desenvolvido como trabalho acadêmico para demonstrar habilidades em desenvolvimento web full-stack usando ASP.NET Core e tecnologias modernas.

## Tecnologias Utilizadas

- **Backend**: C#, ASP.NET Core 10.0
- **Banco de Dados**: SQLite (configurado para desenvolvimento; pode ser alterado para SQL Server em produção)
- **ORM**: Entity Framework Core 10.0.1
- **Frontend**: HTML, CSS, JavaScript, Razor Pages, MVC
- **Autenticação**: ASP.NET Identity com roles
- **Localização**: Microsoft.Extensions.Localization
- **Paginação**: X.PagedList
- **Excel**: ClosedXML para exportação de dados
- **Sessões**: Microsoft.AspNetCore.Session

## Requisitos

- .NET 10.0 SDK ou superior
- Visual Studio 2022 ou VS Code com extensão C#
- SQLite (incluído no projeto)

## Instruções de Instalação

Siga estes passos para configurar o projeto localmente:

1. **Clone o repositório**:
   ```bash
   git clone <url-do-repositorio>
   cd "Programação Web - Servidor II"
   ```

2. **Instale as dependências**:
   ```bash
   cd MotoPartsShop
   dotnet restore
   ```

3. **Configure o ambiente**:
   - Edite `appsettings.json` e configure as seguintes definições:
     - Connection string do banco de dados (padrão é SQLite)
     - Outras variáveis de ambiente conforme necessário

4. **Execute as migrações do banco de dados**:
   ```bash
   dotnet ef database update
   ```

5. **Inicie o servidor de desenvolvimento**:
   ```bash
   dotnet run
   ```

A aplicação estará disponível em `https://localhost:7220` ou `http://localhost:5161`.

## Configuração do appsettings.json

Certifique-se de que seu `appsettings.json` inclui as seguintes configurações principais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db;Cache=Shared"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Para produção, altere a connection string para SQL Server, por exemplo:
```json
"DefaultConnection": "Server=localhost;Database=MotoPartsShop;Trusted_Connection=True;MultipleActiveResultSets=true"
```

## Executando Migrações e Seeders

Após configurar o banco de dados em `appsettings.json`:

1. **Execute as migrações**:
   ```bash
   dotnet ef database update
   ```

2. **Execute o inicializador de dados** (para popular com dados de exemplo):
   - O `DbInitializer` é executado automaticamente na inicialização da aplicação, populando categorias, marcas, modelos e peças de exemplo.

## Funcionalidades Principais

- **Descoberta de Peças**: Navegue e pesquise peças por categoria, marca e modelo
- **Autenticação de Usuários**: Registro, login e gestão de perfis
- **Avaliações de Peças**: Usuários podem avaliar peças em uma escala
- **Comentários**: Usuários autenticados podem deixar comentários em peças
- **Favoritos**: Usuários podem adicionar/remover peças da lista de favoritos
- **Painel Administrativo**: Interface administrativa para gestão de usuários, peças e comentários
- **Carrinho de Compras**: Sistema completo de e-commerce com carrinho, pedidos e encomendas
- **Comparador de Peças**: Ferramenta para comparar peças lado a lado
- **Notificações de Stock**: Usuários podem se inscrever para notificações quando peças voltam ao stock
- **Cupões de Desconto**: Sistema de cupões para promoções
- **Multi-idioma**: Suporte a Português (padrão), Inglês, Espanhol e Francês

## Perfis de Usuário

### Usuário Regular
- Visualizar peças e seus detalhes
- Pesquisar e filtrar peças
- Registrar e autenticar
- Avaliar peças (escala 1-5)
- Deixar comentários em peças
- Adicionar/remover peças dos favoritos
- Editar informações do perfil
- Gerenciar carrinho e pedidos

### Administrador
- Todas as permissões de usuário regular
- Acesso ao painel de administração
- Gerenciar usuários (visualizar, alterar status de admin, excluir)
- Gerenciar peças (criar, editar, excluir)
- Gerenciar categorias, marcas e modelos
- Gerenciar comentários (visualizar, excluir)
- Gerenciar pedidos e encomendas
- Exportar dados para Excel (usuários e peças)

## Estrutura do Projeto

```
Programação Web - Servidor II/
├── MotoPartsShop/
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── MotoPartsShop.csproj
│   ├── Program.cs
│   ├── ScaffoldingReadMe.txt
│   ├── Areas/
│   │   └── Identity/
│   │       └── Pages/
│   ├── bin/
│   │   └── Debug/
│   │       └── net10.0/
│   ├── Controllers/
│   │   └── LanguageController.cs
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   ├── DbInitializer.cs
│   │   ├── RoleSeeder.cs
│   │   └── Migrations/
│   │       ├── 00000000000000_CreateIdentitySchema.cs
│   │       ├── 00000000000000_CreateIdentitySchema.Designer.cs
│   │       ├── 20251212100157_Inicial.cs
│   │       ├── 20251212100157_Inicial.Designer.cs
│   │       ├── 20251213152353_AddUserProfile.cs
│   │       ├── 20251213152353_AddUserProfile.Designer.cs
│   │       ├── 20251214115023_AdicionarPedidosFavoritosAvaliacoes.cs
│   │       └── ...
│   ├── Models/
│   │   ├── Avaliacao.cs
│   │   ├── CarrinhoItem.cs
│   │   ├── Categoria.cs
│   │   ├── Cliente.cs
│   │   ├── ComparadorItem.cs
│   │   ├── Cupao.cs
│   │   ├── Encomenda.cs
│   │   ├── Favorito.cs
│   │   ├── ItemEncomenda.cs
│   │   ├── Marca.cs
│   │   ├── Modelo.cs
│   │   ├── NotificacaoStock.cs
│   │   ├── Peca.cs
│   │   └── Pedido.cs
│   ├── obj/
│   │   ├── MotoPartsShop.csproj.codegeneration.targets
│   │   ├── MotoPartsShop.csproj.nuget.dgspec.json
│   │   ├── MotoPartsShop.csproj.nuget.g.props
│   │   ├── MotoPartsShop.csproj.nuget.g.targets
│   │   ├── project.assets.json
│   │   └── Debug/
│   │       └── net10.0/
│   ├── Pages/
│   │   ├── _ViewImports.cshtml
│   │   ├── _ViewStart.cshtml
│   │   ├── Carrinho.cshtml
│   │   ├── Carrinho.cshtml.cs
│   │   ├── Checkout.cshtml
│   │   ├── Checkout.cshtml.cs
│   │   ├── Comparador.cshtml
│   │   ├── Comparador.cshtml.cs
│   │   ├── Dashboard.cshtml
│   │   ├── Dashboard.cshtml.cs
│   │   ├── Error.cshtml
│   │   ├── Error.cshtml.cs
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── MeusFavoritos.cshtml
│   │   ├── MeusFavoritos.cshtml.cs
│   │   ├── MeusPedidos.cshtml
│   │   ├── MeusPedidos.cshtml.cs
│   │   ├── NotificarStock.cshtml
│   │   ├── NotificarStock.cshtml.cs
│   │   ├── Privacy.cshtml
│   │   ├── Privacy.cshtml.cs
│   │   ├── Profile.cshtml
│   │   ├── Profile.cshtml.cs
│   │   ├── Sucesso.cshtml
│   │   ├── Sucesso.cshtml.cs
│   │   ├── Welcome.cshtml
│   │   ├── Welcome.cshtml.cs
│   │   └── Admin/
│   │       └── ...
│   ├── Pecas/
│   │   └── ...
│   ├── Shared/
│   │   └── ...
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Resources/
│   │   ├── SharedResources.cs
│   │   ├── SharedResources.en.resx
│   │   ├── SharedResources.es.resx
│   │   ├── SharedResources.fr.resx
│   │   └── SharedResources.pt.resx
│   ├── ViewComponents/
│   │   └── UserDisplayNameViewComponent.cs
│   ├── Views/
│   │   └── Shared/
│   ├── wwwroot/
│   │   ├── css/
│   │   ├── images/
│   │   ├── js/
│   │   └── lib/
│   └── MotoPartsShop.sln
├── Programação Web - Servidor II.sln
└── README.md
```

## Credenciais de Teste

Não há credenciais de teste pré-definidas neste projeto. Os usuários devem se registrar através da interface da aplicação para criar contas. O inicializador de banco de dados cria dados de exemplo, mas não define senhas para usuários.
