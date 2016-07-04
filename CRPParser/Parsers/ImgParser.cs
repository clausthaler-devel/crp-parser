using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRPTools.Parsers
{
    public static class ImgParser
    {
        public static MagickImage parseImage(CrpReader reader, long fileSize)
        {
            bool forceLinearFlag = reader.ReadBoolean();
            uint imgLength = reader.ReadUInt32();
            MagickImage retVal = new MagickImage(reader.ReadBytes((int)imgLength));
            retVal.Format = MagickFormat.Png;
 
            return retVal;
        }
    }
}
