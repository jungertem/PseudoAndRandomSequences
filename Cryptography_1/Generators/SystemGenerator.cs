using System.Collections.Generic;
using System;
using System.Linq;

namespace RandomSequences
{
    class SystemGenerator : IGenerator
    {
        public string GeneratorName { get; private set; }
        Random rand;
        public List<byte> ByteSequence { get; private set; }
        public SystemGenerator()
        {
            GeneratorName = "SystemGenerator";
            rand = new Random();
            ByteSequence = new List<byte>();
        }

        public void GenerateBytes(int byteLength)
        {
            byte[] seedArray = new byte[byteLength];
            rand.NextBytes(seedArray);
            ByteSequence = seedArray.ToList();
        }
    }

   
   
}
