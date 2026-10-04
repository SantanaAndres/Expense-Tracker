using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto.Request;
using Application.Helper;
using Application.Helper.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Feature.User.UpdatePasswordUser.ResetPassword;

public record ResetPasswordCommand(string Recipient, string CommunicationServiceType);

public class ResetPasswordHandler(
    CommunicationServiceFactory  factory, 
    ILogger<ResetPasswordHandler> logger, 
    IUserRepository userRepository,
    ITokenService  tokenService,
    IConfiguration conf
    )
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("ResetPasswordHandler");
        
        var appLink = conf.GetSection("AppLink").Value;
        Domain.Entities.User? user = null;
        Domain.Entities.User? userMail = null;

        if (string.IsNullOrEmpty(appLink))
        {
            logger.LogError("An app link doesn't has been configure yet.");            
            throw new NotFoundException("Please configure AppLink");
        }
        
        logger.LogInformation($"AppLink: {appLink}");
        
        if (command.CommunicationServiceType == "Sms")
            user = await userRepository.CheckUserExistenceByPhone(command.Recipient, cancellationToken);
        else if (command.CommunicationServiceType == "Email")
            userMail = await userRepository.CheckUserExistenceByEmail(command.Recipient, cancellationToken);
        
        if(user is null || userMail is null)
            throw new NotFoundException("Please provide a valid user");

        GenerateUserTokenDto generateUserTokenDto = new GenerateUserTokenDto(userMail.UserId, userMail.Email);
        
        var token = tokenService.GenerateToken(generateUserTokenDto);

        string resetLink = string.Concat(appLink, "/", "?token=", token);
        
        var communicationService = factory.GetCommunicationService(command.CommunicationServiceType);
        
        logger.LogInformation($"Communication type service: {command.CommunicationServiceType}");

        string message = GetResetPasswordMesssage(command.CommunicationServiceType, resetLink);

        var parameters = new CommunicationServiceParameters(message, "Reset your password", command.Recipient);
        
        var result = await communicationService.SendMessage(parameters);
        
    }

    private string GetResetPasswordMesssage(string communicationServiceType, string appLink)
    {
        if (communicationServiceType == "Sms")
            return string.Concat("Usa este enlace para reestablecer tu contraseña ", appLink, ". Si no solicitaste restablecer tu contraseña, puedes ignorar este mensaje.");

        else
            return $"""
                   <!DOCTYPE html>
                   
                   <html lang="es">
                   <head>
                       <meta charset="UTF-8">
                       <meta name="viewport" content="width=device-width, initial-scale=1.0">
                       <title>Restablecer contraseña - Expense Tracker</title>
                   </head>
                   
                   <body style="
                       margin: 0;
                       padding: 0;
                       background-color: #f4f6f8;
                       font-family: Arial, Helvetica, sans-serif;
                   ">
                   
                   <table width="100%" cellpadding="0" cellspacing="0" border="0"
                          style="background-color: #f4f6f8; padding: 40px 20px;">
                   
                       <tr>
                           <td align="center">
                   
                               <!-- Contenedor -->
                               <table width="500" cellpadding="0" cellspacing="0" border="0"
                                      style="
                                          width: 100%;
                                          max-width: 500px;
                                          background-color: #ffffff;
                                          border-radius: 12px;
                                          overflow: hidden;
                                      ">
                   
                                   <!-- Header -->
                                   <tr>
                                       <td align="center"
                                           style="
                                               background-color: #2563eb;
                                               padding: 30px;
                                           ">
                   
                                           <h1 style="
                                               margin: 0;
                                               color: #ffffff;
                                               font-size: 26px;
                                               font-weight: bold;
                                           ">
                                               Expense Tracker
                                           </h1>
                   
                                       </td>
                                   </tr>
                   
                                   <!-- Contenido -->
                                   <tr>
                                       <td style="padding: 40px 35px;">
                   
                                           <h2 style="
                                               margin: 0 0 15px 0;
                                               color: #111827;
                                               font-size: 22px;
                                           ">
                                               Restablecer contraseña
                                           </h2>
                   
                                           <p style="
                                               margin: 0 0 20px 0;
                                               color: #4b5563;
                                               font-size: 15px;
                                               line-height: 1.6;
                                           ">
                                               Hola
                                           </p>
                   
                                           <p style="
                                               margin: 0 0 25px 0;
                                               color: #4b5563;
                                               font-size: 15px;
                                               line-height: 1.6;
                                           ">
                                               Recibimos una solicitud para restablecer la
                                               contraseña de tu cuenta de Expense Tracker.
                                           </p>
                   
                                           <p style="
                                               margin: 0 0 30px 0;
                                               color: #4b5563;
                                               font-size: 15px;
                                               line-height: 1.6;
                                           ">
                                               Haz clic en el siguiente botón para crear una
                                               nueva contraseña:
                                           </p>
                   
                                           <!-- Botón -->
                                           <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                               <tr>
                                                   <td align="center">
                   
                                                       <a href="{appLink}"
                                                          style="
                                                              display: inline-block;
                                                              background-color: #2563eb;
                                                              color: #ffffff;
                                                              text-decoration: none;
                                                              font-size: 15px;
                                                              font-weight: bold;
                                                              padding: 14px 28px;
                                                              border-radius: 8px;
                                                          ">
                                                           Restablecer contraseña
                                                       </a>
                   
                                                   </td>
                                               </tr>
                                           </table>
                   
                                           <p style="
                                               margin: 30px 0 0 0;
                                               color: #6b7280;
                                               font-size: 13px;
                                               line-height: 1.6;
                                           ">
                                               Este enlace estará disponible durante
                                               15 minutos.
                                           </p>
                   
                                           <p style="
                                               margin: 15px 0 0 0;
                                               color: #6b7280;
                                               font-size: 13px;
                                               line-height: 1.6;
                                           ">
                                               Si no solicitaste restablecer tu contraseña,
                                               puedes ignorar este correo. Tu contraseña
                                               permanecerá sin cambios.
                                           </p>
                   
                                       </td>
                                   </tr>
                   
                                   <!-- Footer -->
                                   <tr>
                                       <td align="center"
                                           style="
                                               background-color: #f9fafb;
                                               padding: 20px 30px;
                                               border-top: 1px solid #e5e7eb;
                                           ">
                   
                                           <p style="
                                               margin: 0;
                                               color: #9ca3af;
                                               font-size: 12px;
                                           ">
                                               © 2026 Expense Tracker
                                           </p>
                   
                                           <p style="
                                               margin: 6px 0 0 0;
                                               color: #9ca3af;
                                               font-size: 12px;
                                           ">
                                               Este es un correo automático, por favor no respondas.
                                           </p>
                   
                                       </td>
                                   </tr>
                   
                               </table>
                   
                           </td>
                       </tr>
                   
                   </table>
                   
                   
                   </body>
                   </html>
                   """;
    }
}

public class ResetPasswordCommandValidator: AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Recipient).NotEmpty().WithMessage("Recipient is required");
        RuleFor(x => x.CommunicationServiceType).NotEmpty().WithMessage("CommunicationServiceType is required");
    }
}