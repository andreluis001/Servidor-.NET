// ServidorProjeto/Configurations/EmailSettings.cs

namespace ServidorProjeto.Configurations
{
    // Esta classe mapeia a seção "EmailSettings" no seu arquivo appsettings.json
    public class EmailSettings
    {
        // Corresponde a "SmtpServer" no JSON
        public string SmtpServer { get; set; } = string.Empty;

        // Corresponde a "SmtpPort" no JSON
        public int SmtpPort { get; set; }

        // Corresponde a "Username" no JSON
        public string Username { get; set; } = string.Empty;

        // Corresponde a "Password" no JSON
        public string Password { get; set; } = string.Empty;

        // Corresponde a "SenderName" no JSON
        public string SenderName { get; set; } = "Site Contato Automático";

        // Corresponde a "RecipientEmail" no JSON (Admin)
        public string RecipientEmail { get; set; } = string.Empty;
    }
}