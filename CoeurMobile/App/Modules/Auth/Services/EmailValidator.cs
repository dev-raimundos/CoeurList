namespace CoeurMobile.App.Modules.Auth.Services;

public static class EmailValidator
{
    // O "obrigatório" já é responsabilidade do Required do form; aqui só valida o formato quando preenchido.
    public static bool HasValidFormat(string email)
        => string.IsNullOrWhiteSpace(email) || email.Contains('@');
}
