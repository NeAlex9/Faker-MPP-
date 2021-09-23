using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class UShortGenerator : Generator
    {
        public override object Generate()
        {
            return (ushort)Random.Next(65536);
        }
    }
}
