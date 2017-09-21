# Distroir.BSP

Open-source library for reading and editing BSP files

## Features

Distroir.BSP library allows you to easily read and edit BSP files. Things you can do with this library:

- Reading BSP file header
- Reading lump informations
- Reading lump data
- Overwriting header
- Overwriting lump informations
- Overwriting lump data

## Code exampless

#### Reading BSP file header

There are few ways that you can read BSP header. Example code will show both ways you can do it:

```csharp
using (FileStream fs = new FileStream(filename, FileMode.Open))
{
  //Read header from FileStream
  BSPInfo methodOne = BSPReader.ReadInfo(fs);
  
  //Read header from BinaryReader
  using (BinaryReader r = new BinaryReader(fs))
  {
    BSPInfo methodTwo = BSPReader.ReadInfo(r);
    //Do more with BSP file
  }
}
```

#### Reading lump information

You can read lump informations from BSP file header:

```csharp
//Read PakFile lump informations from header two ways
using (FileStream fs = new FileStream(filename, FileMode.Open))
{
  //Read header from FileStream
  BSPInfo header1 = BSPReader.ReadInfo(fs);
  //Read lump from header
  Lump lumpOne = header1.Lumps[40];
  
  using (BinaryReader r = new BinaryReader(fs))
  {
    //Read header from BinaryReader
    BSPInfo header2 = BSPReader.ReadInfo(r);
    //Read lump from header
    Lump lumpTwo = header2.Lumps[(int)BSPLumps.LUMP_PAKFILE];
  }
}
```

Or directly from file:

```csharp
using (FileStream fs = new FileStream(filename, FileMode.Open))
{
  using (BinaryReader r = new BinaryReader(fs))
  {
    //Read PakFile lump informations directly from file
    Lump lumpOne = BSPReader.ReadLump(r, 40);
    Lump lumpTwo = BSPReader.ReadLump(r, (int)BSPLumps.LUMP_PAKFILE);
  }
}
```

#### Reading lump data

Reading lump data is really easy:

```csharp
using (FileStream fs = new FileStream(filename, FileMode.Open))
{
  using (BinaryReader r = new BinaryReader(fs))
  {
    //Read PakFile lump informations
    Lump lump = BSPReader.ReadLump(r, 40);
    //Read data
    byte[] pakFileData = BSPReader.ReadLumpData(r, lump);
  }
}
```

#### Overwriting lump data

You can aslo overwrite lump data. Length of lump and offsets of orther lumps will be updated:

```csharp
//These values are just an example
//They won't work correctly ingame
byte[] newLumpData = new byte[4] { 1, 36, 96, 2 };

//Open file
//We will replace pak lump inside this file
FileStream input = new FileStream

//Create file stream
using (FileStream output = new FileStream(filename, FileMode.Append))
{
  //Write lump data
  BSPWriter.WriteLumpData(input, newLumpData, (int)BSPLumps.LUMP_PAKFILE, output);
}
```

