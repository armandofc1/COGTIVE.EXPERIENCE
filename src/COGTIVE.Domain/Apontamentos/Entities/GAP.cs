using System;

namespace COGTIVE.Domain.Apontamentos
{
    public class GAP
    {
        public Intervalo Intervalo { get; set; }

        public GAP(DateTime dataInicio, DateTime dataFim)
        {
            this.Intervalo = new Intervalo(dataInicio: dataInicio, dataFim: dataFim);
        }

        public GAP()
        {
            this.Intervalo = new Intervalo();
        }
    }
}
