namespace PCKEditor
{
    // The bulk of the conversion work is done when you read and write the PCK.
    // As such, the editor mostly just uses those functions and a few others to perform each mode.
    public static class PCKEditor
    {
        public static void ProcessPCK(string pck_path, char option, string[]? args = null)
        {
            switch (option)
            {
                case 'E':
                    {
                        // get output folder
                        string output_dir = Path.Combine("output_" + Path.GetFileNameWithoutExtension(pck_path));
                        if (Directory.Exists(output_dir))
                            Directory.Delete(output_dir, true);
                        Directory.CreateDirectory(output_dir);

                        ExtractAllSounds(pck_path, output_dir);
                        break;
                    }
                case 'R':
                    {
                        Repack(pck_path, args[0], File.ReadAllLines(args[1]));
                        break;
                    }
                case 'e':
                    {
                        ExtractSound(pck_path, args[0]);
                        break;
                    }
                case 'r':
                    {
                        // get output folder
                        string output_dir = Path.Combine("output_" + Path.GetFileNameWithoutExtension(pck_path));
                        if (Directory.Exists(output_dir))
                            Directory.Delete(output_dir, true);
                        Directory.CreateDirectory(output_dir);

                        ReplaceSound(pck_path, args[0], args[1], output_dir);
                        break;
                    }
                case 'L':
                    {
                        ListSounds(pck_path);
                        break;
                    }
                default:
                    throw new Exception($"Processing option \"{option}\" is invalid.");
            }
        }

        public static void ExtractAllSounds(string pck_path, string output_dir = "")
        {
            // Error: PCK isn't real
            if (!File.Exists(pck_path))
                throw new Exception($"\"{pck_path}\" does not exist.");

            // write all sounds in the PCK
            PCKFile pack_file = new PCKFile(pck_path);
            Log.WriteLine($"Exporting {pack_file.sounds.Count} sounds{(output_dir.Length != 0 ? $" to \"{output_dir}\"" : "")}...");
            foreach (var sound in pack_file.sounds)
            {
                Log.WriteToLog($"Exporting \"{sound.name}\"...\n");
                sound.Write(output_dir);
            }
        }

        public static void Repack(string output_path, string source_dir, string[] sound_names)
        {
            // make new PCK and populate with sounds
            PCKFile pack_file = new PCKFile();
            foreach(var name in sound_names)
            {
                Log.WriteToLog($"Adding \"{Path.Combine(source_dir, name)}\" to PCK...\n");
                pack_file.AddSound(Path.Combine(source_dir, name));
            }

            Log.WriteLine($"Writing a new PCK file at \"{output_path}\"...");
            pack_file.Write(output_path);
        }

        public static void ExtractSound(string pck_path, string sound_name, string output_dir = "")
        {
            // Error: PCK isn't real
            if (!File.Exists(pck_path))
                throw new Exception($"\"{pck_path}\" does not exist.");

            // get PCK and check for file
            PCKFile pack_file = new PCKFile(pck_path);
            Sound? sound_to_extract = pack_file.sounds.Find(x => x.name == sound_name);
            if (sound_to_extract == null)
                throw new Exception($"Selected sound \"{sound_name}\" is not in the PCK file.");

            // write the sound if it was found
            Log.WriteLine($"Exporting {sound_to_extract.name}...");
            sound_to_extract.Write(output_dir);
        }

        public static void ReplaceSound(string pck_path, string old_sound, string new_sound, string output_dir = "")
        {
            // get PCK and check for file
            PCKFile out_pck = new PCKFile(pck_path);
            Sound? sound_to_replace = out_pck.sounds.Find(x => x.name == old_sound);
            if (sound_to_replace == null)
                throw new Exception($"\"{old_sound}\" is not in the PCK file.");

            // replace sound and output new PCK
            sound_to_replace = new Sound(new_sound);
            Log.WriteLine($"Replacing \"{old_sound}\" with \"{new_sound}\"...");
            out_pck.Write(Path.Combine(output_dir, Path.GetFileName(pck_path)));
        }

        public static void ListSounds(string pck_path, string output_dir = "")
        {
            // get PCK and write sound names to txt file
            PCKFile pack_file = new PCKFile(pck_path);
            Log.WriteLine($"Writing sound names to \"{Path.GetFileNameWithoutExtension(pck_path) + ".txt"}\"...");
            File.WriteAllLines(Path.Combine(output_dir, Path.GetFileNameWithoutExtension(pck_path) + ".txt"), pack_file.sounds.Select(x => x.name));
        }
    }
}
