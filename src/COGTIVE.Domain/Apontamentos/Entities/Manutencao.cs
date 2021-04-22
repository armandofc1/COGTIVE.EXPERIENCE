using System;

namespace COGTIVE.Domain.Apontamentos
{
    public class Manutencao : Apontamento
    {
        public Evento Evento { get; set; }

        public Manutencao(int idApontamento, DateTime dataInicio, DateTime dataFim,
                          int idEvento, TipoEvento tipoEvento, string descricao) : base(idApontamento, dataInicio, dataFim)
        {
            this.Evento = new Evento(idEvento: idEvento, tipo: tipoEvento, descricao: descricao);
            this.DataInclusao = DateTime.Now;
        }

        public Manutencao(Evento evento)
        {
            this.Evento = evento;
            this.DataInclusao = DateTime.Now;
        }

        public Manutencao()
        {
            this.Evento = new Evento();
            this.DataInclusao = DateTime.Now;
        }
    }
}
