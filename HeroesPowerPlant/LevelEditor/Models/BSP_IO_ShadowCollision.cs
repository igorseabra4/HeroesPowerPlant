using System;
using System.Collections.Generic;
using System.Linq;
using RenderWareFile;
using RenderWareFile.Sections;

namespace HeroesPowerPlant.LevelEditor
{
    public static class BSP_IO_ShadowCollision
    {
        public static int shadowRenderWareVersion = 0x1C020037;

        public static RWSection[] CreateShadowCollisionBSPFile(ModelConverterData data)
        {
            Vertex3 Max = new Vertex3(data.VertexList[0].Position.X, data.VertexList[0].Position.Y, data.VertexList[0].Position.Z);
            Vertex3 Min = new Vertex3(data.VertexList[0].Position.X, data.VertexList[0].Position.Y, data.VertexList[0].Position.Z);

            foreach (Vertex i in data.VertexList)
            {
                if (i.Position.X > Max.X)
                    Max.X = i.Position.X;
                if (i.Position.Y > Max.Y)
                    Max.Y = i.Position.Y;
                if (i.Position.Z > Max.Z)
                    Max.Z = i.Position.Z;
                if (i.Position.X < Min.X)
                    Min.X = i.Position.X;
                if (i.Position.Y < Min.Y)
                    Min.Y = i.Position.Y;
                if (i.Position.Z < Min.Z)
                    Min.Z = i.Position.Z;
            }

            List<Vertex3> vList = new List<Vertex3>(data.VertexList.Count);
            foreach (Vertex v in data.VertexList)
                vList.Add(new Vertex3(v.Position.X, v.Position.Y, v.Position.Z));

            List<RenderWareFile.Triangle> tList = new List<RenderWareFile.Triangle>(data.TriangleList.Count);
            foreach (Triangle t in data.TriangleList)
                tList.Add(new RenderWareFile.Triangle((ushort)t.MaterialIndex, (ushort)t.vertex1, (ushort)t.vertex2, (ushort)t.vertex3));

            List<BinMesh> binMeshList = new List<BinMesh>();
            int TotalNumberOfTristripIndicies = 0;

            for (int i = 0; i < data.MaterialList.Count; i++)
            {
                List<int> indices = new List<int>();
                foreach (Triangle f in data.TriangleList)
                {
                    if (f.MaterialIndex == i)
                    {
                        indices.Add(f.vertex1);
                        indices.Add(f.vertex2);
                        indices.Add(f.vertex3);
                    }
                }
                TotalNumberOfTristripIndicies += indices.Count();

                binMeshList.Add(new BinMesh
                {
                    materialIndex = i,
                    indexCount = indices.Count(),
                    vertexIndices = indices.ToArray()
                });
            }

            // GENERATE COLLISION DATA

            List<ushort> TriangleIndexList = new List<ushort>();
            List<Split> splitlist = new List<Split>();
            ushort loop = 0;
            bool exitloop = false;
            Split split = new Split();
            byte trianglesPerSplit = 200;

            while (!exitloop)
            {
                split = new Split
                {
                    negativeSector = new Sector()
                    {
                        Max = Max,
                        Min = Min,
                        TriangleIndexList = new List<ushort>(),
                        splitPosition = Max.X,
                        type = SectorType.NegativeX
                    },
                    positiveSector = new Sector()
                    {
                        Max = Max,
                        Min = Min,
                        TriangleIndexList = new List<ushort>(),
                        splitPosition = Min.X,
                        type = SectorType.PositiveX
                    }
                };

                for (ushort i = (ushort)(trianglesPerSplit * loop); i < tList.Count(); i++)
                {
                    TriangleIndexList.Add(i);

                    split.negativeSector.TriangleIndexList.Add(i);

                    if (split.negativeSector.TriangleIndexList.Count() == trianglesPerSplit)
                    {
                        split.negativeSector.triangleAmount = trianglesPerSplit;
                        split.negativeSector.referenceIndex = (ushort)(trianglesPerSplit * loop);
                        loop += 1;

                        split.positiveSector.triangleAmount = 0xFF;
                        split.positiveSector.referenceIndex = loop;
                        splitlist.Add(split);
                        split = new Split();
                        exitloop = false;
                        break;
                    }

                    exitloop = true;
                }
            }

            split.negativeSector.triangleAmount = (byte)split.negativeSector.TriangleIndexList.Count();
            split.negativeSector.referenceIndex = (ushort)(trianglesPerSplit * loop);
            split.positiveSector.triangleAmount = 0;
            split.positiveSector.referenceIndex = 0;
            loop += 1;
            splitlist.Add(split);

            //Sector sector = new Sector()
            //{
            //    Max = Max,
            //    Min = Min,
            //    TriangleIndexList = new List<ushort>()
            //};

            // sector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, sector, TriangleIndexList);

            // List<ushort> TriangleIndexReferenceList = new List<ushort>();
            // List<Split> splitList = new List<Split>();

            //PositionOnList = 0;
            //SplitSector(sector, 20, 0, splitList, TriangleIndexReferenceList, vList, tList);

            // COLLISION FLAGS

            Color[] cFlagList = new Color[data.MaterialList.Count];
            for (int i = 0; i < cFlagList.Length; i++)
            {
                cFlagList[i] = new Color(0x01, 0x00, 0x02, 0x00);

                string a = data.MaterialList[i].Split('_').Last();

                if (a == "c") // ceiling
                    cFlagList[i] = new Color(0x00, 0x00, 0x00, 0x00);
                else if (a == "f") // road floor
                    cFlagList[i] = new Color(0x01, 0x00, 0x02, 0x00);
                else if (a == "fs") // stone floor
                    cFlagList[i] = new Color(0x01, 0x00, 0x00, 0x60);
                else if (a == "fm") // metal floor
                    cFlagList[i] = new Color(0x01, 0x01, 0x01, 0x10);
                else if (a == "t") // triangle jump wall
                    cFlagList[i] = new Color(0x02, 0x00, 0x00, 0x00);
                else if (a == "a") // angle wall
                    cFlagList[i] = new Color(0x02, 0x01, 0x01, 0x10);
                else if (a == "i") // invisible wall
                    cFlagList[i] = new Color(0x02, 0x02, 0x00, 0x00);
                else if (a == "g") // green goo
                    cFlagList[i] = new Color(0x05, 0x00, 0x02, 0x00);
                else if (a == "k") // barrier
                    cFlagList[i] = new Color(0x08, 0x00, 0x00, 0x00);
                else if (a == "i2") // invisible wall at distance
                    cFlagList[i] = new Color(0x10, 0x00, 0x00, 0x00);
                else if (a == "x") // death
                    cFlagList[i] = new Color(0x20, 0x00, 0x00, 0x00);
                else if (a.Count() == 8)
                    try
                    {
                        cFlagList[i] = Color.FromString(a);
                    }
                    catch
                    {
                        cFlagList[i] = new Color(0x01, 0x00, 0x02, 0x00);
                    }
            }
            
            List<Color> cFlags = new List<Color>();
            foreach (Triangle t in data.TriangleList)
                cFlags.Add(cFlagList[t.MaterialIndex]);

            // GENERATE RENDERWARE DATA

            World_000B world = new World_000B
            {
                worldStruct = new WorldStruct_0001()
                {
                    rootIsWorldSector = 1,
                    inverseOrigin = new Vertex3(-0f, -0f, -0f),
                    numTriangles = (uint)data.TriangleList.Count(),
                    numVertices = (uint)data.VertexList.Count(),
                    numPlaneSectors = 0,
                    numAtomicSectors = 1,
                    colSectorSize = 0,
                    worldFlags = WorldFlags.WorldSectorsOverlap,// | WorldFlags.ModulateMaterialColors, //(WorldFlags)0x40000040,
                    boxMaximum = Max,
                    boxMinimum = Min
                },

                materialList = new MaterialList_0008()
                {
                    materialListStruct = new MaterialListStruct_0001()
                    {
                        materialCount = data.MaterialList.Count()
                    },
                    materialList = new Material_0007[data.MaterialList.Count()]
                },

                firstWorldChunk = new AtomicSector_0009()
                {
                    atomicStruct = new AtomicSectorStruct_0001()
                    {
                        matListWindowBase = 0,
                        numTriangles = data.TriangleList.Count(),
                        numVertices = data.VertexList.Count(),
                        boxMaximum = Max,
                        boxMinimum = Min,
                        collSectorPresent = 0x0012f410,
                        unused = 0,
                        vertexArray = vList.ToArray(),
                        colorArray = null,
                        uvArray = null,
                        triangleArray = tList.ToArray()
                    },
                    atomicExtension = new Extension_0003()
                    {
                        extensionSectionList = new List<RWSection>()
                        {
                            new BinMeshPLG_050E()
                            {
                                binMeshHeaderFlags = BinMeshHeaderFlags.TriangleList,
                                numMeshes = binMeshList.Count(),
                                totalIndexCount = TotalNumberOfTristripIndicies,
                                binMeshList = binMeshList.ToArray()
                            },
                            new CollisionPLG_011D()
                            {
                                colTree = new ColTree_002C()
                                {
                                    colTreeStruct = new ColTreeStruct_0001()
                                    {
                                        useMap = 1,
                                        boxMaximum = Max,
                                        boxMinimum = Min,
                                        numSplits = splitlist.Count(),
                                        numTriangles = TriangleIndexList.Count(),
                                        splitArray = splitlist.ToArray(),
                                        triangleArray = TriangleIndexList.ToArray(),
                                    },
                                },
                                unknownValue = 0x00037002
                            },
                            new UserDataPLG_011F()
                            {
                                userDataType = 0x02,
                                unknown2 = 0x0A,
                                attribute = "attribute",
                                unknown3 = 0x01,
                                numTriangles = tList.Count(),
                                collisionFlags = cFlags.ToArray(),
                                unknown4 = 0x0D,
                                userData = "FVF.UserData",
                                unknown5 = 0x01,
                                unknown6 = 0x01,
                                unknown7 = 0x3003
                            }
                        }
                    }
                },

                worldExtension = new Extension_0003()
            };

            for (int i = 0; i < data.MaterialList.Count; i++)
            {
                world.materialList.materialList[i] = new Material_0007()
                {
                    materialStruct = new MaterialStruct_0001()
                    {
                        unusedFlags = 0,
                        color = new Color() { R = 0xFF, G = 0xFF, B = 0xFF, A = 0xFF },
                        unusedInt2 = 0x01FAE70C,
                        isTextured = 0,
                        ambient = 1f,
                        specular = 1f,
                        diffuse = 1f
                    },
                    texture = null,
                    materialExtension = new Extension_0003()
                };
            }

            return new RWSection[] { world };
        }

        private static int numberoftimes;
        private static ushort PositionOnList = 0;

        public static void SplitSector(Sector t, int MaxTrianglesOnNode, int recursion, List<Split> splitList,
            List<ushort> TriangleIndexReferenceList, List<Vertex3> vList, List<RenderWareFile.Triangle> tList)
        {
            numberoftimes++;

            Split newSplitX = new Split
            {
                negativeSector = new Sector(),
                positiveSector = new Sector()
            };

            newSplitX.negativeSector.TriangleIndexList = new List<ushort>();
            newSplitX.negativeSector.splitPosition = (t.Max.X + t.Min.X) / 2;
            newSplitX.negativeSector.Min.X = t.Min.X;
            newSplitX.negativeSector.Min.Y = t.Min.Y;
            newSplitX.negativeSector.Min.Z = t.Min.Z;
            newSplitX.negativeSector.Max.X = newSplitX.negativeSector.splitPosition;
            newSplitX.negativeSector.Max.Y = t.Max.Y;
            newSplitX.negativeSector.Max.Z = t.Max.Z;
            newSplitX.negativeSector.type = SectorType.NegativeX;
            newSplitX.negativeSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitX.negativeSector, t.TriangleIndexList);

            newSplitX.positiveSector.TriangleIndexList = new List<ushort>();
            newSplitX.positiveSector.splitPosition = (t.Max.X + t.Min.X) / 2;
            newSplitX.positiveSector.Min.X = newSplitX.positiveSector.splitPosition;
            newSplitX.positiveSector.Min.Y = t.Min.Y;
            newSplitX.positiveSector.Min.Z = t.Min.Z;
            newSplitX.positiveSector.Max.X = t.Max.X;
            newSplitX.positiveSector.Max.Y = t.Max.Y;
            newSplitX.positiveSector.Max.Z = t.Max.Z;
            newSplitX.positiveSector.type = SectorType.PositiveX;
            newSplitX.positiveSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitX.positiveSector, t.TriangleIndexList);

            //splitList.Add(newSplitX);


            Split newSplitY = new Split
            {
                negativeSector = new Sector(),
                positiveSector = new Sector()
            };
            newSplitY.negativeSector.TriangleIndexList = new List<ushort>();
            newSplitY.negativeSector.splitPosition = (t.Max.Y + t.Min.Y) / 2;
            newSplitY.negativeSector.Min.X = t.Min.X;
            newSplitY.negativeSector.Min.Y = t.Min.Y;
            newSplitY.negativeSector.Min.Z = t.Min.Z;
            newSplitY.negativeSector.Max.X = t.Max.X;
            newSplitY.negativeSector.Max.Y = newSplitY.negativeSector.splitPosition;
            newSplitY.negativeSector.Max.Z = t.Max.Z;
            newSplitY.negativeSector.type = SectorType.NegativeY;
            newSplitY.negativeSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitY.negativeSector, t.TriangleIndexList);

            newSplitY.positiveSector.TriangleIndexList = new List<ushort>();
            newSplitY.positiveSector.splitPosition = (t.Max.Y + t.Min.Y) / 2;
            newSplitY.positiveSector.Min.X = t.Min.X;
            newSplitY.positiveSector.Min.Y = newSplitY.positiveSector.splitPosition;
            newSplitY.positiveSector.Min.Z = t.Min.Z;
            newSplitY.positiveSector.Max.X = t.Max.X;
            newSplitY.positiveSector.Max.Y = t.Max.Y;
            newSplitY.positiveSector.Max.Z = t.Max.Z;
            newSplitY.positiveSector.type = SectorType.PositiveY;
            newSplitY.positiveSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitY.positiveSector, t.TriangleIndexList);

            //splitList.Add(newSplitY);


            Split newSplitZ = new Split
            {
                negativeSector = new Sector(),
                positiveSector = new Sector()
            };
            newSplitZ.negativeSector.TriangleIndexList = new List<ushort>();
            newSplitZ.negativeSector.splitPosition = (t.Max.Z + t.Min.Z) / 2;
            newSplitZ.negativeSector.Min.X = t.Min.X;
            newSplitZ.negativeSector.Min.Y = t.Min.Y;
            newSplitZ.negativeSector.Min.Z = t.Min.Z;
            newSplitZ.negativeSector.Max.X = t.Max.X;
            newSplitZ.negativeSector.Max.Y = t.Max.Y;
            newSplitZ.negativeSector.Max.Z = newSplitZ.negativeSector.splitPosition;
            newSplitZ.negativeSector.type = SectorType.NegativeZ;
            newSplitZ.negativeSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitZ.negativeSector, t.TriangleIndexList);

            newSplitZ.positiveSector.TriangleIndexList = new List<ushort>();
            newSplitZ.positiveSector.splitPosition = (t.Max.Z + t.Min.Z) / 2;
            newSplitZ.positiveSector.Min.X = t.Min.X;
            newSplitZ.positiveSector.Min.Y = t.Min.Y;
            newSplitZ.positiveSector.Min.Z = newSplitZ.positiveSector.splitPosition;
            newSplitZ.positiveSector.Max.X = t.Max.X;
            newSplitZ.positiveSector.Max.Y = t.Max.Y;
            newSplitZ.positiveSector.Max.Z = t.Max.Z;
            newSplitZ.positiveSector.type = SectorType.PositiveZ;
            newSplitZ.positiveSector.TriangleIndexList = FindTrianglesInsideNode(vList, tList, newSplitZ.positiveSector, t.TriangleIndexList);

            //splitList.Add(newSplitZ);


            int XDiff = Math.Abs(newSplitX.negativeSector.TriangleIndexList.Count - newSplitX.positiveSector.TriangleIndexList.Count);
            int YDiff = Math.Abs(newSplitY.negativeSector.TriangleIndexList.Count - newSplitY.positiveSector.TriangleIndexList.Count);
            int ZDiff = Math.Abs(newSplitZ.negativeSector.TriangleIndexList.Count - newSplitZ.positiveSector.TriangleIndexList.Count);

            int PairToUse;
            if (XDiff <= YDiff & XDiff <= ZDiff)
                PairToUse = 0;
            else if (YDiff <= ZDiff)
                PairToUse = 1;
            else
                PairToUse = 2;

            if (PairToUse == 0)
                splitList.Add(newSplitX);
            else if (PairToUse == 1)
                splitList.Add(newSplitY);
            else if (PairToUse == 2)
                splitList.Add(newSplitZ);

            if (splitList.Last().negativeSector.TriangleIndexList.Count > MaxTrianglesOnNode)
            {
                splitList.Last().negativeSector.triangleAmount = 0xFF;
                splitList.Last().negativeSector.referenceIndex = (ushort)(splitList.Count);
                SplitSector(splitList.Last().negativeSector, MaxTrianglesOnNode, recursion + 1, splitList,
                    TriangleIndexReferenceList, vList, tList);
            }
            else
            {
                splitList.Last().negativeSector.triangleAmount = (byte)splitList.Last().negativeSector.TriangleIndexList.Count();
                splitList.Last().negativeSector.referenceIndex = PositionOnList;
                TriangleIndexReferenceList.AddRange(splitList.Last().negativeSector.TriangleIndexList);
                PositionOnList = (ushort)TriangleIndexReferenceList.Count();
            }

            if (splitList.Last().positiveSector.TriangleIndexList.Count > MaxTrianglesOnNode)
            {
                splitList.Last().positiveSector.triangleAmount = 0xFF;
                splitList.Last().positiveSector.referenceIndex = (ushort)(splitList.Count);
                SplitSector(splitList.Last().positiveSector, MaxTrianglesOnNode, recursion + 1, splitList,
                    TriangleIndexReferenceList, vList, tList);
            }
            else
            {
                splitList.Last().positiveSector.triangleAmount = (byte)splitList.Last().positiveSector.TriangleIndexList.Count();
                splitList.Last().positiveSector.referenceIndex = PositionOnList;
                TriangleIndexReferenceList.AddRange(splitList.Last().positiveSector.TriangleIndexList);
                PositionOnList = (ushort)TriangleIndexReferenceList.Count();
            }
        }

        private static List<ushort> FindTrianglesInsideNode(List<Vertex3> vList, List<RenderWareFile.Triangle> tList, Sector sector, List<ushort> parentIndexList)
        {
            List<ushort> NewIndexList = new List<ushort>();
            foreach (ushort i in parentIndexList)// (ushort i = 0; i < tList.Count; i++)
            {
                if (IsTriangleInsideBox(vList, tList[i], sector))
                    NewIndexList.Add(i);
            }
            return NewIndexList;
        }

        public static bool IsTriangleInsideBox(List<Vertex3> VertexList, RenderWareFile.Triangle t, Sector sector)
        {
            if ((VertexList[t.vertex1].X >= sector.Min.X & VertexList[t.vertex1].X <= sector.Max.X) &
                (VertexList[t.vertex1].Y >= sector.Min.Y & VertexList[t.vertex1].Y <= sector.Max.Y) &
                (VertexList[t.vertex1].Z >= sector.Min.Z & VertexList[t.vertex1].Z <= sector.Max.Z))
                return true;
            if ((VertexList[t.vertex2].X >= sector.Min.X & VertexList[t.vertex2].X <= sector.Max.X) &
                (VertexList[t.vertex2].Y >= sector.Min.Y & VertexList[t.vertex2].Y <= sector.Max.Y) &
                (VertexList[t.vertex2].Z >= sector.Min.Z & VertexList[t.vertex2].Z <= sector.Max.Z))
                return true;
            if ((VertexList[t.vertex3].X >= sector.Min.X & VertexList[t.vertex3].X <= sector.Max.X) &
                (VertexList[t.vertex3].Y >= sector.Min.Y & VertexList[t.vertex3].Y <= sector.Max.Y) &
                (VertexList[t.vertex3].Z >= sector.Min.Z & VertexList[t.vertex3].Z <= sector.Max.Z))
                return true;
            return false;
        }
    }
}