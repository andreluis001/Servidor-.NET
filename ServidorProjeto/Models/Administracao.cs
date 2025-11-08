using ServidorProjeto.Models;
using System.Runtime.CompilerServices;

namespace ServidorProjeto
{
    public class Administracao : Usuario
    {
        private int SenhaAdministrador;
        public Administracao()
        {
            IsAdm = true;
            SenhaAdministrador = 0;
        }
        public int GetSenhaAdministrador()
        {
            return SenhaAdministrador;
        }
        public void SetSenhaAdm(int novaSenhaAdm)
        {
            SenhaAdministrador = novaSenhaAdm;
        }
    }
}