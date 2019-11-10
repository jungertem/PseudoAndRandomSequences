using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculating();

            Console.ReadKey();
        }
        static void Calculating()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.SetWindowSize(85, 50);

            List<IGenerator> generatorList = new List<IGenerator>
            {
                new SystemGenerator(),
                new Linear20(),
                new Linear89(),
                new Geffe(),
                new LemehrLowModification(124),
                new Wolfram(123),
                new BlumMikaliBitModification(),  // calculates  4 minutes (because of ModPow)
                new BlumMikaliByteModification(), // calculates  3 minutes  (because of ModPow)
                new BlumBlumShubBitModification(),
                new BlumBlumShubByteModification(),
                new Librarian("Harry Potter and the Sorcerer.txt")
            };

            int ParameterForThirdTest = 40;
            foreach (IGenerator gen in generatorList)
            {
                gen.GenerateBytes(125000);
                var generatedByteSequence = gen.ByteSequence;
                var nameOfCurrentGenerator = gen.GeneratorName;

                IGeneratorTest currentFirsTest = new SignIndependence(nameOfCurrentGenerator, generatedByteSequence);
                IGeneratorTest currentSecondTest = new SignsEquiprobability(nameOfCurrentGenerator, generatedByteSequence);
                IGeneratorTest currentThirdTest = new UniformityCriterion(nameOfCurrentGenerator, generatedByteSequence, ParameterForThirdTest);

                currentFirsTest.ShowStatistics();
                currentSecondTest.ShowStatistics();
                currentThirdTest.ShowStatistics();
            }


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
        }

    }


}
