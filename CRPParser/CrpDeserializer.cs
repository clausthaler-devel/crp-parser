using CRPTools.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;


namespace CRPTools
{

    public static class CrpDeserializer
    {
        //private Dictionary<string, int> typeRefCount = new Dictionary<string, int>();

        public static CrpAssetInfo parseFile( FileInfo file )
        {
            return parseFile( file.FullName );
        }

        public static CrpAssetInfo parseFile( string filePath )
        {
            var stream = File.Open( filePath, FileMode.Open );
            return parseFile( stream );
        }

        public static CrpAssetInfo parseFile( Stream stream )
        {
            var reader = new CrpReader( stream );
            return parseFile( reader );
        }

        public static CrpAssetInfo parseFile( CrpReader reader )
        {

            string magicStr = new string(reader.ReadChars(4));

            var crpAssetInfo = new CrpAssetInfo();

            if (magicStr.Equals(Consts.MAGICSTR))
            {
                crpAssetInfo.header = parseHeader(reader);

                for (int i = 0; i < crpAssetInfo.header.numAssets; i++)
                    parseAssets(reader, crpAssetInfo, i);
            }
            else
            {
                throw new InvalidDataException("Invalid file format!");
            }

            return crpAssetInfo;
        }

        private static CrpHeader parseHeader(CrpReader reader)
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

        private static void parseAssets(CrpReader reader, CrpAssetInfo crpInfo, int index)
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

                dynamic obj = new AssetParser(reader).parseObject((int)assetContentLen, assetType);

                switch  ( assetType ) {
                    case "ColossalFramework.Importers.Image": crpInfo.Images.Add( fileName, obj ); break;
                    case "UnityEngine.Texture2D": crpInfo.Textures.Add( fileName, obj ); break;
                    case "UnityEngine.Material": crpInfo.Materials.Add( fileName, obj ); break;
                    case "UnityEngine.Mesh": crpInfo.Meshes.Add( fileName, obj ); break;
                    case "BuildingInfoGen": crpInfo.InfoGens.Add( fileName, obj ); break;
                    case "UnityEngine.GameObject": crpInfo.gameObjects.Add( fileName, obj); break;
                    case "CustomAssetMetaData": crpInfo.metadata = obj; break;
                    default: crpInfo.blobs.Add( fileName, obj ); break;
                }
            }

        }

    }
}
