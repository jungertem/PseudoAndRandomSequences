using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class UniformityCriterion : IGeneratorTest
    {
        private string GeneratorName;

        private const double alpha1 = 0.01;
        private const double alpha2 = 0.05;
        private const double alpha3 = 0.1;

        private const double quantile1 = 2.326;
        private const double quantile2 = 1.645;
        private const double quantile3 = 1.282;

        private readonly double chiSquare1;
        private readonly double chiSquare2;
        private readonly double chiSquare3;

        private readonly int r;
        private readonly int n;

        private double ChiSquarePractical;
        private double sequenceLength;

        private List<List<byte>> subsequences;
        private List<byte> consideredSequence;
        Dictionary<byte, int> byteMap;


        public UniformityCriterion(string generatorName, List<byte> byteSequence, int rParameter)
        {
            GeneratorName = generatorName;
            sequenceLength = byteSequence.Count;

            r = rParameter;
            n = byteSequence.Count / r * r;

            double l = 255 * (r - 1);
            chiSquare1 = Math.Sqrt(2 * l) * quantile1 + l;
            chiSquare2 = Math.Sqrt(2 * l) * quantile2 + l;
            chiSquare3 = Math.Sqrt(2 * l) * quantile3 + l;

            consideredSequence = byteSequence.GetRange(0, n);
            subsequences = FillSubsecuences(byteSequence);
            byteMap = BytesCount(consideredSequence);
        }
        private void CalculatingChiSquarePractical()
        {
            int mShtrih = n / r;

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= r - 1; j++)
                {
                    int x = ByteCountInSection((byte)i, j);
                    ChiSquarePractical += x * x / (double)(byteMap[(byte)i] * mShtrih);
                }
            }

            ChiSquarePractical = (ChiSquarePractical - 1) * n;
        }
        private int ByteCountInSection(byte checkByte, int section)
        {
            int count = 0;
            foreach (byte item in subsequences[section])
            {
                if (item == checkByte)
                    count++;
            }

            return count;
        }
        public void ShowStatistics()
        {
            CalculatingChiSquarePractical();

            Console.WriteLine("\n\n" + new string('-', 70));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\tGENERATOR GeneratorName: " + GeneratorName);
            Console.ResetColor();

            Console.WriteLine($"\nalpha = {alpha1} quantile = {quantile1}");
            Console.WriteLine($"alpha = {alpha2} quantile = {quantile2}");
            Console.WriteLine($"alpha = {alpha3}  quantile = {quantile3} \n");

            Console.WriteLine($"ChiSquarePractical theoretical value: {chiSquare1}");
            Console.WriteLine($"ChiSquarePractical theoretical value: {chiSquare2} ");
            Console.WriteLine($"ChiSquarePractical theoretical value: {chiSquare3} ");

            Console.WriteLine($"\nSequence length (in bytes): {sequenceLength}");
            Console.WriteLine($"\nChiSquare practical value: {ChiSquarePractical}");

            Console.WriteLine($"Compliance with the condition1 : {ChiSquarePractical <= chiSquare1}");
            Console.WriteLine($"Compliance with the condition2 : {ChiSquarePractical <= chiSquare2}");
            Console.WriteLine($"Compliance with the condition3 : {ChiSquarePractical <= chiSquare3}");


            Console.WriteLine("\n\n" + new string('-', 70));

        }
        private Dictionary<byte, int> BytesCount(List<byte> byteSequence)
        {
            Dictionary<byte, int> result = new Dictionary<byte, int>();

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                result.Add((byte)i, 0);
            }

            foreach (byte item in byteSequence)
            {
                result[item]++;
            }

            for (int i = 0; i <= 255; i++)
            {
                if (result[(byte)i] == 0)
                {
                    result[(byte)i]++;
                }
            }


            return result;
        }
        private List<List<byte>> FillSubsecuences(List<byte> byteSequence)
        {
            subsequences = new List<List<byte>>();
            int subLength = byteSequence.Count / r;

            for (int i = 0; i < r; i++)
            {
                subsequences.Add(byteSequence.GetRange(i, subLength));
            }

            return subsequences;
        }

    }


}
