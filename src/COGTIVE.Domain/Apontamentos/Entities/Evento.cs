namespace COGTIVE.Domain.Apontamentos
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public TipoEvento Tipo { get; set; }
        public string Descricao{ get; set; }

        public Evento(int idEvento, TipoEvento tipo, string descricao)
        {
            this.IdEvento = idEvento;
            this.Tipo = tipo;
            this.Descricao = descricao;
        }

        public Evento() { }

    }
}
