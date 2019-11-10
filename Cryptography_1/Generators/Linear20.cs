using System;
using System.Collections.Generic;

namespace RandomSequences
{
    class Linear20 : IGenerator
    {

        public string GeneratorName { get; private set; }
        private LinearRegister linearRegister;
        public List<byte> ByteSequence { get; private set; }
        public Linear20()
        {
            GeneratorName = "Linear20";
            linearRegister = new LinearRegister(20, new int[] { 17, 15, 11, 0 });
            ByteSequence = new List<byte>();
        }
        
        public void GenerateBytes(int byteLength)
        {
            string cancatenated = "";
            for (int i = 0; i < byteLength * 8; i++)
            {
                byte next = linearRegister.GenerateNext();
                cancatenated += next;
                if(cancatenated.Length == 8)
                {
                    ByteSequence.Add(Convert.ToByte(cancatenated, 2));
                    cancatenated = "";
                }
            }
        }
    }


}
