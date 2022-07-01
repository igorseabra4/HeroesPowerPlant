using Assimp;
using RenderWareFile;
using RenderWareFile.Sections;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeroesPowerPlant.LevelEditor
{
    public static class BSP_IO_Assimp
    {
        public static string GetImportFilter()
        {
            string[] formats = new AssimpContext().GetSupportedImportFormats();

            string filter = "All supported types|";

            foreach (string s in formats)
                filter += "*" + s + ";";

            filter += "*.bsp|BSP Files|*.bsp";

            foreach (string s in formats)
                filter += "|" + s.Substring(1).ToUpper() + " files|*" + s;

            filter += "|All files|*.*";

            return filter;
        }

        public static ModelConverterData ReadAssimp(string fileName)
        {
            Scene scene = new AssimpContext().ImportFile(fileName,
                PostProcessSteps.Debone | PostProcessSteps.FindInstances | PostProcessSteps.FindInvalidData |
                PostProcessSteps.OptimizeGraph | PostProcessSteps.OptimizeMeshes | PostProcessSteps.Triangulate |
                PostProcessSteps.PreTransformVertices);

            ModelConverterData data = new ModelConverterData()
            {
                MaterialList = new List<string>(),
                VertexList = new List<Vertex>(),
                UVList = new List<Vector2>(),
                ColorList = new List<SharpDX.Color>(),
                TriangleList = new List<Triangle>()
            };

            foreach (var mat in scene.Materials)
                if (mat.TextureDiffuse.FilePath == null)
                    data.MaterialList.Add(Path.GetFileNameWithoutExtension(""));
                else
                    data.MaterialList.Add(Path.GetFileNameWithoutExtension(mat.TextureDiffuse.FilePath));

            int totalVertices = 0;

            foreach (var m in scene.Meshes)
            {
                for (int i = 0; i < m.VertexCount; i++)
                {
                    Vertex v = new Vertex() { Position = new Vector3(m.Vertices[i].X, m.Vertices[i].Y, m.Vertices[i].Z) };

                    if (m.HasTextureCoords(0))
                        v.TexCoord = new Vector2(m.TextureCoordinateChannels[0][i].X, m.TextureCoordinateChannels[0][i].Y);
                    else
                        v.TexCoord = new Vector2();

                    if (m.HasVertexColors(0))
                        v.Color = new SharpDX.Color(m.VertexColorChannels[0][i].R, m.VertexColorChannels[0][i].G, m.VertexColorChannels[0][i].B, m.VertexColorChannels[0][i].A);
                    else
                        v.Color = SharpDX.Color.White;

                    data.VertexList.Add(v);
                }

                foreach (var t in m.Faces)
                    data.TriangleList.Add(new Triangle()
                    {
                        vertex1 = t.Indices[0] + totalVertices,
                        vertex2 = t.Indices[1] + totalVertices,
                        vertex3 = t.Indices[2] + totalVertices,
                        MaterialIndex = m.MaterialIndex
                    });
                totalVertices += m.VertexCount;
            }

            return data;
        }

        public static RWSection[] CreateBSPFromAssimp(string fileName, bool flipUVs)
        {
            PostProcessSteps pps =
                PostProcessSteps.Debone | PostProcessSteps.FindInstances |
                PostProcessSteps.FindInvalidData | PostProcessSteps.OptimizeGraph |
                PostProcessSteps.OptimizeMeshes | PostProcessSteps.Triangulate |
                PostProcessSteps.PreTransformVertices;

            Scene scene = new AssimpContext().ImportFile(fileName, pps);

            int vertexCount = scene.Meshes.Sum(m => m.VertexCount);
            int triangleCount = scene.Meshes.Sum(m => m.FaceCount);

            if (vertexCount > 65535 || triangleCount > 65536)
                throw new ArgumentException("Model has too many vertices or triangles. Please import a simpler model.");

            List<Vertex3> vertices = new List<Vertex3>(vertexCount);
            List<RenderWareFile.Color> vColors = new List<RenderWareFile.Color>(vertexCount);
            List<Vertex2> textCoords = new List<Vertex2>(vertexCount);
            List<RenderWareFile.Triangle> triangles = new List<RenderWareFile.Triangle>(triangleCount);

            int totalVertices = 0;

            foreach (var m in scene.Meshes)
            {
                foreach (Vector3D v in m.Vertices)
                    vertices.Add(new Vertex3(v.X, v.Y, v.Z));

                if (m.HasTextureCoords(0))
                    foreach (Vector3D v in m.TextureCoordinateChannels[0])
                        textCoords.Add(new Vertex2(v.X, flipUVs ? -v.Y : v.Y));
                else
                    for (int i = 0; i < m.VertexCount; i++)
                        textCoords.Add(new Vertex2());

                if (m.HasVertexColors(0))
                    foreach (Color4D c in m.VertexColorChannels[0])
                        vColors.Add(new RenderWareFile.Color(
                            (byte)(c.R * 255),
                            (byte)(c.G * 255),
                            (byte)(c.B * 255),
                            (byte)(c.A * 255)));
                else
                    for (int i = 0; i < m.VertexCount; i++)
                        vColors.Add(new RenderWareFile.Color(255, 255, 255, 255));

                foreach (var t in m.Faces)
                    triangles.Add(new RenderWareFile.Triangle()
                    {
                        vertex1 = (ushort)(t.Indices[0] + totalVertices),
                        vertex2 = (ushort)(t.Indices[1] + totalVertices),
                        vertex3 = (ushort)(t.Indices[2] + totalVertices),
                        materialIndex = (ushort)m.MaterialIndex
                    });

                totalVertices += m.VertexCount;
            }

            if (vertices.Count != textCoords.Count || vertices.Count != vColors.Count)
                throw new ArgumentException("Internal error: texture coordinate or vertex color count is different from vertex count.");

            triangles = triangles.OrderBy(t => t.materialIndex).ToList();

            Vertex3 Max = new Vertex3(vertices[0].X, vertices[0].Y, vertices[0].Z);
            Vertex3 Min = new Vertex3(vertices[0].X, vertices[0].Y, vertices[0].Z);

            foreach (Vertex3 i in vertices)
            {
                if (i.X > Max.X)
                    Max.X = i.X;
                if (i.Y > Max.Y)
                    Max.Y = i.Y;
                if (i.Z > Max.Z)
                    Max.Z = i.Z;
                if (i.X < Min.X)
                    Min.X = i.X;
                if (i.Y < Min.Y)
                    Min.Y = i.Y;
                if (i.Z < Min.Z)
                    Min.Z = i.Z;
            }

            BinMesh[] binMeshes = new BinMesh[scene.MaterialCount];

            Material_0007[] materials = new Material_0007[scene.MaterialCount];

            for (int i = 0; i < scene.MaterialCount; i++)
            {
                List<int> indices = new List<int>(triangles.Count);
                foreach (RenderWareFile.Triangle f in triangles)
                    if (f.materialIndex == i)
                    {
                        indices.Add(f.vertex1);
                        indices.Add(f.vertex2);
                        indices.Add(f.vertex3);
                    }

                binMeshes[i] = new BinMesh()
                {
                    materialIndex = i,
                    indexCount = indices.Count(),
                    vertexIndices = indices.ToArray()
                };

                materials[i] = new Material_0007()
                {
                    materialStruct = new MaterialStruct_0001()
                    {
                        unusedFlags = 0,
                        color = new RenderWareFile.Color(
                             (byte)(scene.Materials[i].ColorDiffuse.R / 255),
                             (byte)(scene.Materials[i].ColorDiffuse.G / 255),
                             (byte)(scene.Materials[i].ColorDiffuse.B / 255),
                             (byte)(scene.Materials[i].ColorDiffuse.A / 255)),
                        unusedInt2 = 0x2DF53E84,
                        isTextured = scene.Materials[i].HasTextureDiffuse ? 1 : 0,
                        ambient = 1f,
                        specular = 1f,
                        diffuse = 1f
                    },
                    texture = scene.Materials[i].HasTextureDiffuse ? new Texture_0006()
                    {
                        textureStruct = new TextureStruct_0001() // use wrap as default
                        {
                            FilterMode = TextureFilterMode.FILTERLINEAR,

                            AddressModeU =
                            scene.Materials[i].TextureDiffuse.WrapModeU == TextureWrapMode.Clamp ? TextureAddressMode.TEXTUREADDRESSCLAMP :
                            scene.Materials[i].TextureDiffuse.WrapModeU == TextureWrapMode.Decal ? TextureAddressMode.TEXTUREADDRESSBORDER :
                            scene.Materials[i].TextureDiffuse.WrapModeU == TextureWrapMode.Mirror ? TextureAddressMode.TEXTUREADDRESSMIRROR :
                            TextureAddressMode.TEXTUREADDRESSWRAP,

                            AddressModeV =
                            scene.Materials[i].TextureDiffuse.WrapModeV == TextureWrapMode.Clamp ? TextureAddressMode.TEXTUREADDRESSCLAMP :
                            scene.Materials[i].TextureDiffuse.WrapModeV == TextureWrapMode.Decal ? TextureAddressMode.TEXTUREADDRESSBORDER :
                            scene.Materials[i].TextureDiffuse.WrapModeV == TextureWrapMode.Mirror ? TextureAddressMode.TEXTUREADDRESSMIRROR :
                            TextureAddressMode.TEXTUREADDRESSWRAP,

                            UseMipLevels = 1
                        },
                        diffuseTextureName = new String_0002(Path.GetFileNameWithoutExtension(scene.Materials[i].TextureDiffuse.FilePath)),
                        alphaTextureName = new String_0002(""),
                        textureExtension = new Extension_0003()
                    } : null,
                    materialExtension = new Extension_0003(),
                };
            }

            WorldFlags worldFlags = WorldFlags.HasOneSetOfTextCoords | WorldFlags.HasVertexColors | WorldFlags.WorldSectorsOverlap | (WorldFlags)0x00010000;

            World_000B world = new World_000B()
            {
                worldStruct = new WorldStruct_0001()
                {
                    rootIsWorldSector = 1,
                    inverseOrigin = new Vertex3(-0f, -0f, -0f),
                    numTriangles = (uint)triangleCount,
                    numVertices = (uint)vertexCount,
                    numPlaneSectors = 0,
                    numAtomicSectors = 1,
                    colSectorSize = 0,
                    worldFlags = worldFlags,
                    boxMaximum = Max,
                    boxMinimum = Min,
                },

                materialList = new MaterialList_0008()
                {
                    materialListStruct = new MaterialListStruct_0001()
                    {
                        materialCount = scene.MaterialCount
                    },
                    materialList = materials
                },

                firstWorldChunk = new AtomicSector_0009()
                {
                    atomicSectorStruct = new AtomicSectorStruct_0001()
                    {
                        matListWindowBase = 0,
                        numTriangles = triangleCount,
                        numVertices = vertexCount,
                        boxMaximum = Max,
                        boxMinimum = Min,
                        collSectorPresent = 0x2F50D984,
                        unused = 0,
                        vertexArray = vertices.ToArray(),
                        colorArray = vColors.ToArray(),
                        uvArray = textCoords.ToArray(),
                        triangleArray = triangles.ToArray()
                    },
                    atomicSectorExtension = new Extension_0003()
                    {
                        extensionSectionList = new List<RWSection>() { new BinMeshPLG_050E()
                        {
                            binMeshHeaderFlags = BinMeshHeaderFlags.TriangleList,
                            numMeshes = binMeshes.Count(),
                            totalIndexCount = binMeshes.Sum(b => b.indexCount),
                            binMeshList = binMeshes
                        }
                        }
                    }
                },

                worldExtension = new Extension_0003()
            };

            return new RWSection[] { world };
        }

        public static void ExportAssimp(string fileName, RenderWareModelFile bspFile, bool flipUVs, ExportFormatDescription format, string textureExtension)
        {
            Scene scene = new Scene();

            foreach (RWSection rw in bspFile.GetAsRWSectionArray())
            {
                if (rw is World_000B w)
                {
                    for (int i = 0; i < w.materialList.materialList.Length; i++)
                    {
                        var mat = w.materialList.materialList[i];
                        string objName = Path.GetFileNameWithoutExtension(fileName) + "_" + (mat.materialStruct.isTextured != 0 ? mat.texture.diffuseTextureName.stringString : "default");

                        scene.Materials.Add(new Material()
                        {
                            ColorDiffuse = new Color4D(
                                mat.materialStruct.color.R / 255f,
                                mat.materialStruct.color.G / 255f,
                                mat.materialStruct.color.B / 255f,
                                mat.materialStruct.color.A / 255f),
                            TextureDiffuse = mat.materialStruct.isTextured != 0 ? new TextureSlot()
                            {
                                FilePath = mat.texture.diffuseTextureName.stringString + textureExtension,
                                TextureType = TextureType.Diffuse
                            } : new TextureSlot(),
                            Name = "mat_" + objName
                        });

                        scene.Meshes.Add(new Mesh(PrimitiveType.Triangle) { MaterialIndex = i, Name = "mesh_" + objName });
                    }

                    if (w.firstWorldChunk.sectionIdentifier == Section.AtomicSector)
                    {
                        GetAtomicTriangleList(scene, (AtomicSector_0009)w.firstWorldChunk);
                    }
                    else if (w.firstWorldChunk.sectionIdentifier == Section.PlaneSector)
                    {
                        GetPlaneTriangleList(scene, (PlaneSector_000A)w.firstWorldChunk);
                    }
                }
            }

            scene.RootNode = new Node() { Name = "root" };

            Node latest = scene.RootNode;

            for (int i = 0; i < scene.MeshCount; i++)
            {
                latest.Children.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(
                    "{\"Name\":\"" + scene.Meshes[i].Name + "\", \"MeshIndices\": [" + i.ToString() + "]}"));

                //latest = latest.Children[0];
            }

            new AssimpContext().ExportFile(scene, fileName, format.FormatId,

                //PostProcessSteps.GenerateNormals |
                PostProcessSteps.JoinIdenticalVertices |
                PostProcessSteps.RemoveRedundantMaterials |
                PostProcessSteps.ValidateDataStructure |

                PostProcessSteps.Debone |
                PostProcessSteps.FindInstances |
                PostProcessSteps.FindInvalidData |
                PostProcessSteps.OptimizeGraph |
                PostProcessSteps.OptimizeMeshes |
                PostProcessSteps.Triangulate |
                PostProcessSteps.PreTransformVertices |

                (flipUVs ? PostProcessSteps.FlipUVs : 0));
        }

        private static void GetPlaneTriangleList(Scene scene, PlaneSector_000A planeSection)
        {
            if (planeSection.leftSection is AtomicSector_0009 a1)
            {
                GetAtomicTriangleList(scene, a1);
            }
            else if (planeSection.leftSection is PlaneSector_000A p1)
            {
                GetPlaneTriangleList(scene, p1);
            }

            if (planeSection.rightSection is AtomicSector_0009 a2)
            {
                GetAtomicTriangleList(scene, a2);
            }
            else if (planeSection.rightSection is PlaneSector_000A p2)
            {
                GetPlaneTriangleList(scene, p2);
            }
        }

        private static void GetAtomicTriangleList(Scene scene, AtomicSector_0009 atomic)
        {
            if (atomic.atomicSectorStruct.isNativeData)
            {
                GetNativeTriangleList(scene, atomic.atomicSectorExtension);
                return;
            }

            int[] totalVertexIndices = new int[scene.MeshCount];

            for (int i = 0; i < scene.MeshCount; i++)
                totalVertexIndices[i] = scene.Meshes[i].VertexCount;

            foreach (RenderWareFile.Triangle t in atomic.atomicSectorStruct.triangleArray)
            {
                scene.Meshes[t.materialIndex].Faces.Add(new Face(new int[] {
                    t.vertex1 + totalVertexIndices[t.materialIndex],
                    t.vertex2 + totalVertexIndices[t.materialIndex],
                    t.vertex3 + totalVertexIndices[t.materialIndex]
                }));
            }

            foreach (Mesh mesh in scene.Meshes)
            {
                foreach (Vertex3 v in atomic.atomicSectorStruct.vertexArray)
                    mesh.Vertices.Add(new Vector3D(v.X, v.Y, v.Z));

                foreach (Vertex2 v in atomic.atomicSectorStruct.uvArray)
                    mesh.TextureCoordinateChannels[0].Add(new Vector3D(v.X, v.Y, 0f));

                foreach (RenderWareFile.Color c in atomic.atomicSectorStruct.colorArray)
                    mesh.VertexColorChannels[0].Add(new Color4D(
                        c.R / 255f,
                        c.G / 255f,
                        c.B / 255f,
                        c.A / 255f));
            }
        }

        private static void GetNativeTriangleList(Scene scene, Extension_0003 extension)
        {
            NativeDataGC n = null;

            foreach (RWSection rw in extension.extensionSectionList)
            {
                if (rw is BinMeshPLG_050E binmesh)
                {
                    if (binmesh.numMeshes == 0) return;
                }
                if (rw is NativeDataPLG_0510 native)
                {
                    n = native.nativeDataStruct.nativeData;
                }
            }

            if (n == null)
                throw new Exception("Native data not found");

            List<Vertex3> vertexList_init = new List<Vertex3>();
            List<RenderWareFile.Color> colorList_init = new List<RenderWareFile.Color>();
            List<Vertex2> textCoordList_init = new List<Vertex2>();

            foreach (Declaration d in n.declarations)
            {
                foreach (object o in d.entryList)
                {
                    if (o is Vertex3 v)
                        vertexList_init.Add(v);
                    else if (o is RenderWareFile.Color c)
                        colorList_init.Add(c);
                    else if (o is Vertex2 t)
                        textCoordList_init.Add(t);
                    else throw new Exception();
                }
            }

            foreach (TriangleDeclaration td in n.triangleDeclarations)
            {
                Mesh mesh = new Mesh(PrimitiveType.Triangle)
                {
                    MaterialIndex = td.MaterialIndex,
                    Name = scene.Materials[td.MaterialIndex].Name.Replace("mat_", "mesh_") + "_" + (scene.MeshCount + 1).ToString()
                };

                foreach (TriangleList tl in td.TriangleListList)
                {
                    int totalVertexIndices = mesh.VertexCount;
                    int vcount = 0;

                    foreach (int[] objectList in tl.entries)
                    {
                        for (int j = 0; j < objectList.Count(); j++)
                        {
                            if (n.declarations[j].declarationType == Declarations.Vertex)
                            {
                                var v = vertexList_init[objectList[j]];
                                mesh.Vertices.Add(new Vector3D(v.X, v.Y, v.Z));
                                vcount++;
                            }
                            else if (n.declarations[j].declarationType == Declarations.Color)
                            {
                                var c = colorList_init[objectList[j]];
                                mesh.VertexColorChannels[0].Add(new Color4D(
                                        c.R / 255f,
                                        c.G / 255f,
                                        c.B / 255f,
                                        c.A / 255f));
                            }
                            else if (n.declarations[j].declarationType == Declarations.TextCoord)
                            {
                                var v = textCoordList_init[objectList[j]];
                                mesh.TextureCoordinateChannels[0].Add(new Vector3D(v.X, v.Y, 0f));
                            }
                        }
                    }

                    bool control = true;
                    for (int i = 2; i < vcount; i++)
                    {
                        if (control)
                        {
                            mesh.Faces.Add(new Face(new int[] {
                                i - 2 + totalVertexIndices,
                                i - 1 + totalVertexIndices,
                                i + totalVertexIndices
                            }));

                            control = false;
                        }
                        else
                        {
                            mesh.Faces.Add(new Face(new int[] {
                                i - 2 + totalVertexIndices,
                                i + totalVertexIndices,
                                i - 1 + totalVertexIndices
                            }));

                            control = true;
                        }
                    }
                }

                scene.Meshes.Add(mesh);
            }
        }
    }
}
