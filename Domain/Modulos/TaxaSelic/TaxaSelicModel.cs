
namespace Domain.Modulos.TaxaSelic
{
    public class TaxaSelicModel
    {
        public int Id { get; private set; }
        public int Ano { get; private set; }
        public int Mes { get; private set; }
        public float Valor { get; private set; }

        public TaxaSelicModel(int ano, int mes, float valor)
        {
            Ano = ano;
            Mes = mes;
            Valor = valor;
        }

        public void Atualizar(int ano, int mes, float valor)
        {
            Ano = ano;
            Mes = mes;
            Valor = valor;
        }

    }
}