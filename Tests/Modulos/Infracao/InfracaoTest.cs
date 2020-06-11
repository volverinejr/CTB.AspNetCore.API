using System;
using Domain.Modulos.Infracao.Infracao.Command;
using Xunit;


namespace Tests.Modulos.Infracao
{
    public class InfracaoTest
    {
        [Fact(DisplayName = "Infracao: Insert Inválido")]
        public void InsertCommand_Invalido()
        {
            var command = new InfracaoInsertCommand("   ", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoCodigo_Curto = command.Invalid;

            command = new InfracaoInsertCommand("   ", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoCodigo_Longo = command.Invalid;


            Assert.True(validacaoCodigo_Curto);
            Assert.True(validacaoCodigo_Longo);
        }


        [Fact(DisplayName = "Infracao: Insert válido")]
        public void InsertCommand_Valido()
        {
            var command = new InfracaoInsertCommand(
                "60501", "Descrição completa da infração", "Amparo Legal", "Medida Adm", false, false, 
                1, 1, 
                true, false, false, true, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoCodigo = command.Invalid;

            Assert.False(validacaoCodigo);
        }


        [Fact(DisplayName = "Infracao: Update Inválido")]
        public void UpdateCommand_Invalido()
        {
            var command = new InfracaoUpdateCommand(0, "12345", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoId = command.Invalid;

            command = new InfracaoUpdateCommand(1, "1234", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoNomeCurto = command.Invalid;

            command = new InfracaoUpdateCommand(1, "123456", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacaoNomeLongo = command.Invalid;


            Assert.True(validacaoId);
            Assert.True(validacaoNomeCurto);
            Assert.True(validacaoNomeLongo);
        }


        [Fact(DisplayName = "Infracao: Update válido")]
        public void UpdateCommand_Valido()
        {
            var command = new InfracaoUpdateCommand(1, "12345", "descricção", "Amparo Legal", "Medida Adm", false, false, 1, 1, false, false, false, false, false, new DateTime(2000, 1, 1, 0, 0, 0));
            command.Validate();
            var validacao = command.Invalid;

            Assert.False(validacao);
        }

    }
}