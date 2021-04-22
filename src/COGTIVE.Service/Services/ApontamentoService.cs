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
    public class ApontamentoService : IApontamentoService
    {
        public static ApontamentoRepository _repository { get; set; }

        public ApontamentoService()
        {
            this.LoadApontamento();
        }

        public ApontamentoService(ApontamentoRepository repository)
        {
            _repository = repository;
        }

        public void LoadApontamento()
        {
            try
            {
                if (_repository == null)
                {
                    _repository = Data.Load(new ApontamentoRepository());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Producao> GetAllProducao()
        {
            try
            {
                return _repository.GetAllProducao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Manutencao> GetAllManutencao()
        {
            try
            {
                return _repository.GetAllManutencao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Apontamento> GetAllApontamentos()
        {
            try
            {
                return _repository.GetAllApontamentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TimeSpan CalculetePeriodoTotalFromManutencao(IList<Manutencao> listManutecao)
        {
            TimeSpan periodoTotal = new TimeSpan();
            if (listManutecao != null && listManutecao.Count > 0)
            {
                periodoTotal = CalculetePeriodoTotal(listManutecao.Select(mnt => mnt.Intervalo).ToList());
            }
            return periodoTotal;
        }

        private TimeSpan CalculetePeriodoTotalFromGAPs(IList<GAP> listGAPs)
        {
            TimeSpan periodoTotal = new TimeSpan();
            if (listGAPs != null && listGAPs.Count > 0)
            {
                periodoTotal = CalculetePeriodoTotal(listGAPs.Select(gp => gp.Intervalo).ToList());
            }
            return periodoTotal;
        }

        private TimeSpan CalculetePeriodoTotal(List<Intervalo> intervalos)
        {
            TimeSpan periodoTotal = new TimeSpan();
            if (intervalos != null && intervalos.Count > 0)
            {
                intervalos.ForEach(delegate (Intervalo itv)
                {
                    if (itv.Duracao() != null)
                    {
                        periodoTotal = periodoTotal.Add((TimeSpan)itv.Duracao());
                    }
                });
            }
            return periodoTotal;
        }
        private string GetPeriodoTotal(TimeSpan periodoTotal)
        {
            string sPeriodoTotal;

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

            return sPeriodoTotal;
        }

        public IList<GAP> FindGAPs(IList<Apontamento> listApontamentos)
        {
            IList<GAP> listGAP = new List<GAP>();
            if (listApontamentos != null && listApontamentos.Count > 0)
            {
                for (int i = 0; i < listApontamentos.Count; i++)
                {
                    DateTime dataInicio;
                    DateTime dataFim;

                    dataInicio = listApontamentos[i].Intervalo.DataFim.Value;
                    if (!listApontamentos.ToList().Exists(a =>
                                                         a.Intervalo.DataInicio.HasValue
                                                         && (a.Intervalo.DataInicio.Equals(dataInicio)))
                        && listApontamentos.Count > (i + 1))
                    {
                        dataFim = listApontamentos[i + 1].Intervalo.DataInicio.Value;
                        if (dataFim > dataInicio)
                        {
                            GAP gap = new GAP(dataInicio: dataInicio, dataFim: dataFim);
                            listGAP.Add(gap);
                        }
                    }
                }
            }
            return listGAP;
        }

        public GAPModel GetGAPs()
        {
            try
            {
                GAPModel gapModel = new GAPModel();
                IList<Apontamento> listApontamentos = this.GetAllApontamentos().OrderBy(a => a.Intervalo.DataInicio.Value).ToList();
                IList<GAP> listGAPs = FindGAPs(listApontamentos);
                TimeSpan periodoTotal = CalculetePeriodoTotalFromGAPs(listGAPs);
                gapModel.QuantidadeTotal = listGAPs.Count;
                gapModel.PeriodoTotal = GetPeriodoTotal(periodoTotal);
                return gapModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProducaoModel GetProducao()
        {
            try
            {
                ProducaoModel producao = new ProducaoModel();
                IList<Producao> listProducao = this.GetAllProducao();
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

        public ManutencaoModel GetManutencao()
        {
            try
            {
                ManutencaoModel manutencao = new ManutencaoModel();
                IList<Manutencao> listManutecao = this.GetAllManutencao();
                TimeSpan periodoTotal = CalculetePeriodoTotalFromManutencao(listManutecao);
                manutencao.PeriodoTotal = GetPeriodoTotal(periodoTotal);
                return manutencao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
