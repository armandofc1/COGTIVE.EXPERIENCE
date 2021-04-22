using System;
using System.IO;
using COGTIVE.Repository;
using COGTIVE.Domain.Apontamentos;

namespace COGTIVE.Crosscutting.Utils
{
    public class Data
    {
        public static ApontamentoRepository Load(ApontamentoRepository repository)
        {
            try
            {
                string sfileName = Data.UrlFileData();
                using (StreamReader sr = new StreamReader(sfileName))
                {
                    do
                    {
                        string line = sr.ReadLine();
                        string[] arrData = line.Split(';');
                        IApontamento apontamento = Data.FactoryApontamento(arrData);
                        if (apontamento != null)
                        {
                            repository.Add(apontamento);
                        }
                    } while (!(sr.EndOfStream));
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return repository;
        }

        public static string UrlFileData()
        {
            string relative = @"..\..\..\..\..\data\data.csv";
            string absolute = System.IO.Path.GetFullPath(relative);
            return absolute;
        }

        public static IApontamento? FactoryApontamento(string[] arrData)
        {
            if (arrData != null && arrData.Length > 0)
            {
                if(arrData[4] == "1" || arrData[4] == "2")
                {
                    return CreateProducao(arrData);
                }

                if (arrData[4] == "19")
                {
                    return CreateManutencao(arrData);
                }
            }
            return null;
        }

        public static IApontamento CreateProducao(string[] arrData)
        {
            int idApontamento = Convert.ToInt32(arrData[0]);
            DateTime dataInicio = Convert.ToDateTime(arrData[1]);
            DateTime dataFim = Convert.ToDateTime(arrData[2]);
            string numeroLote = arrData[3];
            int idEvento = Convert.ToInt32(arrData[4]);
            TipoEvento tipoEvento = TipoEvento.Producao;
            string descricao = "Produção";
            int quantidade = Convert.ToInt32(arrData[5]);

            IApontamento producao = new Producao(idApontamento: idApontamento,
                                             dataInicio: dataInicio,
                                             dataFim: dataFim,
                                             idEvento: idEvento,
                                             tipoEvento: tipoEvento,
                                             descricao: descricao,
                                             numeroLote: numeroLote,
                                             quantidade: quantidade);
            return producao;
        }

        public static IApontamento CreateManutencao(string[] arrData)
        {
            int idApontamento = Convert.ToInt32(arrData[0]);
            DateTime dataInicio = Convert.ToDateTime(arrData[1]);
            DateTime dataFim = dataFim = Convert.ToDateTime(arrData[2]);
            int idEvento = Convert.ToInt32(arrData[4]);
            TipoEvento tipoEvento = TipoEvento.Manutencao;
            string descricao = "Manutenção";

            IApontamento manutencao = new Manutencao(idApontamento: idApontamento,
                                                   dataInicio: dataInicio,
                                                   dataFim: dataFim,
                                                   idEvento: idEvento,
                                                   tipoEvento: tipoEvento,
                                                   descricao: descricao);
            return manutencao;
        }
    }
}
