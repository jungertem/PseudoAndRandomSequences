using System;
using System.Numerics;

namespace RandomSequences
{
    class BigOperations
    {
        // stolen from stackoverflow
        public static BigInteger RandomGenerate(BigInteger maxValue)
        {
            Random random = new Random();
            byte[] maxValue_array = maxValue.ToByteArray();
            byte[] randomValue_array = new byte[maxValue_array.Length];
            bool on_limit = true;
            for (int generate_byte = maxValue_array.Length - 1; generate_byte >= 0; generate_byte--)
            {
                byte random_byte = 0;
                if (on_limit)
                {
                    random_byte = (byte)random.Next(maxValue_array[generate_byte]);
                    if (random_byte != (byte)random.Next(maxValue_array[generate_byte]))
                    {
                        on_limit = false;
                    }
                }
                else
                {
                    random_byte = (byte)random.Next(256);
                }
                randomValue_array[generate_byte] = random_byte;
            }
            return new BigInteger(randomValue_array);
        }
    }
   
}
