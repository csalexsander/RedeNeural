using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RedeNeural
{
    public class Menu
    {
        public static void iniciaAplicacao()
        {
            var op = true;
            var dados = new Dados();
            try
            {

                Arquivo.leArquivoDados(dados);
                Arquivo.leClasse(dados);

                while (op)
                {
                    exibeMenu();

                    var opc = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (opc)
                    {
                        case 1:
                            geraPesos(dados);
                            break;
                        case 2:
                            treinaNeuronio(dados);
                            break;
                        case 3:
                            operaNeuronio(dados);
                            break;
                        case 4:
                            op = false;
                            break;
                    }

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }

        private static void operaNeuronio(Dados dados)
        {
            var i = 0;
            var desempenho = 0.0;

            try
            {
                var tamC = Arquivo.qtdDadosColunas(Arquivo.arquivoDadosTeste);
                var tamL = Arquivo.qtdDadosLinhas(Arquivo.arquivoDadosTeste);
                dados.entrada = new double[tamC];
                verificaArquivosExiste(dados);

                for (i = 0; i < tamL; i++)
                {
                    Arquivo.lePesos(dados);

                    for (var j = 0; j < tamC; j++)
                    {
                       dados.entrada[j] = dados.DadosEntrada[i, j];
                    }
                    desempenho = desempenho + 1;
                    Neuronio.executaCalculo(dados);
                    desempenho = (dados.erro != 0) ? desempenho-- : desempenho++;
                }
                Console.WriteLine((desempenho / i) * 100 + "% de acerto");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void treinaNeuronio(Dados dados)
        {
            Console.WriteLine("Insira a quantidade de epocas desejadas!");
            var epocasDesejadas = double.Parse(Console.ReadLine());
            for (int epocas = 0; epocas < epocasDesejadas; epocas++)
            {
                var desempenho = 0.0;
                var i = 0;

                try
                {
                    var tamC = Arquivo.qtdDadosColunas(Arquivo.arquivoDados);
                    var tamL = Arquivo.qtdDadosLinhas(Arquivo.arquivoDados);
                    dados.entrada = new double[tamC - 1];
                    verificaArquivosExiste(dados);

                    for (i = 0; i < tamL; i++)
                    {
                        Arquivo.lePesos(dados);

                        for (var j = 0; j < tamC; j++)
                        {
                            if (j != 0)
                                dados.entrada[j - 1] = dados.DadosEntrada[i, j];
                            else
                                dados.desejado = dados.DadosEntrada[i, j];
                        }
                        desempenho = desempenho + 1;
                        desempenho = Neuronio.TreinaNeuronio(dados, desempenho);
                    }
                    Console.WriteLine((desempenho / i) * 100 + "% de acerto");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(epocas + 1 + "epocas");
            }
        }

        private static void verificaArquivosExiste(Dados dados)
        {
            if (!Arquivo.verificaseArquivoExiste(Arquivo.arquivoPesos))
                geraPesos(dados);

            if (!Arquivo.verificaseArquivoExiste(Arquivo.arquivoDados))
                throw new Exception("O Arquivo de Dados não foi encontrador no diretorio: " + Arquivo.arquivoDados);
        }

        public static void exibeMenu()
        {
            Console.WriteLine("Selecione Uma das opções abaixo: ");
            Console.WriteLine("1 - Gerar pesos");
            Console.WriteLine("2 - Treinar rede");
            Console.WriteLine("3 - Operar rede");
            Console.WriteLine("4 - Sair");
            Console.Write("--> ");
        }

        public static void geraPesos(Dados dados)
        {
            dados.pesos = new double[Arquivo.qtdDadosColunas(Arquivo.arquivoDados) - 1];
            var rand = new Random();
            for (var i = 0; i < dados.pesos.Length; i++)
            {
                dados.pesos[i] = ((rand.NextDouble()*2) - 1);
            }

            Arquivo.salvarArquivoPesos(dados);
        }
    }
}
