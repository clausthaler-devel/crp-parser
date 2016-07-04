using System.IO;
using Newtonsoft.Json;
using CRPTools;

namespace CRPTools
{
    public static class CrpExporter
    {
        public static bool Export( string fileName )
        {
            return Export( fileName, Directory.GetCurrentDirectory() );
        }

        public static bool Export( string fileName, string saveToPath )
        {
            var assetInfo = CrpDeserializer.parseFile( fileName );
            return Export( assetInfo, saveToPath );
        }
        
        public static bool Export( CrpAssetInfo assetInfo, string saveToPath )
        {
            if ( Directory.Exists( saveToPath ) || MakePath( saveToPath ) )
                return ExportHeader( assetInfo, saveToPath ) &&
                        ExportGameObjects( assetInfo, saveToPath ) &&
                        ExportImages( assetInfo, saveToPath ) &&
                        ExportTextures( assetInfo, saveToPath ) &&
                        ExportMaterials( assetInfo, saveToPath ) &&
                        ExportMeshes( assetInfo, saveToPath ) &&
                        ExportBlobs( assetInfo, saveToPath ) &&
                        ExportInfoGens( assetInfo, saveToPath ) && 
                        ExportMetadata( assetInfo, saveToPath ) &&
                        true;
            return false;
        }

        static bool MakePath( string directory )
        {
            return MakePath( new DirectoryInfo( directory ) );
        }

        static bool MakePath( DirectoryInfo directory )
        {
            try
            {
                if ( !directory.Exists && MakePath( directory.Parent ) )
                    directory.Create();
            }
            catch
            {
                return false;
            }

            return true;
        }

        static bool ExportHeader( CrpAssetInfo assetInfo, string saveToPath )
        {
            return ExportJSON( Path.Combine( saveToPath, assetInfo.header.mainAssetName + "_header.json" ), assetInfo.header );
        }

        static bool ExportGameObjects( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var gameObject in assetInfo.gameObjects )
                if ( !ExportJSON( Path.Combine( saveToPath, gameObject.Key + ".bin"), gameObject.Value ) )
                    return false;
            return true;
        }

        static bool ExportImages( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var image in assetInfo.Images )
                if ( !ExportImage( Path.Combine( saveToPath, image.Key + ".png" ), image.Value ) )
                    return false;
            return true;
        }

        static bool ExportTextures( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var image in assetInfo.Textures )
                if ( !ExportImage( Path.Combine( saveToPath, image.Key + ".png" ), image.Value ) )
                    return false;
            return true;
        }

        static bool ExportMaterials( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var gameObject in assetInfo.Materials )
                if ( !ExportJSON( Path.Combine( saveToPath, gameObject.Key + ".json"), gameObject.Value ) )
                    return false;
            return true;
        }

        static bool ExportMeshes( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var gameObject in assetInfo.Meshes )
                if ( !ExportJSON( Path.Combine( saveToPath, gameObject.Key + ".json"), gameObject.Value ) )
                    return false;
            return true;
        }

        static bool ExportBlobs( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var gameObject in assetInfo.blobs )
                if ( !ExportBinary( Path.Combine( saveToPath, gameObject.Key + ".bin" ), gameObject.Value ) )
                    return false;
            return true;
        }

        static bool ExportInfoGens( CrpAssetInfo assetInfo, string saveToPath )
        {
            foreach ( var gameObject in assetInfo.InfoGens )
                if ( !ExportJSON( Path.Combine( saveToPath, gameObject.Key + ".json" ), gameObject.Value ) )
                    return false;
            return true;
        }

        static bool ExportMetadata( CrpAssetInfo assetInfo, string saveToPath )
        {
            return ExportJSON( Path.Combine( saveToPath, "metadata.json" ), assetInfo.metadata );
        }

        static bool ExportJSON( string saveToFile, dynamic dataObject )
        {
            try
            {
                string json = JsonConvert.SerializeObject(dataObject, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
                StreamWriter file = new StreamWriter(new FileStream(saveToFile, FileMode.Create));
                file.Write( json );
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool ExportBinary( string saveToFile, byte[] dataObject )
        {
            try
            {
                FileStream fs = new FileStream(saveToFile, FileMode.Create);
                fs.Write( dataObject, 0, dataObject.Length );
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool ExportImage( string saveToFile, dynamic dataObject )
        {
            try
            {
                dataObject.Write( saveToFile );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}