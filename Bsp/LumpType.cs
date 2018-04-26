/*
Distroir.BSP
Copyright (C) 2017 Distroir

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace Source.Bsp
{
    public enum LumpType
    {
        /// <summary>
        /// Map entities
        /// </summary>
        Entities = 0,
        /// <summary>
        /// Plane array
        /// </summary>
        Planes = 1,
        /// <summary>
        /// Index to texture names
        /// </summary>
        TextureData = 2,
        /// <summary>
        /// Vertex array
        /// </summary>
        Vertices = 3,
        /// <summary>
        /// Compressed visibility bit arrays
        /// </summary>
        Visibility = 4,
        /// <summary>
        /// BSP tree nodes
        /// </summary>
        Nodes = 5,
        /// <summary>
        /// Face texture array
        /// </summary>
        TextureInfo = 6,
        /// <summary>
        /// Face array
        /// </summary>
        Faces = 7,
        /// <summary>
        /// Lightmap samples
        /// </summary>
        Lighting = 8,
        /// <summary>
        /// Occlusion polygons and vertices
        /// </summary>
        Occlusion = 9,
        /// <summary>
        /// BSP tree leaf nodes
        /// </summary>
        Leafs = 10,
        /// <summary>
        /// Correlates between dfaces and Hammer face IDs. Also used as random seed for detail prop placement.
        /// </summary>
        FaceIds = 11,
        /// <summary>
        /// Edge array
        /// </summary>
        Edges = 12,
        /// <summary>
        /// Index of edges
        /// </summary>
        SurfaceEdges = 13,
        /// <summary>
        /// Brush models (geometry of brush entities)
        /// </summary>
        Models = 14,
        /// <summary>
        /// Internal world lights converted from the entity lump
        /// </summary>
        WorldLights = 15,
        /// <summary>
        /// Index to faces in each leaf
        /// </summary>
        LeafFaces = 16,
        /// <summary>
        /// Index to brushes in each leaf
        /// </summary>
        LeafBrushes = 17,
        /// <summary>
        /// Brush array
        /// </summary>
        Brushes = 18,
        /// <summary>
        /// Brushside array
        /// </summary>
        BrushSides = 19,
        /// <summary>
        /// Area array
        /// </summary>
        Areas = 20,
        /// <summary>
        /// Portals between areas
        /// </summary>
        AreaPortals = 21,
        /// <summary>
        /// Static props convex hull lists
        /// </summary>
        PropCollisions = 22,
        /// <summary>
        /// Static prop convex hulls
        /// </summary>
        PropHulls = 23,
        /// <summary>
        /// Static prop collision vertices
        /// </summary>
        LUMP_PROPHULLVERTS = 24,
        /// <summary>
        /// Static prop per hull triangle index start/count
        /// </summary>
        PropTriangles = 25,
        /// <summary>
        /// Displacement surface array
        /// </summary>
        DisplacementInfo = 26,
        /// <summary>
        /// Brush faces array before splitting
        /// </summary>
        OriginalFaces = 27,
        /// <summary>
        /// Displacement physics collision data
        /// </summary>
        PhysicsDisplacement = 28,
        /// <summary>
        /// Physics collision data
        /// </summary>
        PhysicsCollision = 29,
        /// <summary>
        /// Face plane normals
        /// </summary>
        VertexNormals = 30,
        /// <summary>
        /// Face plane normal index array
        /// </summary>
        VertexNormalIndices = 31,
        /// <summary>
        /// Displacement lightmap alphas (unused/empty since Source 2006)
        /// </summary>
        DisplacementLightmapAlphas = 32,
        /// <summary>
        /// Vertices of displacement surface meshes
        /// </summary>
        DisplacementVertices = 33,
        /// <summary>
        /// Displacement lightmap sample positions
        /// </summary>
        DisplacementLightmapSamplePositions = 34,
        /// <summary>
        /// Game-specific data lump
        /// </summary>
        GameLump = 35,
        /// <summary>
        /// Data for leaf nodes that are inside water
        /// </summary>
        LeafWaterData = 36,
        /// <summary>
        /// Water polygon data
        /// </summary>
        Primitives = 37,
        /// <summary>
        /// Water polygon vertices
        /// </summary>
        PrimitiveVertices = 38,
        /// <summary>
        /// Water polygon vertex index array
        /// </summary>
        PrimitiveIndices = 39,
        /// <summary>
        /// Embedded uncompressed Zip-format file
        /// </summary>
        PakFile = 40,
        /// <summary>
        /// Clipped portal polygon vertices
        /// </summary>
        ClipPortalVertices = 41,
        /// <summary>
        /// env_cubemap location array
        /// </summary>
        Cubemaps = 42,
        /// <summary>
        /// Texture name data
        /// </summary>
        TextureStringData = 43,
        /// <summary>
        /// Index array into texdata string data
        /// </summary>
        TextureDataStringTable = 44,
        /// <summary>
        /// info_overlay data array
        /// </summary>
        Overlays = 45,
        /// <summary>
        /// Distance from leaves to water
        /// </summary>
        LeafsMinimumDistanceToWater = 46,
        /// <summary>
        /// Macro texture info for faces
        /// </summary>
        FaceMakroTextureInfo = 47,
        /// <summary>
        /// Displacement surface triangles
        /// </summary>
        DisplacementTriangles = 48,
        /// <summary>
        /// Static prop triangle and string data
        /// </summary>
        PropBlob = 49,
        /// <summary>
        /// info_overlay's on water faces?
        /// </summary>
        WaterOverlays = 50,
        /// <summary>
        /// Index of LUMP_LEAF_AMBIENT_LIGHTING_HDR
        /// </summary>
        LeafAmbientIndexHDR = 51,
        /// <summary>
        /// Index of LUMP_LEAF_AMBIENT_LIGHTING
        /// </summary>
        LeafAmbientIndex = 52,
        /// <summary>
        /// HDR lightmap samples
        /// </summary>
        LightingHDR = 53,
        /// <summary>
        /// Internal HDR world lights converted from the entity lump
        /// </summary>
        WorldlightsHDR = 54,
        /// <summary>
        /// Per-leaf ambient light samples (HDR)
        /// </summary>
        LeafAmbientLightingHDR = 55,
        /// <summary>
        /// Per-leaf ambient light samples (LDR)
        /// </summary>
        LeafAmbientLighting = 56,
        /// <summary>
        /// XZip version of pak file for Xbox. Deprecated.
        /// </summary>
        XzipPakFile = 57,
        /// <summary>
        /// HDR maps may have different face data
        /// </summary>
        FacesHDR = 58,
        /// <summary>
        /// Extended level-wide flags. Not present in all levels.
        /// </summary>
        MapFlags = 59,
        /// <summary>
        /// Fade distances for overlays
        /// </summary>
        OverlayFades = 60,
        /// <summary>
        /// System level settings (min/max CPU & GPU to render this overlay)
        /// </summary>
        OverlaySystemSettings = 61,
        /// <summary>
        /// ???
        /// </summary>
        PhysicsLevel = 62,
        /// <summary>
        /// Displacement multiblend info
        /// </summary>
        DisplacementMultiblend = 63
    }
}