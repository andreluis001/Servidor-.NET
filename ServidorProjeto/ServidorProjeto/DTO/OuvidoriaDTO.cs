// ServidorProjeto/DTOs/OuvidoriaDTO.cs

using System.ComponentModel.DataAnnotations;

namespace ServidorProjeto.DTOs
{
    public class OuvidoriaDTO
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O assunto é obrigatório.")]
        [RegularExpression("^(Seja um doador|Quero ser voluntário|Parceria|Quero informações|Outro assunto)$",
            ErrorMessage = "O assunto deve ser um dos valores predefinidos.")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        // CORREÇÃO: O parâmetro é 'ErrorMessage'
        [StringLength(1000, ErrorMessage = "A mensagem deve ter no máximo 1000 caracteres.")]
        public string Mensagem { get; set; }
    }
}