using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class LemehrLowModification : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }
        private uint seed;
        public LemehrLowModification(uint seed)
        {
            GeneratorName = "LemehrLowModification";
            this.seed = seed;
            ByteSequence = new List<byte>();
        }

        public void GenerateBytes(int byteLength)
        {
            uint xPrev = seed;
            for (int i = 0; i < byteLength; i++)
            {
                uint tempValue = ((((uint)Math.Pow(2, 16) + 1) * xPrev) + 119) & uint.MaxValue;
                string lowBits = Convert.ToString(tempValue, 2).PadLeft(32, '0').Substring(24, 8);
                ByteSequence.Add(Convert.ToByte(lowBits, 2));

                xPrev = tempValue;
            }
        }

    }


}
