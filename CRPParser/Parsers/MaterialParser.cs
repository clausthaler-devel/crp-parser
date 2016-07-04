using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRPTools.Parsers
{
    class MaterialParser : BinaryParser
    {
        public static MaterialStub parseMaterial(CrpReader reader, long fileSize)
        {
            long fileContentBegin = reader.BaseStream.Position;
            MaterialStub retVal = new MaterialStub();
            retVal.shaderName = reader.ReadString();
            retVal.numProperties = reader.ReadInt32();
            for(int i =0; i< retVal.numProperties; i++)
            {
                int propertyType = reader.ReadInt32();
                string propertyName = reader.ReadString();
                switch (propertyType)
                {
                    case 0:
                        retVal.colors[propertyName] = reader.singlarObjParser["UnityEngine.Color"]();
                        break;
                    case 1:
                        retVal.vectors[propertyName] = reader.singlarObjParser["UnityEngine.Vector4"]();
                        break;
                    case 2:
                        retVal.floats[propertyName] = reader.ReadSingle();
                        break;
                    case 3:
                        bool isNull = reader.ReadBoolean();
                        if (!isNull)
                        {
                            retVal.textures[propertyName] = reader.ReadString();
                        }else
                        {
                            retVal.textures[propertyName] = "";
                        }
                        break;
                }
            }

            ReadUntil( reader, fileContentBegin, fileSize );

            return retVal;
        }
    }
}
