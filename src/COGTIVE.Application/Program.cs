using System;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Domain.Apontamentos.Models;
using COGTIVE.Service;

namespace COGTIVE.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Title("COGTIVEXPERIENCE");
            try
            {
                IApontamentoService service = new ApontamentoService();
                FeatureGAPs(service);
                FeatureProducao(service);
                FeatureManutecao(service);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro!", ex.Message);
            }
            Console.ReadLine();
        }

        private static void Title(string descricao)
        {
            string tracos = "-------------------------------------------------------------------";

            Console.Write(Environment.NewLine);
            Console.WriteLine(descricao);
            Console.WriteLine(tracos);
            Console.Write(Environment.NewLine);
        }

        private static void FeatureGAPs(IApontamentoService service)
        {
            try
            {
                Title("1. Funcionalidade Calcular GAPs");

                GAPModel gap = service.GetGAPs();
                Console.WriteLine("Quantidade de GAPs: {0}", gap.QuantidadeTotal);
                Console.WriteLine("Período Total: {0}", gap.PeriodoTotal);
            }
            catch (Exception ex)
            {
                Exception erro = new Exception(string.Format("{0}Na Funcionalidade 1: {1}", Environment.NewLine, ex.Message));
                throw erro;
            }
        }

        private static void FeatureProducao(IApontamentoService service)
        {
            try {
                Title("2. Funcionalidade Calcular Quantidades Produzidas");

                ProducaoModel producao = service.GetProducao();
                Console.WriteLine("Quantidade Total Produzida: {0}", producao.QuantidadeTotal);

                if(producao.Top3Lotes != null && producao.Top3Lotes.Count > 0)
                {
                    int place = 0;
                    foreach (Producao prod in producao.Top3Lotes)
                    {
                        place++;
                        Console.WriteLine("{0}º Lote {1} produziu {2}", place, prod.NumeroLote, prod.Quantidade);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception erro = new Exception(string.Format("{0}Na Funcionalidade 2: {1}", Environment.NewLine, ex.Message));
                throw erro;
            }
        }

        private static void FeatureManutecao(IApontamentoService service)
        {
            try
            {
                Title("3. Funcionalidade Calcular Horas de Manutenção");

                ManutencaoModel manutencao = service.GetManutencao();
                Console.WriteLine("Período Total De Manutenção: {0}", manutencao.PeriodoTotal);
            }
            catch (Exception ex)
            {
                Exception erro = new Exception(string.Format("{0}Na Funcionalidade 3: {1}", Environment.NewLine, ex.Message));
                throw erro;
            }
        }
    }
}
