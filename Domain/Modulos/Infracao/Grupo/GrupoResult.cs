namespace Domain.Modulos.Infracao.Grupo
{
    public class GrupoResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public GrupoResult(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}