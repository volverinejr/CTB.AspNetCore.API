namespace Domain.Modulos.TaxaSelic
{
    public class TaxaSelicResult
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public float Valor { get; set; }

        public TaxaSelicResult(int id, int ano, int mes, float valor)
        {
            Id = id;
            Ano = ano;
            Mes = mes;
            Valor = valor;
        }
        
    }
}