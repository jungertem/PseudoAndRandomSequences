using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class LemehrHigModification : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }
        private uint seed;
        public LemehrHigModification(uint seed)
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
                string highBits = Convert.ToString(tempValue, 2).PadLeft(32, '0').Substring(0, 8);
                ByteSequence.Add(Convert.ToByte(highBits, 2));

                xPrev = tempValue;
            }

        }
    }


}
