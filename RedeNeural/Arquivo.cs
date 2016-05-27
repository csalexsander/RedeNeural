using System;
using System.Data.Common;
using System.Runtime;

namespace RedeNeural
{
    public class Arquivo
    {
        public static string pastaArquivo = @"C:\Users\Alexsander\Documents\Visual Studio 2015\Projects\RedeNeural\RedeNeural\Arquivos";
        public static readonly string arquivoPesos = pastaArquivo + @"\pesos.txt";
        public static readonly string arquivoDados = pastaArquivo + @"\dados.txt";
        public static readonly string arquivoClasse = pastaArquivo + @"\classes.txt";
        public static readonly string arquivoDadosTeste = pastaArquivo + @"\testes.txt";

        public static void salvarArquivoPesos(Dados dados)
        {

            if (verificaseArquivoExiste(arquivoPesos))
                System.IO.File.Delete(arquivoPesos);

            var linhaPesos = new string[dados.pesos.Length];
            var count = 0;
            foreach (var pesos in dados.pesos)
            {
                linhaPesos[count] = dados.pesos[count].ToString();
                count++;
            }
            System.IO.File.WriteAllLines(arquivoPesos,linhaPesos);
        }

        public static Dados leArquivoDados(Dados dados)
        {
            try
            {
                if (!verificaseArquivoExiste(arquivoDados))
                    throw new Exception("arquivo: "+arquivoDados+" Não existe!");
                
                var lines = System.IO.File.ReadAllLines(arquivoDados);
                var tamL = lines.Length;
                var tamC = lines[0].Split('|').Length;
                dados.DadosEntrada = new double[tamL,tamC];
                var linhas = new string[tamC];

                for (var i = 0; i < tamL; i++)
                {
                    linhas = lines[i].Split('|');
                    for (var j = 0; j < tamC; j++)
                    {
                        dados.DadosEntrada[i, j] = double.Parse(linhas[j]);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

            return dados;
        }

        public static void lePesos(Dados dados)
        {
            try
            {
                if (!verificaseArquivoExiste(arquivoPesos))
                    throw new Exception("Não foi encontrado o arquivo de pesos no caminho: "+arquivoPesos);

                dados.pesos = new double[qtdDadosColunas(arquivoDados) - 1];

                var linha = System.IO.File.ReadAllLines(arquivoPesos);

                for (var i = 0; i < linha.Length; i++)
                {
                    dados.pesos[i] = double.Parse(linha[i]);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public static bool verificaseArquivoExiste(string arquivo)
        {
            return System.IO.File.Exists(arquivo);
        }

        public static void leClasse(Dados dados)
        {
            try
            {
                if (!verificaseArquivoExiste(arquivoClasse))
                    throw new Exception("O arquivo de classes não foi encontrado no diretorio: " +arquivoClasse);

                var linha = System.IO.File.ReadAllLines(arquivoClasse);
                dados.classes = new int[linha.Length];

                for (var i = 0; i < linha.Length; i++)
                {
                    dados.classes[i] = int.Parse(linha[i]);
                }
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message);
            }
        }

        public static int qtdDadosColunas(string arquivo)
        {
            if (!verificaseArquivoExiste(arquivo))
                throw new Exception("Arquivo de dados Não existe!!");

            var line = System.IO.File.ReadAllLines(arquivo);

            return line[0].Split('|').Length;
        }

        public static int qtdDadosLinhas(string arquivo)
        {
            if (!verificaseArquivoExiste(arquivo))
                throw new Exception("Arquivo de dados Não existe!!");

            var line = System.IO.File.ReadAllLines(arquivo);

            return line.Length;
        }

    }
}