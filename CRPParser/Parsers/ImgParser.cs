
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRPTools.Parsers
{
    public static class ImgParser
    {
        public static byte[] parseImage(CrpReader reader, long fileSize)
        {
            bool forceLinearFlag = reader.ReadBoolean();
            uint imgLength = reader.ReadUInt32();
            var imgData = reader.ReadBytes((int)imgLength);
            return imgData;
        }
    }
}
