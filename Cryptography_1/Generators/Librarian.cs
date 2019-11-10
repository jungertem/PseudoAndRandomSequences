using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RandomSequences
{
    class Librarian : IGenerator
    {
        public string GeneratorName { get; private set; }
        public List<byte> ByteSequence { get; private set; }
        private string path;
        public Librarian(string path)
        {
            GeneratorName = "Librarian";
            ByteSequence = new List<byte>();
            this.path = path;
        }
  
        public void GenerateBytes(int byteLength)
        {
            byte[] allBytes = File.ReadAllBytes(path);
            ByteSequence = allBytes.ToList<byte>().GetRange(0, byteLength - 1);
        }
    }


}
