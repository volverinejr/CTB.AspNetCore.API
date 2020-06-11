using Domain.Modulos.Infracao.Grupo.Command;
using Xunit;


namespace Tests.Modulos.Infracao
{
    public class GrupoTest
    {
        [Fact(DisplayName = "Grupo: Insert Inválido")]
        public void InsertCommand_Invalido()
        {
            var command = new GrupoInsertCommand("   ");
            command.Validate();
            var validacaoNomeCurto = command.Invalid;

            command = new GrupoInsertCommand("123456789012345678901234567890123456789012345678901");
            command.Validate();
            var validacaoNomeLongo = command.Invalid;

            Assert.True(validacaoNomeCurto);
            Assert.True(validacaoNomeLongo);
        }


        [Fact(DisplayName = "Grupo: Insert válido")]
        public void InsertCommand_Valido()
        {
            var command = new GrupoInsertCommand("1234");
            command.Validate();
            var validacaoNome = command.Invalid;

            Assert.False(validacaoNome);
        }


        [Fact(DisplayName = "Grupo: Update Inválido")]
        public void UpdateCommand_Invalido()
        {
            var command = new GrupoUpdateCommand(0, "Leve");
            command.Validate();
            var validacaoId = command.Invalid;

            command = new GrupoUpdateCommand(1, "123");
            command.Validate();
            var validacaoNomeCurto = command.Invalid;

            command = new GrupoUpdateCommand(1, "12345678901234567890123456789012345678901234567890-");
            command.Validate();
            var validacaoNomeLongo = command.Invalid;

            Assert.True(validacaoId);
            Assert.True(validacaoNomeCurto);
            Assert.True(validacaoNomeLongo);
        }


        [Fact(DisplayName = "Grupo: Update válido")]
        public void UpdateCommand_Valido()
        {
            var command = new GrupoUpdateCommand(1, "Telefone Celular");
            command.Validate();
            var validacao = command.Invalid;

            Assert.False(validacao);
        }

    }
}