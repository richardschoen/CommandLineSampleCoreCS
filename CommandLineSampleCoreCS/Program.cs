using System;
using CommandLine;
using System.IO;

namespace CommandLineSampleCoreCS
{
    class Program
    {
        // Useful CommandLine parser links
        // Google: commandlineparser examples c#
        // https://github.com/commandlineparser/commandline
        // https://csharp.hotexamples.com/examples/CommandLine/CommandLineParser/-/php-commandlineparser-class-examples.html

        // Command line parm parsing option class
        // Define all your parameters in this class
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
            
            [Option("parm1", Required = true, HelpText = "String parameter 1.")]
            public string StringParm1 { get; set; }

            [Option("parm2", Required = true, HelpText = "String parameter 2.")]
            public string StringParm2 { get; set; }

            [Option("intparm1", Default = 22,Required = false, HelpText = "Optional integer parameter 1")]
            public int IntParm1 { get; set; }

            [Option("intparm2", Required = true, HelpText = "Required integer parameter 2")]
            public int Intparm2 { get; set; }

            [Option("boolparm1", Default=false, Required = false, HelpText = "Optional boolean parm. (True/False)")]
            public  bool Boolparm1 { get; set; }

        }
        static void Main(string[] args)
        {

            string dashes = "------------------------------------------------------------------";
            string stringparm1 = "";
            string stringparm2 = "";
            int intparm1 = 0;
            int intparm2 = 0;
            bool boolparm1 = false;
            int exitcode = 0;
            string exitmsg = "";

            try

            {

                // Parse command line parms
                var optionsAreValid=Parser.Default.ParseArguments<Options>(args)
                    .WithParsed<Options>(o =>
                             {
                                 if (o.Verbose)
                                 {
                                     //Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                                     //Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                                 }
                                 else
                                 {
                                     //Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                                     //Console.WriteLine("Quick Start Example!");
                                 }

                                 // Get command line parms and move to app work fields. 
                                 stringparm1 = o.StringParm1;
                                 stringparm2 = o.StringParm2;
                                 boolparm1 = o.Boolparm1;
                                 intparm1 = o.IntParm1;
                                 intparm2 = o.Intparm2;

                             })
                    // Handle parsing errors
                    .WithNotParsed<Options>(o =>
                     {
                         // Bail out if any parsing errors detected
                         throw new Exception("At least one invalid command line parameter was passed.");
                     });

                // Process start message - Optional, but I like a start/end block
                Console.WriteLine(dashes);
                Console.WriteLine(String.Format("Starting command line parse process at {0}",DateTime.Now));

                // TODO = Do your all your real work work here. 

                // This example juist writes out the parms
                Console.WriteLine(String.Format("String parm 1: {0}",stringparm1));
                Console.WriteLine(String.Format("String parm 1: {0}", stringparm1));

                // Program work completed normally. Set exit code and final console message.
                exitcode = 0;
                exitmsg = String.Format("Command line parsed successfully");
                Console.WriteLine(exitmsg);

            } catch (Exception ex)
            {
                // Catch and handle errors. In this case we just set return code
                // and out put the message. 
                exitcode = 99;
                exitmsg = ex.Message;
                Console.WriteLine(exitmsg);
            }
            finally
            {
                // Write out and final data and exit the program now. We're done.
                Console.WriteLine(String.Format("Ending command line parse process at {0}", DateTime.Now));
                Console.WriteLine(dashes);
                Environment.Exit(exitcode);
            }

        }
    }
}
