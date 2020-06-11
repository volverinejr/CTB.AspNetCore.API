namespace Domain.Modulos.Infracao.Natureza
{
    public class NaturezaResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ponto { get; set; }
        public float Valor { get; set; }
        public float PercentualDeDesconto { get; set; }

        public NaturezaResult(int id, string nome, int ponto, float valor, float percentualDeDesconto)
        {
            Id = id;
            Nome = nome;
            Ponto = ponto;
            Valor = valor;
            PercentualDeDesconto = percentualDeDesconto;
        }


    }
}