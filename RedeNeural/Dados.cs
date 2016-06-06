namespace RedeNeural
{
    public class Dados
    {
        public double[] pesos { get; set; }
        public double[,] DadosEntrada { get; set; }
        public double[] entrada { get; set; }
        public double taxa = 0.5;
        public double erro { get; set; }
        public double desejado { get; set; }
        public double resultado { get; set; }
        public int Bias = -1;
        public int[] classes { get; set; }

    }
}