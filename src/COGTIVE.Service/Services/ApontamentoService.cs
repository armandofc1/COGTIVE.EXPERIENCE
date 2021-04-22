using System;
using System.Linq;
using System.Collections.Generic;
using COGTIVE.Crosscutting.Utils;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Domain.Apontamentos.Models;
using COGTIVE.Repository;
using System.Text;

namespace COGTIVE.Service
{
    public static class ApontamentoService
    {
        public static ApontamentoRepository _repository { get; set; }

        public static void LoadApontamento()
        {
            try
            {
                if (_repository == null)
                {
                    ApontamentoService._repository = Data.Load();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<Producao> GetAllProducao()
        {
            try
            {
                ApontamentoService.LoadApontamento();
                return _repository.GetAllProducao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<Manutencao> GetAllManutencao()
        {
            try
            {
                ApontamentoService.LoadApontamento();
                return _repository.GetAllManutencao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<Apontamento> GetAllApontamentos()
        {
            try
            {
                ApontamentoService.LoadApontamento();
                return _repository.GetAllApontamentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GAPModel GetGAP()
        {
            try
            {
                GAPModel gapModel = new GAPModel();
                IList<Apontamento> listApontamentos = ApontamentoService.GetAllApontamentos().OrderBy(a => a.Intervalo.DataInicio.Value).ToList();
                IList<GAP> listGAP = new List<GAP>();
                string sPeriodoTotal = string.Empty;
                TimeSpan periodoTotal = new TimeSpan();
                if (listApontamentos != null && listApontamentos.Count > 0)
                {
                    for(int i = 0; i < listApontamentos.Count; i++)
                    {
                        DateTime dataInicio;
                        DateTime dataFim;

                        dataInicio = listApontamentos[i].Intervalo.DataFim.Value;
                        if(!listApontamentos.ToList().Exists(a =>
                                                            a.Intervalo.DataInicio.HasValue
                                                            && (a.Intervalo.DataInicio.Equals(dataInicio)))
                            && listApontamentos.Count > (i + 1))
                        {
                            dataFim = listApontamentos[i + 1].Intervalo.DataInicio.Value;
                            if (dataFim > dataInicio) {
                                GAP gap = new GAP(dataInicio: dataInicio, dataFim: dataFim);
                                listGAP.Add(gap);

                                if (gap.Intervalo.Duracao() != null)
                                {
                                    periodoTotal = periodoTotal.Add((TimeSpan)gap.Intervalo.Duracao());
                                }
                            }
                        }
                    }
                }
                gapModel.QuantidadeTotal = listGAP.Count;

                if (periodoTotal.Days > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(((periodoTotal.Days * 24) + periodoTotal.Hours).ToString());
                    sb.Append(":");
                    sb.Append(periodoTotal.Minutes.ToString());
                    sb.Append(":");
                    sb.Append(periodoTotal.Seconds.ToString());
                    sPeriodoTotal = sb.ToString();
                }
                else
                {
                    sPeriodoTotal = periodoTotal.ToString("hh\\:mm\\:ss");
                }
                gapModel.PeriodoTotal = sPeriodoTotal;

                return gapModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProducaoModel GetProducao()
        {
            try
            {
                ProducaoModel producao = new ProducaoModel();
                IList<Producao> listProducao = ApontamentoService.GetAllProducao();
                if (listProducao != null && listProducao.Count > 0)
                {
                    producao.QuantidadeTotal = listProducao.Sum(p => p.Quantidade);
                    producao.Top3Lotes = listProducao.GroupBy(g => g.NumeroLote)
                                                      .Select(p => new Producao()
                                                      {
                                                          NumeroLote = p.FirstOrDefault().NumeroLote,
                                                          Quantidade = p.Sum(q => q.Quantidade)
                                                      })
                                                      .OrderByDescending(p => p.Quantidade)
                                                      .Take(3).ToList();
                }
                return producao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ManutencaoModel GetManutencao()
        {
            try
            {
                ManutencaoModel manutencao = new ManutencaoModel();
                IList<Manutencao> listManutecao = ApontamentoService.GetAllManutencao();
                if (listManutecao != null && listManutecao.Count > 0)
                {
                    string sPeriodoTotal = string.Empty;
                    TimeSpan periodoTotal = new TimeSpan();
                    listManutecao.ToList().ForEach(delegate (Manutencao mnt)
                    {
                        if (mnt.Intervalo.Duracao() != null) {
                            periodoTotal = periodoTotal.Add((TimeSpan)mnt.Intervalo.Duracao());
                        }
                    });

                    if(periodoTotal.Days > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(((periodoTotal.Days * 24) + periodoTotal.Hours).ToString());
                        sb.Append(":");
                        sb.Append(periodoTotal.Minutes.ToString());
                        sb.Append(":");
                        sb.Append(periodoTotal.Seconds.ToString());
                        sPeriodoTotal = sb.ToString();
                    }
                    else
                    {
                        sPeriodoTotal = periodoTotal.ToString("hh\\:mm\\:ss");
                    }

                    manutencao.PeriodoTotal = sPeriodoTotal;
                }
                return manutencao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
