using System.Collections.Generic;
using System.Linq;
using COGTIVE.Domain.Apontamentos;

namespace COGTIVE.Repository
{
    public class ApontamentoRepository: IApontamentoRepository
    {
        private IList<IApontamento> _list { get; set; }

        public ApontamentoRepository()
        {
            this._list = new List<IApontamento>();
        }

        public void Add(IApontamento dto)
        {
            this._list.Add(dto);
        }

        public void Update(IApontamento dto)
        {
            IApontamento apontamento = this.GetId((dto as Apontamento).IdApontamento.Value);
            if(apontamento != null)
            {
                apontamento = dto;
            }
        }

        public IApontamento GetId(int id)
        {
            return this._list.FirstOrDefault(a => (a as Apontamento).IdApontamento.Equals(id));
        }

        public IApontamento Get(IApontamento dto)
        {
            return this._list.FirstOrDefault(a => (a as Apontamento).IdApontamento.Equals((dto as Apontamento).IdApontamento));
        }

        public IList<IApontamento> GetAll()
        {
            return this._list;
        }

        public IList<Apontamento> GetAllApontamentos()
        {
            return this.GetAll().Cast<Apontamento>().ToList();
        }

        public IList<Producao> GetAllProducao()
        {
            return this._list.Where(a => a.GetType() == typeof(Producao)).Cast<Producao>().ToList();
        }

        public IList<Manutencao> GetAllManutencao()
        {
            return this._list.Where(a => a.GetType() == typeof(Manutencao)).Cast<Manutencao>().ToList();
        }
    }
}
