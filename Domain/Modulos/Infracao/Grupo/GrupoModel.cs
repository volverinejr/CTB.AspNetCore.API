namespace Domain.Modulos.Infracao.Grupo
{
    public class GrupoModel 
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public GrupoModel(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
        
    }
}