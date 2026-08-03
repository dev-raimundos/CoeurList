# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

CoeurMobile is a personal finance mobile app built with **.NET MAUI Blazor Hybrid** (net10.0), targeting **Android** as the primary platform. It consumes a separate ASP.NET Core backend ("CoeurApi") for authentication and financial data persistence. All UI is Blazor (Razor components) rendered inside a `BlazorWebView`; there is no native XAML UI beyond `MainPage.xaml` (the BlazorWebView host).

There is a single project, `CoeurMobile/CoeurMobile.csproj` (`net10.0-android`), opened via `CoeurMobile.slnx`. There used to be a separate `CoeurMobile.Application` class library holding the API/auth logic — it was removed (see commit `323b1d9`) and its code folded back into this project under `App/Core` and `App/Modules`. **`README.md` still describes the old class-library split and Windows target support — don't rely on it; treat the actual `.csproj` and folder structure below as the source of truth.** The stray top-level folders `CoeurApp.Mobile/` and `Coeur.Mobile.Application/` are untracked leftovers from that refactor (not in git, not referenced by the solution) — ignore them.

## Commands

```bash
# Build for Android
dotnet build CoeurMobile/CoeurMobile.csproj -f net10.0-android

# Restore
dotnet restore CoeurMobile/CoeurMobile.csproj
```

There is no test project and no lint/format command configured in this repo. `.csstools.json` at the repo root configures an external CSS-linting tool (max 5 rule definitions, max 8 lines per scoped `.razor.css` file) — keep scoped component stylesheets small and split components rather than growing one `.razor.css` file past that budget.

Deploying/running the app is done through Visual Studio (`CoeurMobile.slnx`, Android emulator/device as the debug target) rather than a CLI run command.

## Architecture

### Folder structure (`CoeurMobile/App/`)

- `Core/` — cross-cutting infrastructure, not tied to any one feature:
  - `Config/AppConfig.cs` — API base URL, switched by `#if DEBUG` (`10.0.2.2:8000` for the Android emulator loopback to host) vs release (`api.coeur.app.br`).
  - `Http/Client/` — `ICoeurApiClient` / `CoeurApiClient`, the typed HTTP client for the backend.
  - `Http/Handlers/` — `DelegatingHandler` pipeline registered in `MauiProgram.cs`: `BearerTokenHandler` (attaches `Authorization: Bearer` from `TokenAccessor`) runs first, then `ApiExceptionHandler` (turns non-2xx responses into toasts + throws `CoeurApiException`, and flags `TokenAccessor` on 401 to trigger auto-logout).
  - `Services/` — app-wide singletons: `ThemeService` (light/dark toggle), `ToastService`/`IToastService`, `CoeurAuthenticationStateProvider` (bridges `AuthService` state into Blazor's `AuthenticationStateProvider`/`AuthorizeRouteView`).
  - `Theme/AppTheme.cs` — the MudBlazor `MudTheme` (light/dark palettes), applied in `MainLayout` via `<MudThemeProvider Theme="AppTheme.Theme" IsDarkMode="ThemeService.IsDarkMode" />`.
  - `Layout/MainLayout/` — the authenticated app shell (nav menu, toast listener, MudBlazor providers).
- `Modules/` — one folder per feature, each with its own `Pages/`, `Services/`, `Dtos/`, `Models/` as needed (currently `Auth`, `Home`, `Profile`, `Palette`). Put new features here rather than in `Core`.
- `Shared/Components/` — reusable UI not owned by a single module (`NavMenu`, `NotFound`, `ToastListener`).
- `Routes.razor` — the Blazor `Router`; wraps everything in `CascadingAuthenticationState` + `AuthorizeRouteView` with a fallback `RequireAuthenticatedUser()` policy (see `MauiProgram.cs`), so **every route requires auth by default** — unauthenticated users are redirected via `RedirectToLogin`.

### Component convention

Non-trivial Razor components are split three ways, all same name/folder:
- `Foo.razor` — markup only
- `Foo.razor.cs` — `public partial class Foo` code-behind (`[Inject]` properties, event handlers, private state)
- `Foo.razor.css` — scoped CSS (kept small — see `.csstools.json` budget above)

### Auth flow

`AuthService` (singleton) is the source of truth for the current session:
- On construction it kicks off `LoadSessionAsync()` (fire-and-forget, awaited via `EnsureInitializedAsync()`) to restore a persisted session from `ISecureSessionStore` (`MauiSecureSessionStore`, backed by MAUI `SecureStorage`).
- `LoginAsync` calls `ICoeurApiClient.LoginAsync`, stores the resulting `AuthSession` in memory + `TokenAccessor.Token` + secure storage, then raises `OnChange`.
- `TokenAccessor.OnUnauthorized` (raised by `ApiExceptionHandler` on a 401) triggers `LogoutAsync()` automatically — no user interaction needed to clear a dead session.
- `CoeurAuthenticationStateProvider` subscribes to `AuthService.OnChange` and translates session state into a `ClaimsPrincipal`, which is what actually drives `AuthorizeRouteView`/the router redirect to `/login`.

When adding a new API call, add a method to `ICoeurApiClient`/`CoeurApiClient` — don't call `HttpClient` directly from pages/components; the DI-registered client already carries the bearer/error-handling pipeline.

### UI stack

MudBlazor is the component library (`AddMudServices()` in `MauiProgram.cs`); prefer existing MudBlazor components over hand-rolled markup. Toasts go through `IToastService`/`ToastListener` (backed by MudBlazor snackbar), not ad-hoc alerts — services and handlers (like `ApiExceptionHandler`) already push errors through this channel.

## Conventions

- Commit messages: Conventional Commits style (`feat:`, `fix:`, `refactor:`) written in Portuguese, describing the *why*/*what* briefly.
- Code comments and XML doc comments in this codebase are written in Portuguese; match that when editing existing files.
- `Nullable` is enabled — respect nullability annotations rather than suppressing them.
