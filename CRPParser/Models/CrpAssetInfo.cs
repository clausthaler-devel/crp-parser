using System.Collections.Generic;
using ImageMagick;

namespace CRPTools
{
    public class CrpAssetInfo
    {
        public CrpHeader header { get; set; }
        public Dictionary<string, MaterialStub> Materials = new Dictionary<string, MaterialStub>();
        public Dictionary<string, MagickImage> Images = new Dictionary<string, MagickImage>();
        public Dictionary<string, MagickImage> Textures = new Dictionary<string, MagickImage>();
        public Dictionary<string, Mesh> Meshes = new Dictionary<string, Mesh>();
        public Dictionary<string, Dictionary<string, dynamic>> InfoGens = new Dictionary<string, Dictionary<string, dynamic>>();
        public Dictionary<string, dynamic> gameObjects = new Dictionary<string, dynamic>();
        public Dictionary<string, dynamic> metadata;
    }
}