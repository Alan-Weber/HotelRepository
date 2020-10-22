namespace Entities {
    public class Fornecedor
    {
        public int ID { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string NomeContato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
