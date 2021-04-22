using System.Collections.Generic;

namespace COGTIVE.Domain.Apontamentos.Models
{
    public class ProducaoModel
    {
        public int QuantidadeTotal { get; set; }
        public IList<Producao> Top3Lotes { get; set; }
    }
}
