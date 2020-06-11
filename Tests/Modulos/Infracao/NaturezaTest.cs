using Domain.Modulos.Infracao.Natureza.Command;
using Xunit;

namespace Tests.Modulos.Infracao
{
    public class NaturezaTest
    {

        [Fact(DisplayName = "Natureza: Insert Inválido")]
        public void InsertCommand_Invalido()
        {
            var command = new NaturezaInsertCommand("Lev", 1, 10, 10);
            command.Validate();
            var validacaoNome = command.Invalid;

            command = new NaturezaInsertCommand("Leve", 8, 10, 10);
            command.Validate();
            var validacaoPonto = command.Invalid;

            command = new NaturezaInsertCommand("Leve", 7, 0, 10);
            command.Validate();
            var validacaoValor = command.Invalid;

            command = new NaturezaInsertCommand("Leve", 7, 10, 35);
            command.Validate();
            var validacaoPercentual = command.Invalid;

            Assert.True(validacaoNome);
            Assert.True(validacaoPonto);
            Assert.True(validacaoValor);
            Assert.True(validacaoPercentual);
        }


        [Fact(DisplayName = "Natureza: Insert Válido")]
        public void InsertCommand_Valido()
        {
            var command = new NaturezaInsertCommand("Leve", 1, 1500, 20);
            command.Validate();
            var validacao = command.Invalid;

            Assert.False(validacao);
        }



        [Fact(DisplayName = "Natureza: Update Inválido")]
        public void UpdateCommand_Invalido()
        {
            var command = new NaturezaUpdateCommand(0, "Leve", 1, 10, 10);
            command.Validate();
            var validacaoNome = command.Invalid;

            command = new NaturezaUpdateCommand(1, "Leve", 8, 10, 10);
            command.Validate();
            var validacaoPonto = command.Invalid;

            command = new NaturezaUpdateCommand(1, "Leve", 7, 0, 10);
            command.Validate();
            var validacaoValor = command.Invalid;

            command = new NaturezaUpdateCommand(1, "Leve", 7, 10, 35);
            command.Validate();
            var validacaoPercentual = command.Invalid;

            Assert.True(validacaoNome);
            Assert.True(validacaoPonto);
            Assert.True(validacaoValor);
            Assert.True(validacaoPercentual);
        }


        [Fact(DisplayName = "Natureza: Update Válido")]
        public void UpdateCommand_valido()
        {
            var command = new NaturezaUpdateCommand(1, "Leve", 1, 1500, 20);
            command.Validate();
            var validacao = command.Invalid;

            Assert.False(validacao);
        }

    }
}