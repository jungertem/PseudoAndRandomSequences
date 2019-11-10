using System.Collections.Generic;
using System;

namespace RandomSequences
{
    class Geffe : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }

        private LinearRegister L11;
        private LinearRegister L9;
        private LinearRegister L10;

        public Geffe()
        {
            GeneratorName = "GeffeGenerator";
            ByteSequence = new List<byte>();

            L11 = new LinearRegister(11, new int[] { 0, 2 });
            L9  = new LinearRegister(9, new int[] { 0, 1, 3, 4 });
            L10 = new LinearRegister(10, new int[] { 0, 3 });
        }

        public void GenerateBytes(int byteLength)
        {
            string concatenated = "";
            for (int i = 0; i < byteLength * 8; i++)
            {
                byte x = L11.GenerateNext();
                byte y = L9.GenerateNext();
                byte s = L10.GenerateNext();

                concatenated += (s & x) ^ ((1 + s) & y);
                if (concatenated.Length == 8)
                {
                    ByteSequence.Add(Convert.ToByte(concatenated, 2));
                    concatenated = "";
                }
            }
        }

    }
}
