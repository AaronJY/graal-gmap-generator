
# Graal GMAP Generator

A tool to automatically generate a GMAP file and level files for a gmap of any given size.

![demonstration](https://i.imgur.com/qxU7XRZ.jpg)

## Options

The following options can be specified before generating a GMAP.
|Option|Description|
|--|--|
|Name|The name of the gmap. The name will be used as the gmap filename, and also as the prefix for the level files it generates.|
|Width, Height|The width and height of the GMAP in levels. For example, generating a 4 x 4 GMAP will result in 16 level files.| 
|Load full map|Loads all map parts into memory on startup.|
|Automapping|If "n" is selected, disables the assembly of automagical screenshots into a map that is drawn over the MAPIMG image.|
|Level links|If "y" is selected, level links will be automatically added between GMAP levels.|

You can also choose to generate the GMAP files in a specific directory on your computer. If no directory path is given, the files will be generated in the application folder under `gmaps/`
