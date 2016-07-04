using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRPTools.Parsers
{
    public class InfoGenParser : BinaryParser
    {
        public static Dictionary<string, dynamic> parseInfoGen(CrpReader reader, long fileSize)
        {
            Dictionary<string, dynamic> retVal = new Dictionary<string, dynamic>();

            long fileContentBegin = reader.BaseStream.Position;

            int numProperties = reader.ReadInt32();
            for (int i = 0; i < numProperties; i++)
            {
                bool isNull = reader.ReadBoolean();
                if (!isNull)
                {
                    string assemblyQualifiedName = reader.ReadString();
                    string propertyType = assemblyQualifiedName.Split(new char[] { ',' })[0];
                    string propertyName = reader.ReadString();

                    if (propertyType.Contains("[]"))
                        retVal[propertyName] = reader.readUnityArray(propertyType);
                    else
                        retVal[propertyName] = reader.readUnityObj(propertyType);
                }
            }

            ReadUntil( reader, fileContentBegin, fileSize );

            return retVal;
        }
    }
}
