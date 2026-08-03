# CoeurMobile

Aplicativo mobile de planejamento financeiro pessoal, com foco principal na plataforma **Android**. Construído com **.NET MAUI Blazor Hybrid**, consome a API **CoeurApi** (backend em ASP.NET Core) para autenticação e persistência dos dados financeiros do usuário.

## Estrutura do repositório

```
CoeurMobile/
├── CoeurMobile.slnx          # Solução (.NET solution file, formato .slnx)
└── CoeurMobile/              # Projeto único: app MAUI Blazor Hybrid
    ├── App/
    │   ├── Core/              # Infra transversal: Config, Http (client + interceptors), Layout,
    │   │                      #   Services (tema, toast, auth state), Theme
    │   ├── Modules/           # Módulos de feature (Auth, Home, Profile, Palette), cada um com
    │   │                      #   suas próprias Pages/, Services/, Dtos/, Models/
    │   ├── Shared/Components/ # UI reaproveitável entre módulos (NavMenu, NotFound, ToastListener)
    │   └── Routes.razor       # Router raiz; toda rota exige autenticação por padrão
    ├── Platforms/Android/     # Código específico da plataforma Android
    ├── Resources/             # Ícones, splash screen, fontes, imagens
    └── wwwroot/               # Assets estáticos servidos ao BlazorWebView
```

Não há mais uma class library separada para a lógica de integração — toda a comunicação com a API (cliente HTTP, DTOs, autenticação) vive dentro do próprio `CoeurMobile/App`.

## Backend

O app consome a **CoeurApi**, uma aplicação ASP.NET Core mantida em repositório separado. O `CoeurMobile` não expõe nenhuma lógica de negócio própria além da UI: toda comunicação com a API passa por `App/Core/Http` (cliente HTTP tipado + handlers de autenticação/erro).

## Plataformas suportadas

Definidas em `CoeurMobile.csproj`:

- **Android** (`net10.0-android`) — única plataforma alvo

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
dotnet build CoeurMobile/CoeurMobile.csproj -f net10.0-android
```

## Status

Projeto em estágio inicial. Autenticação (login, sessão persistida com validação contra a API no início do app, logout automático em token inválido) já está implementada; as telas de planejamento financeiro em si ainda não.

## Licença

Distribuído sob a licença MIT. Veja [LICENSE.txt](LICENSE.txt).
