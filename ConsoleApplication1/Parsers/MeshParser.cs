﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRPParser.Parsers
{
    class MeshParser
    {
        public static Mesh parseMesh(CrpReader reader, bool saveFile, string saveFileName, long fileSize, bool verbose)
        {
            long fileContentBegin = reader.BaseStream.Position;

            Mesh retVal = new Mesh();
            retVal.vertices = reader.readUnityArray("UnityEngine.Vector3");
            retVal.colors = reader.readUnityArray("UnityEngine.Color");
            retVal.uv = reader.readUnityArray("UnityEngine.Vector2");
            retVal.normals = reader.readUnityArray("UnityEngine.Vector3");
            retVal.tangents = reader.readUnityArray("UnityEngine.Vector4");
            retVal.boneWeights = reader.readUnityArray("UnityEngine.BoneWeight");
            retVal.bindPoses = reader.readUnityArray("UnityEngine.Matrix4x4");
            retVal.subMeshCount = reader.ReadInt32();
            for (int i = 0; i < retVal.subMeshCount; i++)
            {
                int[] triangles = reader.readUnityArray("System.Int32");
                retVal.triangles.AddRange(triangles);
            }

            if ((reader.BaseStream.Position - fileContentBegin) != fileSize)
            {
                int bytesToRead = (int)(fileSize - (reader.BaseStream.Position - fileContentBegin));
                reader.ReadBytes(bytesToRead);
            }
            string fileName = saveFileName + ".obj";
            if (verbose)
            {
                Console.WriteLine("Read {0} bytes into image file {1}", (reader.BaseStream.Position - fileContentBegin), fileName);
            }
            if (saveFile)
            {
                StreamWriter file = new StreamWriter(new FileStream(fileName, FileMode.Create));
                file.Write(retVal.exportObj());
                file.Close();
            }
            return retVal;
        }

    }
}
