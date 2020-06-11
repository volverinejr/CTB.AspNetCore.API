using Domain.Shared.Contratos;

namespace Domain.Shared.Entidade
{
    public class GenericResult : IResult
    {

        public int Status { get; set; }
        public string Mensagem { get; set; }
        public object Data { get; set; }



        public GenericResult()
        {
        }

        public GenericResult(int status, string mensagem, object data)
        {
            Status = status;
            Mensagem = mensagem;
            Data = data;
        }
    }
}