namespace Domain.Shared.Entidade
{
    public class Pesquisa
    {
        public int Pagina { get; set; }
        public int Qtd { get; set; }
        public string Campo { get; set; }
        public int Ordem { get; set; }
        public string Filtro { get; set; }

        public Pesquisa(int pagina = 0, int qtd = 10, string campo = "Id", int ordem = 1, string filtro = "")
        {

            if (qtd < 0)
            {
                qtd = qtd * -1;
            }

            if (qtd > 100)
            {
                qtd = 100;
            }

            if (campo == null)
            {
                campo = "id";
            }

            if ((ordem != 1) && (ordem != -1))
            {
                ordem = 1;
            }

            if (filtro == null)
            {
                filtro = "";
            }

            Pagina = pagina;
            Qtd = qtd;
            Campo = campo;
            Ordem = ordem;
            Filtro = filtro;
        }


    }
}