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
<-R or --RepackAll> <Path to folder containing OGG sound files> <Text file with an OGG file name on each line in the order to repack them>
<-e or --Extract> <Internal name of a given sound file>
<-r or --Replace> <Internal name of a given sound file> <Path to new OGG file>
<-L or --ListAll>
```

## Details
The code includes an implementation of both a PCK class and a Sound class, each of which can be easily incorporated into other projects. The bulk of the important code is found in the constructor and Write() function of the PCKFile class, while the PCKEditor contains useful prewritten operations.

The code will not convert the sound files you import or export for you, leaving them as OGG files (although the extensions might say otherwise, they are all unmodified OGG files).

To create and convert OGG files, use a program such as FFMPEG. Note: The game will only work with OGG files, so you have to convert any new songs before you import them into the game. Additionally, the PCK files are exposed in the local game files, so after producing a PCK, no other work needs to be done to make it ready for use (except overwriting the existing PCK file, of course).
