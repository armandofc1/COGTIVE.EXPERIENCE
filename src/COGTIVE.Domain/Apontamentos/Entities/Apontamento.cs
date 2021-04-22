using System;

namespace COGTIVE.Domain.Apontamentos
{
    public abstract class Apontamento : IApontamento
    {
        public int? IdApontamento { get; set; }
        public Intervalo Intervalo { get; set; }

        public Apontamento(int idApontamento, DateTime dataInicio, DateTime dataFim)
        {
            this.IdApontamento = idApontamento;
            this.Intervalo = new Intervalo(dataInicio: dataInicio, dataFim: dataFim);
        }

        public Apontamento()
        {
            this.Intervalo = new Intervalo();
        }
    }
}
