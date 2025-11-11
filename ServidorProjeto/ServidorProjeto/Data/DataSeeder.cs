using BCrypt.Net;
using ServidorProjeto.Models;

namespace ServidorProjeto.Data
{
    public static class DataSeeder
    {
        public static void SeedAdmin(SistemaBD context)
        {
            // Verifica se já existe um admin
            if (!context.Usuarios.Any(u => u.Email == "admin@alma.com"))
            {
                var admin = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@alma.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                    IsAdm = true
                };

                context.Usuarios.Add(admin);
                context.SaveChanges();
                Console.WriteLine("✅ Usuário administrador criado com sucesso!");
            }
            else
            {
                Console.WriteLine("ℹ️ Usuário administrador já existe.");
            }
        }
    }
}
