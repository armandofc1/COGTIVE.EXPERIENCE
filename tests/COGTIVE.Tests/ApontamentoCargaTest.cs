using Xunit;
using COGTIVE.Crosscutting.Utils;
using COGTIVE.Domain.Apontamentos;

namespace COGTIVE.Tests
{
    public class ApontamentoCargaTest
    {
        [Fact]
        public void Quando_IdEventoFor1ou2_Entao_Deve_CriarUmApontamentoDeProducao()
        {
            string line = "4509;01/03/2018 13:00:00;01/03/2018 14:00:00;18020155;1;9";
            string[] arrData = line.Split(';');
            IApontamento apontamento = Data.FactoryApontamento(arrData);
            Assert.True(apontamento.GetType().Equals(typeof(Producao)));
        }

        [Fact]
        public void Quando_IdEventoFor19_Entao_Deve_CriarUm_Apontamento_de_Manutencao()
        {
            string line = "4514;01/03/2018 14:00:00;01/03/2018 22:00:00;;19;14";
            string[] arrData = line.Split(';');
            IApontamento apontamento = Data.FactoryApontamento(arrData);
            Assert.True(apontamento.GetType().Equals(typeof(Manutencao)));
        }

        [Fact]
        public void Quando_IdEventoForDiferenteDe1ou2ou19_Entao_NaoDeve_CriarNenhumApontamento()
        {
            string line = "13528;07/03/2018 13:30:00;07/03/2018 14:00:00;;22;52";
            string[] arrData = line.Split(';');
            IApontamento apontamento = Data.FactoryApontamento(arrData);
            Assert.Null(apontamento);
        }
    }
}
