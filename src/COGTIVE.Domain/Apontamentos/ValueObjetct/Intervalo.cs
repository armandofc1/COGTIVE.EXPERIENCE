using System;

namespace COGTIVE.Domain.Apontamentos
{
    public class Intervalo : IIntervalo
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public Intervalo(DateTime dataInicio, DateTime dataFim)
        {
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
        }

        public Intervalo() { }

        public TimeSpan? Duracao()
        {
            if (DataFim.HasValue && DataInicio.HasValue && DataFim > DataInicio)
            {
                return DataFim.Value.Subtract(DataInicio.Value);
            }
            return null;
        }
    }
}
