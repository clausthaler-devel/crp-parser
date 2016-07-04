using System.Collections.Generic;
using CRPTools.Parsers;

namespace CRPTools
{
    public class AssetParser
    {
        private CrpReader reader;

        delegate dynamic Parser(CrpReader reader, long fileSize);

        Dictionary<string, Parser> parsers = new Dictionary<string, Parser>();

        public AssetParser(CrpReader reader) {
            this.reader = reader;

            this.parsers["ColossalFramework.Importers.Image"] = ImgParser.parseImage;
            this.parsers["UnityEngine.Mesh"] = MeshParser.parseMesh;
            this.parsers["UnityEngine.Texture2D"] = ImgParser.parseImage;
            this.parsers["BuildingInfoGen"] = InfoGenParser.parseInfoGen;
            this.parsers["PropInfoGen"] = InfoGenParser.parseInfoGen;
            this.parsers["TreeInfoGen"] = InfoGenParser.parseInfoGen;
            this.parsers["VehicleInfoGen"] = InfoGenParser.parseInfoGen;
            this.parsers["CustomAssetMetaData"] = InfoGenParser.parseInfoGen;
            this.parsers["UnityEngine.Material"] = MaterialParser.parseMaterial;

            //TODO:There are quite a few types that need to be parsed here
            //this.parsers["UnityEngine.GameObject"] = GameObjectParser.parseGameObj;

        }

        public dynamic parseObject(int length, string format)
        {
            dynamic retVal;
            if (parsers.ContainsKey(format))
            {
                retVal = this.parsers[format](reader, length);

            }
            else
            {
                retVal = reader.ReadBytes(length);
            }
            return retVal;
        }
    }
}
