using System;
using CRPTools;
using CommandLine;


namespace CRPExtractor
{
    class Program
    {
        static void Main( string[] args )
        {
            var options = new Options();

            if ( args.Length == 1 )
            {
                options.InputFile = args[0];
                options.Verbose = false;
                options.SaveFiles = true;

                CrpDeserializer deserializer = new CrpDeserializer(options.InputFile);
                deserializer.parseFile( options );
            }
            else if ( Parser.Default.ParseArguments( args, options ) )
            {
                CrpDeserializer deserializer = new CrpDeserializer(options.InputFile);
                deserializer.parseFile( options );
            }
        }
    }
}
