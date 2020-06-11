namespace Domain.Modulos.Infracao.Natureza
{
    public class NaturezaModel
    { 
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Ponto { get; private set; }
        public float Valor { get; private set; }
        public float PercentualDeDesconto { get; private set; }

        public NaturezaModel(string nome, int ponto, float valor, float percentualDeDesconto)
        {
            Nome = nome;
            Ponto = ponto;
            Valor = valor;
            PercentualDeDesconto = percentualDeDesconto;
        }

        public void Atualizar(string nome, int ponto, float valor, float percentualDeDesconto)
        {
            Nome = nome;
            Ponto = ponto;
            Valor = valor;
            PercentualDeDesconto = percentualDeDesconto;
        }

    }
}