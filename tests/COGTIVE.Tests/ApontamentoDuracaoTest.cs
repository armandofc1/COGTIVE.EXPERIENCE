using Xunit;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Crosscutting.Utils;

namespace COGTIVE.Tests
{
    public class ApontamentoDuracaoTest
    {
        [Fact]
        public void Quando_DataInicio_ForMenorQue_DataFim_Entao_Deve_CalcularADuracao()
        {
            string line = "4509;01/03/2018 13:00:00;01/03/2018 14:00:00;18020155;1;9";
            string[] arrData = line.Split(';');
            Apontamento apontamento = Data.FactoryApontamento(arrData) as Apontamento;       
            Assert.NotNull(apontamento.Intervalo.Duracao());
        }

        [Fact]
        public void Quando_DataInicio_ForMaiorQue_DataFim_Entao_NaoDeve_CalcularADuracao()
        {
            string line = "4509;01/04/2018 13:00:00;01/03/2018 14:00:00;18020155;1;9";
            string[] arrData = line.Split(';');
            Apontamento apontamento = Data.FactoryApontamento(arrData) as Apontamento;
            Assert.Null(apontamento.Intervalo.Duracao());
        }
    }
}
