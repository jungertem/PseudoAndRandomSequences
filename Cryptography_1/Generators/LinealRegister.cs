using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class LinearRegister
    {
        public List<byte> Sequence { get; private set; }
        private readonly int startLength;
        private readonly int[] triggers;
        public LinearRegister(int startLength, int[] triggers)
        {
            this.triggers = triggers;
            this.startLength = startLength;

            Sequence = new List<byte>();
            Random rand = new Random();

            for (int i = 0; i < startLength; i++)
            {
                Sequence.Add((byte)rand.Next(0, 2));
            }
        }
        public byte GenerateNext()
        {
            byte result = 0;
            foreach (var position in triggers)
            {
                result ^= Sequence[Sequence.Count - startLength + position];
            }

            Sequence.Add(result);
            return result;
        }
    }


}
