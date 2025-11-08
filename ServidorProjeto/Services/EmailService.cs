// ServidorProjeto/Services/EmailService.cs

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using ServidorProjeto.Configurations;
using System;
using System.Threading.Tasks;

namespace ServidorProjeto.Services
{
    // Implementa a interface IEmailService (que deve estar em IEmailService.cs)
    public class EmailService : IEmailService
    {
        // Variável privada que armazena os DADOS de configuração lidos do appsettings.json
        private readonly EmailSettings _emailSettings;

        public EmailService(IConfiguration configuration)
        {
            // Mapeia a seção "EmailSettings" do appsettings.json
            _emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>()
                             ?? throw new InvalidOperationException("Seção 'EmailSettings' não configurada corretamente no appsettings.json. O serviço de e-mail não pode iniciar.");
        }

        public async Task SendContactNotificationAsync(string nome, string email, string telefone, string assunto, string mensagem)
        {
            var message = new MimeMessage();

            // Remetente
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Username));

            // Destinatário (o e-mail do admin)
            message.To.Add(new MailboxAddress("Administrador do Site", _emailSettings.RecipientEmail));

            message.Subject = $"[NOVO CONTATO - OUVIDORIA] {assunto}";

            // Corpo do E-mail em HTML formatado
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <h2 style='color: #1a73e8;'>Nova Mensagem da Ouvidoria</h2>
                        
                        <div style='border: 1px solid #ddd; padding: 15px; border-radius: 8px; background-color: #f7f7f7;'>
                            <p><strong>Nome:</strong> {nome}</p>
                            <p><strong>E-mail do Contato:</strong> <a href='mailto:{email}'>{email}</a></p>
                            <p><strong>Telefone:</strong> {telefone ?? "N/A"}</p>
                            <p><strong>Assunto:</strong> <span style='font-weight: bold; color: #d93025;'>{assunto}</span></p>
                        </div>
                        
                        <h3 style='margin-top: 20px; color: #333;'>Mensagem Detalhada:</h3>
                        <div style='border-left: 4px solid #4CAF50; padding: 10px 15px; background-color: #e8f5e9; margin-bottom: 20px;'>
                            {mensagem.Replace("\n", "<br>")}
                        </div>
                        
                        <p style='font-size: 0.8em; color: #777;'>Mensagem enviada em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                    </body>
                    </html>"
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    // Conexão, autenticação e envio
                    await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERRO NO ENVIO DE E-MAIL (SMTP): {ex.Message}");
                    // Re-lança a exceção para que o Controller a capture e retorne o Status 500
                    throw;
                }
            }
        }
    }
}