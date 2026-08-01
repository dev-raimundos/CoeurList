# CoeurMobile

Aplicativo mobile de planejamento financeiro pessoal, com foco principal na plataforma **Android**. Construído com **.NET MAUI Blazor Hybrid**, consome a API **CoeurApi** (backend em ASP.NET Core) para autenticação e persistência dos dados financeiros do usuário.

## Estrutura do repositório

```
CoeurMobile/
├── CoeurMobile.slnx                   # Solução (.NET solution file, formato .slnx)
├── CoeurMobile/                       # App MAUI Blazor Hybrid (UI, telas, recursos da plataforma)
│   ├── App/
│   │   ├── Core/                      # Layouts, componentes, config, tema e serviços transversais
│   │   └── Modules/                   # Módulos de feature (ex.: Auth) com suas páginas e layouts
│   ├── Platforms/                     # Código específico de cada plataforma (Android, Windows)
│   ├── Resources/                     # Ícones, splash screen, fontes, imagens
│   └── wwwroot/                       # Assets estáticos servidos ao BlazorWebView
└── CoeurMobile.Application/           # Class library com a lógica de aplicação
    ├── Authentication/                # Autenticação/sessão do usuário
    ├── DTOs/                          # Contratos (Requests/Responses) trocados com a CoeurApi
    └── Http/                          # Cliente HTTP e configuração de acesso à API
```

`CoeurMobile` referencia `CoeurMobile.Application` via `ProjectReference` — a lib concentra a lógica de integração com o backend, mantendo a camada de UI mais enxuta.

## Backend

O app consome a **CoeurApi**, uma aplicação ASP.NET Core mantida em repositório separado. O `CoeurMobile` não expõe nenhuma lógica de negócio própria além da UI: toda comunicação com a API passa pelo `CoeurMobile.Application`.

## Plataformas suportadas

Definidas em `CoeurMobile.csproj`:

- **Android** (`net10.0-android`) — plataforma alvo principal
- Windows (`net10.0-windows10.0.19041.0`), como app não empacotada (`WindowsPackageType=None`)

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (testado com `10.0.302`)
- Workload do MAUI instalado:
  ```
  dotnet workload install maui
  ```
- Visual Studio 2022 (17.14+) com a carga de trabalho **.NET Multi-platform App UI development**, ou VS Code com a extensão do .NET MAUI

## Build

Abra `CoeurMobile.slnx` no Visual Studio, ou compile via CLI:

```bash
# Android
dotnet build CoeurMobile/CoeurMobile.csproj -f net10.0-android

# Windows
dotnet build CoeurMobile/CoeurMobile.csproj -f net10.0-windows10.0.19041.0
```

## Status

Projeto em estágio inicial — a estrutura de telas e a camada de integração com a CoeurApi ainda estão sendo implementadas.

## Licença

Distribuído sob a licença MIT. Veja [LICENSE.txt](LICENSE.txt).
