using ServidorProjeto.Models;

namespace ServidorProjeto
{
    public class TipoAtv
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public TipoAtv()
        {
            Id = 0;
            Nome = "nenhum";
        }

        // Comparando com um ArquivoPdf usando a propriedade Id
        public bool Maior(ArquivoPdf pdf)
        {
            return this.Id > pdf.Id;
        }

        // Comparando com outro TipoAtv
        public bool Maior(TipoAtv atv)
        {
            return this.Id > atv.Id;
        }
    }
}
