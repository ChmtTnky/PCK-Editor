using System.Reflection;

namespace PCKEditor
{
	public static class Program
	{
		public static readonly string USAGE =
			"To use the PCKEditor, run the EXE with a path to a PCK as the first argument (which is either the input or output path),\n" +
			"then a flag (denoted with one or more dashes) to set the mode, and the arguments for that given mode.\n" +
			"For more detailed information, please check the README.\n" +
			"If you encounter any issues, check the log file or report them to ChmtTnky.\n" +
			"\n" +
			"Usage:\n" +
			$"{Path.GetFileName(Assembly.GetEntryAssembly().Location)} <Path to PCK File> <Mode> <Options>\n" +
			"\n" +
			"Modes:\n" +
			"<-E or --ExtractAll>\n" +
			"<-R or --RepackAll> <Path to Sounds Folder> <TXT with Ordered Sound Names>\n" +
			"<-e or --Extract> <Sound Name>\n" +
			"<-r or --Replace> <Sound Name> <Path to New Sound>\n" +
			"<-L or --ListAll>";

        public static void Main(string[] args)
		{
			// set up logs
			Log.InitializeLogs();

            // Run program
			#if (!DEBUG)
				try
				{
					Log.WriteToLog("Args: " + string.Join(' ', args) + '\n');
					if (args.Length <= 1)
					{
						Log.WriteLine(USAGE);
					}
					else
						ProcessArgs(args);
				}
				catch (Exception ex)
				{
					Log.OutputError(ex.Message);
				}
			#else
			// dont catch errors in DEBUG mode so Visual Studio reports them to me
				Log.WriteToLog("Args: " + string.Join(' ', args) + '\n');
				if (args.Length <= 1)
				{
					Log.WriteLine(USAGE);
				}
				else
					ProcessArgs(args);
			#endif

            // Exit
            Log.WriteLine("\n" + 
				"Finished!");
			if (args.Length <= 1)
			{
				Log.WriteLine("Press any key to close...");
                Console.ReadKey();
            }
            Log.Shutdown();
        }

		// Read the CLI args and determines what mode to tell the editor to run
		private static void ProcessArgs(string[] args)
		{
			// Check for each option
			if (args[1] == "-E" || args[1] == "--ExtractAll")
			{
				PCKEditor.ProcessPCK(args[0], 'E', args.Skip(2).ToArray());
			}
			else if (args[1] == "-R" || args[1] == "--RepackAll")
			{
				if (args.Length < 4)
					throw new Exception("Arg count is invalid for --RepackAll");

                PCKEditor.ProcessPCK(args[0], 'R', args.Skip(2).ToArray());
			}
			else if (args[1] == "-e" || args[1] == "--Extract")
			{
                if (args.Length < 3)
                    throw new Exception("Arg count is invalid for --Extract");

                PCKEditor.ProcessPCK(args[0], 'e', args.Skip(2).ToArray());
            }
			else if (args[1] == "-r" || args[1] == "--Replace")
			{
                if (args.Length < 4)
                    throw new Exception("Arg count is invalid for --Replace");

                PCKEditor.ProcessPCK(args[0], 'r', args.Skip(2).ToArray());
            }
			else if (args[1] == "-L" || args[1] == "--ListAll")
			{
				PCKEditor.ProcessPCK(args[0], 'L');
            }
			else
				throw new Exception($"Mode \"{args[1]}\" is invalid.");
		}
	}
}