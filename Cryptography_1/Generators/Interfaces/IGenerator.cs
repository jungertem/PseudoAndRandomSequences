using System.Collections.Generic;

namespace RandomSequences
{
    internal interface IGenerator
    {
        string GeneratorName { get; }
        List<byte> ByteSequence { get; }
        void GenerateBytes(int byteLength);
    }
}