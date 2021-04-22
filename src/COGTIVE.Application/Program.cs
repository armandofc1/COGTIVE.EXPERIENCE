using System;
using COGTIVE.Domain.Apontamentos;
using COGTIVE.Domain.Apontamentos.Models;
using COGTIVE.Service;

namespace COGTIVE.Application
{
    public class Program
    {
        private static string tracos = "-------------------------------------------------------------------";

        public static void Main(string[] args)
        {
            Console.WriteLine("COGTIVEXPERIENCE");
            Console.WriteLine(tracos);

            try
            {
                Funcionalidade1();
                Funcionalidade2();
                Funcionalidade3();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro!", ex.Message);
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine(tracos);
            Console.ReadLine();
        }

        private static void Funcionalidade1()
        {
            try
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("1. Funcionalidade Calcular GAPs");
                Console.WriteLine(tracos);
                Console.Write(Environment.NewLine);

                GAPModel gap = ApontamentoService.GetGAP();
                Console.WriteLine("Quantidade de GAPs: {0}", gap.QuantidadeTotal);
                Console.WriteLine("Período Total: {0}", gap.PeriodoTotal);
            }
            catch (Exception ex)
            {
                Exception erro = new Exception(string.Format("{0}Na Funcionalidade 1: {1}", Environment.NewLine, ex.Message));
                throw erro;
            }
        }

        private static void Funcionalidade2()
        {
            try { 
                Console.Write(Environment.NewLine);
                Console.WriteLine("2. Funcionalidade Calcular Quantidades Produzidas");
                Console.WriteLine(tracos);
                Console.Write(Environment.NewLine);

                ProducaoModel producao = ApontamentoService.GetProducao();
                Console.WriteLine("Quantidade Total Produzida: {0}", producao.QuantidadeTotal);

                if(producao.Top3Lotes != null && producao.Top3Lotes.Count > 0)
                {
                    int colocacao = 0;
                    foreach (Producao prod in producao.Top3Lotes)
                    {
                        colocacao++;
                        Console.WriteLine("{0}º Lote {1} produziu {2}", colocacao, prod.NumeroLote, prod.Quantidade);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception erro = new Exception(string.Format("{0}Na Funcionalidade 2: {1}", Environment.NewLine, ex.Message));
                throw erro;
            }
        }

        private static void Funcionalidade3()
        {
            try
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("3. Funcionalidade Calcular Horas de Manutenção");
                Console.WriteLine(tracos);
                Console.Write(Environment.NewLine);

                ManutencaoModel manutencao = ApontamentoService.GetManutencao();
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
