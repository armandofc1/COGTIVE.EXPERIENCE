using System.Collections.Generic;
using COGTIVE.Domain.Apontamentos;


namespace COGTIVE.Repository
{
    interface IApontamentoRepository
    {
        public void Add(IApontamento dto);
        public void Update(IApontamento dto);
        public IApontamento GetId(int id);
        public IApontamento Get(IApontamento dto);
        public IList<IApontamento> GetAll();
        public IList<Apontamento> GetAllApontamentos();
        public IList<Producao> GetAllProducao();
        public IList<Manutencao> GetAllManutencao();
    }
}
