# PCK Editor
 A program that provides functions for extracting and repacking PCK files for the Steam version of DOKAPON! Sword of Fury.

## Basic Usage
To use the PCKEditor, run the EXE with a path to a PCK as the first argument (which is either the input or output path), then a flag (denoted with one or more dashes) to set the mode, and the arguments for that given mode.

If you encounter any issues, check the log file or report them to ChmtTnky.

Command Line Usage:
PCK-Editor.exe <Path to PCK File> <Mode> <Options>
Modes:
```
<-E or --ExtractAll>
<-R or --RepackAll> <Path to folder containing OPUS sound files> <Text file with an OPUS file name on each line in the order to repack them>
<-e or --Extract> <Internal name of a given sound file>
<-r or --Replace> <Internal name of a given sound file> <Path to new OPUS file>
<-L or --ListAll>
```

## Usage for Modders
Other than the basic use of the tool, you will need to encode your new sound files using the "opusenc.exe" found here: https://opus-codec.org/release/dev/2018/09/18/opus-tools-0_2.html

This is required because SOF will not play sound files if they lack comments describing their "LoopStart" and "LoopEnd" positions. If you import a sound file and nothing plays, this is why (if the game crashes, then you encoded it incorrectly).

To include loop data in your files, you have to encode them using "opusenc.exe", since FFMPEG and other similar tools won't do this for you. To accomplish this, use the following arguments with "opusenc.exe":
`opusenc.exe <new_sound.wav> <internal_sound_name.ext> --comment "LoopStart=<Sample>" -- comment "LoopEnd=<Sample>"`
An example in practice might look like:
`opusenc my_song.wav BGM03.bgm --comment "LoopStart=175900" --comment "LoopEnd=3012183"`
Then, import it into the game files with the PCK Editor.

If the game still refuses to play your sound file, it's possible that the sound doesn't need both comments, so try adding or removing one or both and see what happens. Additionally, the comment describes a sample position, not the time in seconds or milliseconds, so you will need a tool such as Audacity to find the exact loop points and their sample numbers. You can also check the comments of the sound file you are tryng to change with a tool like HxD, which can help you determine exactly what the game wants from you.

## Details
The code includes an implementation of both a PCK class and a Sound class, each of which can be easily incorporated into other projects. The bulk of the important code is found in the constructor and Write() function of the PCKFile class, while the PCKEditor contains useful prewritten operations.

The code will not convert the sound files you import or export for you, leaving them as OPUS files (although the extensions might say otherwise, they are all unmodified OPUS files).
