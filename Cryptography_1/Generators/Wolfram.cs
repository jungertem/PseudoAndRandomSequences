using System.Collections.Generic;
using System;

namespace RandomSequences
{
    class Wolfram : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }
        private uint rSeed;
        public Wolfram(uint rSeed)
        {
            GeneratorName = "Wolfram";
            ByteSequence = new List<byte>();
            this.rSeed = rSeed;
        }

        public void GenerateBytes(int byteLength)
        {
            string concatenated = "";

            for (int i = 0; i < byteLength*8; i++)
            {
                byte x = (byte) (rSeed & 1);
                rSeed = (rSeed << 1 | rSeed >> 31) ^ (rSeed | (rSeed >> 1 | rSeed << 31));
                concatenated += x;
                if (concatenated.Length == 8)
                {
                    ByteSequence.Add(Convert.ToByte(concatenated, 2));
                    concatenated = "";
                }
            }
        }
    }

   
   
}
