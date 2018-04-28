# SourceLoader
Source Engine map loader written in C#

Incomplete, I revisit it from time to time but don't expect it to ever be completed.
So far only tested with CSGO maps but should work with most other source maps as well.

What's working:
* loading the BSP tree and most (relevant) lumps
* loading 2D VTF textures (cubemaps might work as well, didn't test that)
* extracting basic brush geometry
* exporting as OBJ

What's missing:
* the skybox
* static props
* displacements
* lightmaps (work for it is mostly done, just needs to be brought together)
* exporting materials

The Unity branch is an outdated version of the same code that was tweaked to work with .Net 3.5.
