using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System;

namespace RandomSequences
{
    class BlumBlumShubByteModification : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }
        private readonly BigInteger n;
        private BigInteger r ;

        public BlumBlumShubByteModification()
        {
            GeneratorName = "BlumBlumShubByteModification";
            ByteSequence = new List<byte>();
            BigInteger p = BigInteger.Parse("0D5BBB96D30086EC484EBA3D7F9CAEB07", NumberStyles.HexNumber);
            BigInteger q = BigInteger.Parse("0425D2B9BFDB25B9CF6C416CC6E37B59C1F", NumberStyles.HexNumber);
            
            r = BigOperations.RandomGenerate(p);
            n = p * q;
        }
        public void GenerateBytes(int byteLength)
        {
            ByteSequence.Clear();
            
            for (int i = 0; i < byteLength; i++)
            {
                r = BigInteger.ModPow(r, 2, n);
                byte x = (byte)(r & 255);
                ByteSequence.Add(x);
            }
        }
    }
   
}
