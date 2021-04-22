using System;

namespace COGTIVE.Domain.Apontamentos
{
    public class Producao : Apontamento
    {
        public Evento Evento { get; set; }
        public string NumeroLote { get; set; }
        public int Quantidade { get; set; }

        public Producao(int idApontamento, DateTime dataInicio, DateTime dataFim,
                        int idEvento, TipoEvento tipoEvento, string descricao, 
                        string numeroLote, int quantidade) : base(idApontamento, dataInicio, dataFim)
        {
            this.Evento = new Evento(idEvento: idEvento, tipo: tipoEvento, descricao: descricao);
            this.NumeroLote = numeroLote;
            this.Quantidade = quantidade;

            this.DataInclusao = DateTime.Now;
        }

        public Producao(Evento evento, string numeroLote, int quantidade)
        {
            this.Evento = evento;
            this.NumeroLote = numeroLote;
            this.Quantidade = quantidade;

            this.DataInclusao = DateTime.Now;
        }

        public Producao() {
            this.Evento = new Evento();
            this.DataInclusao = DateTime.Now;
        }
    }
}
