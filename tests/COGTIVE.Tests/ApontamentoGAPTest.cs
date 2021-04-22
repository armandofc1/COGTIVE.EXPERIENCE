using Xunit;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Crosscutting.Utils;
using COGTIVE.Service;
using COGTIVE.Repository;
using System.Collections.Generic;
using System.Linq;

namespace COGTIVE.Tests
{
    public class ApontamentoGAPTest
    {
        private IList<IApontamento> GetApontamentosGAPs()
        {
            string line;
            string[] arrData;
            IList<IApontamento> lista = new List<IApontamento>();

            line = "4509;01/03/2018 13:00:00;01/03/2018 14:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            line = "4509;01/03/2018 16:00:00;01/03/2018 17:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            line = "4509;01/03/2018 18:00:00;01/03/2018 19:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            return lista;
        }

        private IList<IApontamento> GetApontamentosNoGAPs()
        {
            string line;
            string[] arrData;
            IList<IApontamento> lista = new List<IApontamento>();

            line = "4509;01/03/2018 13:00:00;01/03/2018 14:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            line = "4509;01/03/2018 14:00:00;01/03/2018 15:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            line = "4509;01/03/2018 15:00:00;01/03/2018 16:00:00;18020155;1;9";
            arrData = line.Split(';');
            lista.Add(Data.FactoryApontamento(arrData));

            return lista;
        }

        [Fact]
        public void Quando_ExisteIntervalosNosApontamentos_Entao_Deve_EncontrarGAPs()
        {

            IList<IApontamento> lista = GetApontamentosGAPs();
            ApontamentoRepository repository = new ApontamentoRepository(lista);
            ApontamentoService service = new ApontamentoService(repository);
            IList<Apontamento> apontamentos = lista.Cast<Apontamento>().ToList();
            IList<GAP> gaps = service.FindGAPs(apontamentos);
            Assert.True(gaps.Count > 0);
        }

        [Fact]
        public void Quando_NaoExisteIntervalosNosApontamentos_Entao_NaoDeve_EncontrarGAPs()
        {

            IList<IApontamento> lista = GetApontamentosNoGAPs();
            ApontamentoRepository repository = new ApontamentoRepository(lista);
            ApontamentoService service = new ApontamentoService(repository);
            IList<Apontamento> apontamentos = lista.Cast<Apontamento>().ToList();
            IList<GAP> gaps = service.FindGAPs(apontamentos);
            Assert.True(gaps.Count == 0);
        }
    }
}
