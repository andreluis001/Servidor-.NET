namespace ServidorProjeto.Models
{
    public class Doacao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public double Unite_price { get; set; }
        public string description { get; set; }
        public int categoryID { get; set; }
    }
}
