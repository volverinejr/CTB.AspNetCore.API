using Domain.Modulos.TaxaSelic.Command;
using Xunit;

namespace Tests.Modulos
{
    public class TaxaSelicTest
    {
        [Fact(DisplayName = "Taxa Selic: Insert Inválido")]
        public void InsertCommand_Invalido()
        {
            var command = new TaxaSelicInsertCommand(2000, 1, 3);
            command.Validate();
            var validacaoAno = command.Invalid;


            command = new TaxaSelicInsertCommand(2017, 13, 3);
            command.Validate();
            var validacaoMes = command.Invalid;


            command = new TaxaSelicInsertCommand(2017, 10, -1);
            command.Validate();
            var validacaoValor = command.Invalid;


            Assert.True(validacaoAno);
            Assert.True(validacaoMes);
            Assert.True(validacaoValor);
        }



        [Fact(DisplayName = "Taxa Selic: Insert Válido")]
        public void InsertCommand_Valido()
        {
            var command = new TaxaSelicInsertCommand(2016, 1, 3);
            command.Validate();
            var validacao = command.Invalid;

            Assert.False(validacao);
        }


        [Fact(DisplayName = "Taxa Selic: Update Inválido")]
        public void UpdateCommand_Invalido()
        {
            var command = new TaxaSelicUpdateCommand(0, 2000, 1, 3);
            command.Validate();
            var validacaoId = command.Invalid;

            command = new TaxaSelicUpdateCommand(1, 2000, 1, 3);
            command.Validate();
            var validacaoAno = command.Invalid;

            command = new TaxaSelicUpdateCommand(1, 2017, 13, 3);
            command.Validate();
            var validacaoMes = command.Invalid;

            command = new TaxaSelicUpdateCommand(1, 2017, 13, 0);
            command.Validate();
            var validacaoValor = command.Invalid;


            Assert.True(validacaoId);
            Assert.True(validacaoAno);
            Assert.True(validacaoMes);
            Assert.True(validacaoValor);
        }


        [Fact(DisplayName = "Taxa Selic: Update Válido")]
        public void UpdateCommand_valido()
        {
            var taxaSelicUpdateCommand = new TaxaSelicUpdateCommand(1, 2017, 1, 3);
            taxaSelicUpdateCommand.Validate();
            var validacao = taxaSelicUpdateCommand.Invalid;

            Assert.False(validacao);
        }

    }
}