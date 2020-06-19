namespace Domain.Shared.Entidade
{
    public class GenericPesquisa
    {
        public object Data { get; set; }
        public int Total { get; set; }

        public GenericPesquisa()
        {
        }

        public GenericPesquisa(object data, int total)
        {
            Data = data;
            Total = total;
        }
    }
}