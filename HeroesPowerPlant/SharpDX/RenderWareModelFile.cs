using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using RenderWareFile;
using RenderWareFile.Sections;

namespace HeroesPowerPlant
{
    public class RenderWareModelFile
    {
        public string fileName;

        private const string DefaultTexture = "default";
        private byte[] rwSectionByteArray;
        private RWSection[] rwSectionArray;

        public List<string> MaterialList = new List<string>();
        public string ChunkName;
        public int ChunkNumber;
        public bool isNoCulling = false;
        public bool isShadowCollision = false;

        public List<SharpMesh> meshList;
        
        public uint vertexAmount;
        public uint triangleAmount;

        public List<Vector3> vertexList;
        public List<Triangle> triangleList;

        public RenderWareModelFile(string fileName)
        {
            this.fileName = fileName;
        }

        public void SetChunkNumberAndName()
        {
            string materialType = System.IO.Path.GetFileNameWithoutExtension(fileName).TrimStart
                ('s', 't', 'g', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0');

            if (materialType.Contains("W"))
                isNoCulling = true;
            else
                isNoCulling = false;

            if (materialType.Contains("D") | materialType.Contains("O"))
                ChunkName = "O";
            else if (materialType.Contains("K") | materialType.Contains("G"))
                ChunkName = "K";
            else if (materialType.Contains("P") | materialType.Contains("A"))
                ChunkName = "P";

            foreach (string s in materialType.Split('_'))
                try
                {
                    ChunkNumber = Convert.ToByte(s);
                    break;
                }
                catch { ChunkNumber = -1; }
        }

        public List<Vector3> GetVertexList()
        {
            return vertexList;
        }

        public byte[] GetAsByteArray()
        {
            return rwSectionByteArray;
        }

        public RWSection[] GetAsRWSectionArray()
        {
            return rwSectionArray;
        }

        public void SetForRendering(SharpDevice device, RWSection[] rwChunkList, byte[] rwByteArray)
        {
            rwSectionArray = rwChunkList;

            if (rwByteArray == null)
            {
                rwSectionByteArray = ReadFileMethods.ExportRenderWareFile(rwSectionArray, isShadowCollision ? LevelEditor.BSP_IO_ShadowCollision.shadowRenderWareVersion : LevelEditor.BSP_IO_Heroes.heroesRenderWareVersion);

                if (rwSectionByteArray.Length > 450 * 1024)
                    System.Windows.Forms.MessageBox.Show(fileName + " is larger than 450 kb. I will still import it for you, but be warned that files too large might make the game crash.");
            }
            else
                rwSectionByteArray = rwByteArray;
            meshList = new List<SharpMesh>();

            vertexList = new List<Vector3>();
            triangleList = new List<Triangle>();

            foreach (RWSection rwSection in rwSectionArray)
            {
                if (rwSection is World_000B w)
                {
                    vertexAmount = w.worldStruct.numVertices;
                    triangleAmount = w.worldStruct.numTriangles;

                    foreach (Material_0007 m in w.materialList.materialList)
                    {
                        if (isShadowCollision)
                        {
                            MaterialList.Add(m.materialStruct.color.ToString());
                        }
                        else if (m.texture != null)
                        {
                            MaterialList.Add(m.texture.diffuseTextureName.stringString);
                        }
                        else
                        {
                            MaterialList.Add(DefaultTexture);
                        }
                    }
                    if (w.firstWorldChunk is AtomicSector_0009 a)
                    {
                        AddAtomic(device, a);
                    }
                    else if (w.firstWorldChunk is PlaneSector_000A p)
                    {
                        AddPlane(device, p);
                    }
                }
                else if (rwSection is Clump_0010 c)
                {
                    for (int g = 0; g < c.geometryList.geometryList.Count; g++)
                    {
                        AddGeometry(device, c.geometryList.geometryList[g], CreateMatrix(c.frameList, c.atomicList[g].atomicStruct.frameIndex));                        
                    }
                }
            }
        }

        private Matrix CreateMatrix(FrameList_000E frameList, int frameIndex)
        {
            Matrix transform = Matrix.Identity;

            for (int f = 0; f < frameList.frameListStruct.frames.Count; f++)
            {
                if (frameIndex == f)
                {
                    Frame cf = frameList.frameListStruct.frames[f];

                    transform.M11 = cf.rotationMatrix.M11;
                    transform.M12 = cf.rotationMatrix.M12;
                    transform.M13 = cf.rotationMatrix.M13;
                    transform.M21 = cf.rotationMatrix.M21;
                    transform.M22 = cf.rotationMatrix.M22;
                    transform.M23 = cf.rotationMatrix.M23;
                    transform.M31 = cf.rotationMatrix.M31;
                    transform.M32 = cf.rotationMatrix.M32;
                    transform.M33 = cf.rotationMatrix.M33;

                    transform *= Matrix.Translation(cf.position.X, cf.position.Y, cf.position.Z);
                    break;
                }
            }

            return transform;
        }

        private void AddPlane(SharpDevice device, PlaneSector_000A planeSection)
        {
            if (planeSection.leftSection is AtomicSector_0009 al)
            {
                AddAtomic(device, al);
            }
            else if (planeSection.leftSection is PlaneSector_000A pl)
            {
                AddPlane(device, pl);
            }
            else throw new Exception();

            if (planeSection.rightSection is AtomicSector_0009 ar)
            {
                AddAtomic(device, ar);
            }
            else if (planeSection.rightSection is PlaneSector_000A pr)
            {
                AddPlane(device, pr);
            }
            else throw new Exception();
        }

        void AddAtomic(SharpDevice device, AtomicSector_0009 AtomicSector)
        {
            if (AtomicSector.atomicSectorStruct.isNativeData)
            {
                AddNativeData(device, AtomicSector.atomicSectorExtension, MaterialList, Matrix.Identity);
                return;
            }
            
            List<VertexColoredTextured> vertexList = new List<VertexColoredTextured>();

            foreach (Vertex3 v in AtomicSector.atomicSectorStruct.vertexArray)
            {
                vertexList.Add(new VertexColoredTextured(new Vector3(v.X, v.Y, v.Z), new Vector2(), new SharpDX.Color()));
                this.vertexList.Add(new Vector3(v.X, v.Y, v.Z));
            }

            if (!isShadowCollision)
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    RenderWareFile.Color c = AtomicSector.atomicSectorStruct.colorArray[i];
                    
                    VertexColoredTextured v = vertexList[i];
                    v.Color = new SharpDX.Color(c.R, c.G, c.B, c.A);
                    vertexList[i] = v;
                }

                for (int i = 0; i < vertexList.Count; i++)
                {
                    Vertex2 tc = AtomicSector.atomicSectorStruct.uvArray[i];

                    VertexColoredTextured v = vertexList[i];
                    v.TextureCoordinate = new Vector2(tc.X, tc.Y);
                    vertexList[i] = v;
                }
            }

            List<SharpSubSet> SubsetList = new List<SharpSubSet>();
            List<int> indexList = new List<int>();
            int previousIndexCount = 0;
            
            for (int i = 0; i < MaterialList.Count; i++)
            {
                for (int j = 0; j < AtomicSector.atomicSectorStruct.triangleArray.Length; j++) // each (Triangle t in AtomicSector.atomicStruct.triangleArray)
                {
                    Triangle t = AtomicSector.atomicSectorStruct.triangleArray[j];
                    if (t.materialIndex == i)
                    {
                        indexList.Add(t.vertex1);
                        indexList.Add(t.vertex2);
                        indexList.Add(t.vertex3);
                        
                        if (isShadowCollision)
                        {
                            RenderWareFile.Color c = RenderWareFile.Color.FromString(MaterialList[i]);
                            SharpDX.Color color = new SharpDX.Color(c.R, c.G, c.B, c.A);

                            VertexColoredTextured v1 = vertexList[t.vertex1];
                            v1.Color = color;
                            vertexList[t.vertex1] = v1;

                            VertexColoredTextured v2 = vertexList[t.vertex2];
                            v2.Color = color;
                            vertexList[t.vertex2] = v2;

                            VertexColoredTextured v3 = vertexList[t.vertex3];
                            v3.Color = color;
                            vertexList[t.vertex3] = v3;
                        }

                        triangleList.Add(t);
                    }
                }

                if (indexList.Count - previousIndexCount > 0)
                {
                    SubsetList.Add(new SharpSubSet(previousIndexCount, indexList.Count - previousIndexCount,
                        TextureManager.GetTextureFromDictionary(MaterialList[i]), MaterialList[i]));
                }

                previousIndexCount = indexList.Count();
            }

            if (SubsetList.Count > 0)
                meshList.Add(SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), SubsetList));
        }
        
        private void AddGeometry(SharpDevice device, Geometry_000F g, Matrix transformMatrix)
        {
            List<string> MaterialList = new List<string>();
            foreach (Material_0007 m in g.materialList.materialList)
            {
                if (m.texture != null)
                {
                    string textureName = m.texture.diffuseTextureName.stringString;
                    if (!MaterialList.Contains(textureName))
                        MaterialList.Add(textureName);
                }
                else
                    MaterialList.Add(DefaultTexture);
            }

            if ((g.geometryStruct.geometryFlags2 & GeometryFlags2.isNativeGeometry) != 0)
            {
                AddNativeData(device, g.geometryExtension, MaterialList, transformMatrix);
                return;
            }

            List<VertexColoredTextured> vertexList = new List<VertexColoredTextured>();

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasVertexPositions) != 0)
            {
                foreach (Vertex3 v in g.geometryStruct.morphTargets[0].vertices)
                {
                    Vector3 Position = (Vector3)Vector3.Transform(new Vector3(v.X, v.Y, v.Z), transformMatrix);
                    
                    vertexList.Add(new VertexColoredTextured(Position, new Vector2(), SharpDX.Color.White));
                    this.vertexList.Add(Position);
                }
            }

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasVertexColors) != 0)
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    RenderWareFile.Color c = g.geometryStruct.vertexColors[i];

                    VertexColoredTextured v = vertexList[i];
                    v.Color = new SharpDX.Color(c.R, c.G, c.B, c.A);
                    vertexList[i] = v;
                }
            }
            else
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    VertexColoredTextured v = vertexList[i];
                    v.Color = SharpDX.Color.White;
                    vertexList[i] = v;
                }
            }

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasTextCoords) != 0)
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    Vertex2 tc = g.geometryStruct.textCoords[i];

                    VertexColoredTextured v = vertexList[i];
                    v.TextureCoordinate = new Vector2(tc.X, tc.Y);
                    vertexList[i] = v;
                }
            }

            List<SharpSubSet> SubsetList = new List<SharpSubSet>();
            List<int> indexList = new List<int>();
            int previousIndexCount = 0;

            for (int i = 0; i < MaterialList.Count; i++)
            {
                foreach (Triangle t in g.geometryStruct.triangles)
                {
                    if (t.materialIndex == i)
                    {
                        indexList.Add(t.vertex1);
                        indexList.Add(t.vertex2);
                        indexList.Add(t.vertex3);

                        triangleList.Add(t);
                    }
                }

                if (indexList.Count - previousIndexCount > 0)
                {
                    SubsetList.Add(new SharpSubSet(previousIndexCount, indexList.Count - previousIndexCount,
                        TextureManager.GetTextureFromDictionary(MaterialList[i]), MaterialList[i]));
                }
                
                previousIndexCount = indexList.Count();
            }

            if (SubsetList.Count > 0)
                meshList.Add(SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), SubsetList));
        }

        void AddNativeData(SharpDevice device, Extension_0003 extension, List<string> MaterialStream, Matrix transformMatrix)
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
                    break;
                }
            }

            if (n == null) throw new Exception(ChunkName + ChunkNumber.ToString());

            List<Vertex3> vertexList1 = new List<Vertex3>();
            List<RenderWareFile.Color> colorList = new List<RenderWareFile.Color>();
            List<Vertex2> textCoordList = new List<Vertex2>();
            
            foreach (Declaration d in n.declarations)
            {
                foreach (object o in d.entryList)
                {
                    if (o is Vertex3 v)
                        vertexList1.Add(v);
                    else if (o is RenderWareFile.Color c)
                        colorList.Add(c);
                    else if (o is Vertex2 t)
                        textCoordList.Add(t);
                    else throw new Exception();
                }
            }

            List<VertexColoredTextured> vertexList = new List<VertexColoredTextured>();
            List<int> indexList = new List<int>();
            int k = 0;
            int previousAmount = 0;
            List<SharpSubSet> subSetList = new List<SharpSubSet>();

            foreach (TriangleDeclaration td in n.triangleDeclarations)
            {
                foreach (TriangleList tl in td.TriangleListList)
                {
                    foreach (int[] objectList in tl.entries)
                    {
                        VertexColoredTextured v = new VertexColoredTextured();

                        for (int j = 0; j < objectList.Count(); j++)
                        {
                            if (n.declarations[j].declarationType == Declarations.Vertex)
                            {
                                v.Position = (Vector3)Vector3.Transform(
                                    new Vector3(
                                        vertexList1[objectList[j]].X,
                                        vertexList1[objectList[j]].Y,
                                        vertexList1[objectList[j]].Z),
                                    transformMatrix);
                            }
                            else if (n.declarations[j].declarationType == Declarations.Color)
                            {
                                v.Color = new SharpDX.Color(colorList[objectList[j]].R, colorList[objectList[j]].G, colorList[objectList[j]].B, colorList[objectList[j]].A);
                            }
                            else if (n.declarations[j].declarationType == Declarations.TextCoord)
                            {
                                v.TextureCoordinate.X = textCoordList[objectList[j]].X;
                                v.TextureCoordinate.Y = textCoordList[objectList[j]].Y;
                            }
                        }

                        vertexList.Add(v);
                        indexList.Add(k);
                        k++;

                        this.vertexList.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));
                    }

                    subSetList.Add(new SharpSubSet(previousAmount, vertexList.Count() - previousAmount,
                        TextureManager.GetTextureFromDictionary(MaterialStream[td.MaterialIndex]), MaterialStream[td.MaterialIndex]));
                    
                    previousAmount = vertexList.Count();
                }
            }

            if (vertexList.Count > 0)
                meshList.Add(SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), subSetList, SharpDX.Direct3D.PrimitiveTopology.TriangleStrip));
        }

        public void Render(SharpDevice device)
        {
            foreach (SharpMesh mesh in meshList)
            {
                if (mesh == null) continue;

                mesh.Begin(device);
                for (int i = 0; i < mesh.SubSets.Count(); i++)
                {
                    device.DeviceContext.PixelShader.SetShaderResource(0, mesh.SubSets[i].DiffuseMap);
                    mesh.Draw(device, i);
                }
            }
        }
    }
}
