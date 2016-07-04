namespace CRPTools.Parsers
{
    public class BinaryParser
    {
        public static void ReadUntil(CrpReader reader, long contentBegin, long size)
        {
            if ( ( reader.BaseStream.Position - contentBegin ) != size )
            {
                int bytesToRead = (int)(size - (reader.BaseStream.Position - contentBegin));
                reader.ReadBytes( bytesToRead );
            }
        }
    }
}
