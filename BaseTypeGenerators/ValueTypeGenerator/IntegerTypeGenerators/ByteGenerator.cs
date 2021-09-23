using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class ByteGenerator : Generator
    {
        public override object Generate()
        {
            return (byte)Random.Next(256);
        }
    }
}
