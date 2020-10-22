namespace Entities {
    public class Quarto
    {
        public int ID { get; set; }
        public string Numero { get; set; }
        public int Capacidade { get; set; }
        public bool IsOcupado { get; set; }
        public double Preco { get; set; }
        public int ClienteId { get; set; }
        public bool EstaOcupado { get; set; }
    }
}
