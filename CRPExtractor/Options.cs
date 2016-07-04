using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace CRPExporter
{
    public class Options
    {
        [Option( 'f', "file", Required = true,
          HelpText = "Input file to be processed." )]
        public string InputFile { get; set; }

        [Option( 'o', "file", Required = true,
        HelpText = "output directory for Exported files." )]
        public string OutputDirectory { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
