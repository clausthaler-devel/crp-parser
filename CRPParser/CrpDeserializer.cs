using CRPTools.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRPTools
{
    public class CrpDeserializer
    {
        public string filePath;
        private FileStream stream;
        private CrpReader reader;
        private AssetParser assetParser;
        private Dictionary<string, int> typeRefCount = new Dictionary<string, int>();

        public CrpDeserializer(string filePath)
        {
            stream = File.Open(filePath, FileMode.Open);
            reader = new CrpReader(stream);
            assetParser = new AssetParser(reader);
        }

        public CrpAssetInfo parseFile( Options options )
        {
            return parseFile( options.SaveFiles, options.Verbose );
        }

        public CrpAssetInfo parseFile( bool SaveFiles = false, bool Verbose = false)
        {
            string magicStr = new string(reader.ReadChars(4));
            var crpAssetInfo = new CrpAssetInfo();

            if (magicStr.Equals(Consts.MAGICSTR))
            {
                crpAssetInfo.header = parseHeader();

                if (SaveFiles)
                {
                    string path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + crpAssetInfo.header.mainAssetName + "_contents";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Environment.CurrentDirectory = (path);
                }

                if (Verbose)
                {
                    Console.WriteLine( crpAssetInfo.header );
                }
                if (SaveFiles)
                {
                    StreamWriter file = new StreamWriter(new FileStream(crpAssetInfo.header.mainAssetName + "_header.json", FileMode.Create));
                    string json = JsonConvert.SerializeObject(crpAssetInfo.header, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
                    file.Write(json);
                    file.Close();
                }

                for (int i = 0; i < crpAssetInfo.header.numAssets; i++)
                {
                    parseAssets(crpAssetInfo, i, SaveFiles, Verbose);
                }
            }
            else
            {
                throw new InvalidDataException("Invalid file format!");
            }

            return crpAssetInfo;
        }

        private CrpHeader parseHeader()
        {
            CrpHeader output = new CrpHeader();
            output.formatVersion = reader.ReadUInt16();
            output.packageName = reader.ReadString();
            string encryptedAuthor = reader.ReadString();
            if (encryptedAuthor.Length > 0)
            {
                output.authorName = CryptoUtils.Decrypt(encryptedAuthor);
            }
            else
            {
                output.authorName = "Unknown";
            }
            output.pkgVersion = reader.ReadUInt32();
            output.mainAssetName = reader.ReadString();
            output.numAssets = reader.ReadInt32();
            output.contentBeginIndex = reader.ReadInt64();

            output.assets = new List<CrpAssetInfoHeader>();
            for (int i = 0; i < output.numAssets; i++)
            {
                CrpAssetInfoHeader info = new CrpAssetInfoHeader();
                info.assetName = reader.ReadString();
                info.assetChecksum = reader.ReadString();
                info.assetType = (Consts.AssetTypeMapping)(reader.ReadInt32());
                info.assetOffsetBegin = reader.ReadInt64();
                info.assetSize = reader.ReadInt64();
                output.assets.Add(info);

            }

            return output;
        }

        private void parseAssets(CrpAssetInfo crpInfo, int index, bool saveFiles, bool isVerbose)
        {

            bool isNullFlag = reader.ReadBoolean();
            if (!isNullFlag)
            {
                string assemblyQualifiedName = reader.ReadString();
                string assetType = assemblyQualifiedName.Split(new char[] { ',' })[0];
                long assetContentLen = crpInfo.header.assets[index].assetSize - (2 + assemblyQualifiedName.Length);
                string assetName = reader.ReadString();
                assetContentLen -= (1 + assetName.Length);

                string fileName = string.Format("{0}_{1}_{2}", StrUtils.limitStr(assetName), index, crpInfo.header.assets[index].assetType.ToString());

                dynamic obj = assetParser.parseObject((int)assetContentLen, assetType, saveFiles, fileName, isVerbose);
                switch  ( assetType ) {
                    case "ColossalFramework.Importers.Image": crpInfo.Images.Add( fileName, obj ); break;
                    case "UnityEngine.Texture2D": crpInfo.Textures.Add( fileName, obj ); break;
                    case "UnityEngine.Material": crpInfo.Materials.Add( fileName, obj ); break;
                    case "UnityEngine.Mesh": crpInfo.Meshes.Add( fileName, obj ); break;
                    case "BuildingInfoGen": crpInfo.InfoGens.Add( fileName, obj ); break;
                    case "UnityEngine.GameObject": crpInfo.gameObjects.Add( fileName, obj); break;
                    case "CustomAssetMetaData": crpInfo.metadata = obj; break;
                }
            }

        }

    }
}
