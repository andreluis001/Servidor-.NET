// ServidorProjeto/Services/IEmailService.cs

namespace ServidorProjeto.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Envia uma notificação por e-mail para o administrador com os dados do contato.
        /// </summary>
        /// <param name="nome">Nome do remetente.</param>
        /// <param name="email">E-mail do remetente.</param>
        /// <param name="telefone">Telefone do remetente.</param>
        /// <param name="assunto">Assunto selecionado na Ouvidoria.</param>
        /// <param name="mensagem">Corpo da mensagem.</param>
        Task SendContactNotificationAsync(string nome, string email, string telefone, string assunto, string mensagem);
    }
}