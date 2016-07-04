using System;
using System.IO;
using CRPTools;

namespace CRPExporter
{
    class Program
    {
        static void Main( string[] args )
        {
            var options = new Options();

            if ( args.Length == 0 || args.Length > 2)
            {
                var usage = options.GetUsage();
                Console.WriteLine( usage );
            }
            else
            {
                if ( args.Length >= 1 )
                    options.InputFile = args[0];

                if ( args.Length == 2 )
                    options.OutputDirectory = args[1];
                else
                    options.OutputDirectory = new FileInfo( options.InputFile ).Directory.FullName;

                try
                {
                    if ( !CrpExporter.Export( options.InputFile, options.OutputDirectory ) )
                        Console.WriteLine( "Unexpected Error while extracting CRP-File." );
                    else
                        Console.WriteLine( "Done." );
                }
                catch (Exception ex)
                {
                    Console.WriteLine( "Error while extracting CRP-File.\r\n" + ex.Message );
                }

            }
        }
    }
}
