using System.Globalization;
using System.Numerics;
using System.Collections.Generic;

namespace RandomSequences
{
    class BlumMikaliByteModification : IGenerator
    {
        public string GeneratorName { get; private set; }
        private readonly BigInteger p;
        private readonly BigInteger a;
        private BigInteger t;
        public List<byte> ByteSequence { get; private set; }
        public BlumMikaliByteModification()
        {
            GeneratorName = "BlumMikaliByteModification";
            ByteSequence = new List<byte>();
            p = BigInteger.Parse("0CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3", NumberStyles.HexNumber);
            a = BigInteger.Parse("05B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356", NumberStyles.HexNumber);

            t = BigOperations.RandomGenerate(p - 1);
        }
        public void GenerateBytes(int byteLength)
        {
            ByteSequence.Clear();
            for (int i = 0; i < byteLength; i++)
            {
                for(int k = 0; k <= byte.MaxValue; k++)
                {
                    if(k*(p-1) / 265 <= t && t < (k+1)*(p-1) / 256)
                    {
                        ByteSequence.Add((byte) k);
                        break;
                    }
                }
                t = BigInteger.ModPow(a, t, p);
            }
        }
      
    }
   
}
