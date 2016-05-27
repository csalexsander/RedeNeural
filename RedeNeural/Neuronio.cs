
using System;
using System.Text.RegularExpressions;

namespace RedeNeural
{
    public class Neuronio
    {
        public static double TreinaNeuronio(Dados dados, double desempenho)
        {
            executaCalculo(dados);

            if (Math.Abs(dados.erro) > 0.00 || Math.Abs(dados.erro) < 0.00)
            {
                desempenho = desempenho - 1;
                mudaPesos(dados);
            }

            return desempenho;
        }

        public static void executaCalculo(Dados dados)
        {
            double somatoria = 0.0;

            for (var i = 0; i < dados.entrada.Length; i++)
            {
                somatoria += dados.entrada[i]*dados.pesos[i];
            }

            somatoria = somatoria + dados.Bias;

            dados.resultado = somatoria >= 0 ? dados.classes[1] : dados.classes[0];

            dados.erro = dados.desejado - dados.resultado;
        }

        public static void mudaPesos(Dados dados)
        {
            for (int i = 0; i < dados.pesos.Length; i++)
            {
                var soma = dados.taxa * dados.erro * dados.entrada[i];
                dados.pesos[i] = dados.pesos[i] + soma;
            } 

            Arquivo.salvarArquivoPesos(dados);
        }

    }
}
