namespace CoeurMobile.App.Modules.Auth.Validation;

public static class EmailValidator
{
    // O "obrigatório" já é responsabilidade do Required do form; aqui só valida o formato quando preenchido.
    public static bool HasValidFormat(string email)
    {
        return string.IsNullOrWhiteSpace(email) || email.Contains('@');
    }
}
