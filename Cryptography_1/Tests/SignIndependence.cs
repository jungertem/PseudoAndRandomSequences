using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class SignIndependence : IGeneratorTest
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

        private double ChiSquarePractical;
        private double SequenceLength;
        private int n;

        private Dictionary<Tuple<byte, byte>, int> AllPairsCount;
        private Dictionary<byte, int> FirstByteMap;
        private Dictionary<byte, int> SecondByteMap;
        public SignIndependence(string generatorName, List<byte> byteSequence)
        {
            GeneratorName = generatorName;
            n = byteSequence.Count / 2;
            SequenceLength = byteSequence.Count;

            AllPairsCount = GetAllPairsAndCalculateCount(byteSequence);
            FirstByteMap = FillFirstByteMap();
            SecondByteMap = FillSecondByteMap();

            int l = 255 * 255;
            chiSquare1 = Math.Sqrt(2 * l) * quantile1 + l;
            chiSquare2 = Math.Sqrt(2 * l) * quantile2 + l;
            chiSquare3 = Math.Sqrt(2 * l) * quantile3 + l;

        }
        private void CalculatingChiSquarePractical()
        {
            for (int i = 0; i <= 255; i++)
            {
                for(int j = 0; j <= 255; j++)
                {
                    int x = AllPairsCount[new Tuple<byte, byte>((byte)i, (byte)j)];
                    ChiSquarePractical += x * x / (double)(FirstByteMap[(byte)i] * SecondByteMap[(byte)j]);
                }
            }

            ChiSquarePractical = (ChiSquarePractical - 1) * n;
        }
        private int FirstPlaceByteCount(byte checkByte)
        {
            int finalCount = 0;
            foreach(var pair in AllPairsCount) 
            {
                if(pair.Key.Item1 == checkByte)
                {
                    finalCount += AllPairsCount[pair.Key];
                }
            }

            return finalCount == 0 ? 1 : finalCount;
        }
        private int SecondPlaceByteCount(byte checkByte)
        {
            int finalCount = 0;
            foreach (var pair in AllPairsCount)
            {
                if (pair.Key.Item2 == checkByte)
                {
                    finalCount += AllPairsCount[pair.Key];
                }
            }

            return finalCount == 0? 1 : finalCount;
        }
        private Dictionary<byte,int> FillFirstByteMap()
        {
            Dictionary<byte, int> final = new Dictionary<byte, int>();

            for(int i = 0; i <= 255; i++)
            {
                final.Add((byte)i, FirstPlaceByteCount((byte)i));
            }

            return final;
        }
        private Dictionary<byte,int> FillSecondByteMap()
        {
            Dictionary<byte, int> final = new Dictionary<byte, int>();

            for(int i = 0; i <= 255; i++)
            {
                final.Add((byte)i, SecondPlaceByteCount((byte)i));
            }

            return final;
        }
        private Dictionary<Tuple<byte, byte>, int> GetAllPairsAndCalculateCount(List<byte> sequence)
        {
            Dictionary<Tuple<byte, byte>, int> allPairs = new Dictionary<Tuple<byte, byte>, int>();

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= 255; j++)
                {
                    allPairs.Add(new Tuple<byte, byte>((byte)i, (byte)j), 0);
                }
            }

            for(int i = 1; i < sequence.Count / 2; i++)
            {
                Tuple<byte, byte> currentPair = new Tuple<byte, byte>(sequence[2 * i - 1], sequence[2 * i]);
                allPairs[currentPair]++;
            }


            return allPairs;
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
            Console.WriteLine($"\nSequence length (in bytes): {SequenceLength}");
            Console.WriteLine($"\nChiSquare practical value: {ChiSquarePractical}");
            Console.WriteLine($"Compliance with the condition1 : {ChiSquarePractical <= chiSquare1}");
            Console.WriteLine($"Compliance with the condition2 : {ChiSquarePractical <= chiSquare2}");
            Console.WriteLine($"Compliance with the condition3 : {ChiSquarePractical <= chiSquare3}");
            Console.WriteLine("\n\n" + new string('-', 70));

        }


    }


}
