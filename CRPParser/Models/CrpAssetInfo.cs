using System.Collections.Generic;

namespace CRPTools
{
    public class CrpAssetInfo
    {
        public CrpHeader header { get; set; }
        public Dictionary<string, MaterialStub> Materials = new Dictionary<string, MaterialStub>();
        public Dictionary<string, byte[]> Images = new Dictionary<string, byte[]>();
        public Dictionary<string, byte[]> Textures = new Dictionary<string, byte[]>();
        public Dictionary<string, Mesh> Meshes = new Dictionary<string, Mesh>();
        public Dictionary<string, Dictionary<string, dynamic>> InfoGens = new Dictionary<string, Dictionary<string, dynamic>>();
        public Dictionary<string, dynamic> gameObjects = new Dictionary<string, dynamic>();
        public Dictionary<string, dynamic> metadata;
        public Dictionary<string, dynamic> blobs = new Dictionary<string, dynamic>();
    }
}