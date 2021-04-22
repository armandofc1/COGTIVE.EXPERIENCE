using System.Collections.Generic;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Domain.Apontamentos.Models;

namespace COGTIVE.Service
{
    public interface IApontamentoService
    {
        public void LoadApontamento();
        IList<Producao> GetAllProducao();
        public IList<Manutencao> GetAllManutencao();
        public IList<Apontamento> GetAllApontamentos();
        public IList<GAP> FindGAPs(IList<Apontamento> listApontamentos);
        public GAPModel GetGAPs();
        public ProducaoModel GetProducao();
        public ManutencaoModel GetManutencao();

    }
}
