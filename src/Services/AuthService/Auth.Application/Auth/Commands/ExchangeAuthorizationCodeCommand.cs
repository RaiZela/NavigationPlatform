namespace Auth.Application.Auth.Commands;

public class ExchangeAuthorizationCodeCommand
{
    public string Code { get; set; } = "";
    public string CodeVerifier { get; set; } = "";
}

