using SharpDX;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static HeroesPowerPlant.LayoutEditor.LayoutEditorFunctions;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public class LayoutEditorSystem
    {
        private bool isShadow;
        public bool IsShadow { get => isShadow; }

        private int currentlySelectedIndex = -1;
        public int CurrentlySelectedIndex { get => currentlySelectedIndex; }

        private string currentlyOpenFileName;
        public string CurrentlyOpenFileName { get => currentlyOpenFileName; }

        private ObjectEntry[] heroesObjectEntries;
        private ObjectEntry[] shadowObjectEntries;

        private List<SetObject> setObjects = new List<SetObject>();

        public LayoutEditorSystem()
        {
            heroesObjectEntries = ReadObjectListData("Resources\\Lists\\HeroesObjectList.ini");
            shadowObjectEntries = ReadObjectListData("Resources\\Lists\\ShadowObjectList.ini");
        }

        public int GetSetObjectAmount()
        {
            return setObjects.Count;
        }

        public SetObject GetSetObjectAt(int i)
        {
            return setObjects[i];
        }

        public SetObject GetSelectedObject()
        {
            if (CurrentlySelectedIndex < 0)
                return null;

            return setObjects[CurrentlySelectedIndex];
        }

        public ObjectEntry GetSelectedSetObjectEntry()
        {
            if (CurrentlySelectedIndex < 0)
                return null;
            return setObjects[CurrentlySelectedIndex].objectEntry;
        }

        public int GetSelectedIndex()
        {
            return CurrentlySelectedIndex;
        }

        public ObjectEntry[] GetActiveObjectEntries()
        {
            if (isShadow) return shadowObjectEntries;
            else return heroesObjectEntries;
        }

        public void NewHeroesLayout()
        {
            isShadow = false;
            currentlyOpenFileName = null;
            currentlySelectedIndex = -1;
            setObjects = new List<SetObject>();
        }

        public void NewShadowLayout()
        {
            isShadow = true;
            currentlyOpenFileName = null;
            currentlySelectedIndex = -1;
            setObjects = new List<SetObject>();
        }

        #region Import/Export Methods

        public void OpenLayoutFile(string fileName)
        {
            currentlyOpenFileName = fileName;
            setObjects.Clear();

            if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".bin")
            {
                isShadow = false;
                setObjects.AddRange(GetHeroesLayout(CurrentlyOpenFileName, heroesObjectEntries).ToArray());
            }
            else if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".dat")
            {
                isShadow = true;
                setObjects.AddRange(GetShadowLayout(CurrentlyOpenFileName, shadowObjectEntries).ToArray());
            }
            else throw new InvalidDataException("Unknown file type");
        }

        public void Save(string fileName)
        {
            currentlyOpenFileName = fileName;
            Save();
        }

        public bool Save()
        {
            if (CurrentlyOpenFileName != null)
            {
                if (IsShadow)
                    SaveShadowLayout(setObjects.Cast<SetObjectShadow>(), CurrentlyOpenFileName);
                else
                    SaveHeroesLayout(setObjects.Cast<SetObjectHeroes>(), CurrentlyOpenFileName);
                return true;
            }
            else return false;
        }

        public void SaveINI(string fileName)
        {
            SaveHeroesLayoutINI(setObjects.Cast<SetObjectHeroes>(), fileName);
        }

        public void ImportINI(string fileName)
        {
            setObjects.AddRange(GetHeroesLayoutFromINI(fileName, heroesObjectEntries));
        }

        public void ImportLayoutFile(string fileName)
        {
            if (isShadow)
            {
                setObjects.AddRange(GetShadowLayout(fileName, shadowObjectEntries).ToArray());
            }
            else
            {
                if (Path.GetExtension(fileName).ToLower() == ".bin")
                {
                    setObjects.AddRange(GetHeroesLayout(fileName, heroesObjectEntries).ToArray());
                }
                else if (Path.GetExtension(fileName).ToLower() == ".dat")
                {
                    setObjects.AddRange(GetHeroesLayoutFromShadow(fileName, heroesObjectEntries, shadowObjectEntries).ToArray());
                }
                else throw new InvalidDataException("Unknown file type");
            }
        }

        public void ImportOBJ(string fileName)
        {
            SetObjectHeroes ring = new SetObjectHeroes(0, 3, heroesObjectEntries, Vector3.Zero, Vector3.Zero, 0, 0);
            setObjects.AddRange(GetObjectsFromObjFile(fileName, ring.objectEntry).ToArray());
        }

        #endregion

        #region View/Rendering Methods

        public IEnumerable<ObjectEntry> GetAllObjectEntries()
        {
            List<ObjectEntry> list = new List<ObjectEntry>();
            list.AddRange(heroesObjectEntries);
            list.AddRange(shadowObjectEntries);

            return list;
        }

        public void ViewHere()
        {
            if (CurrentlySelectedIndex != -1)
                SharpRenderer.Camera.SetPosition(GetSelectedObject().Position - 200 * SharpRenderer.Camera.GetForward());
        }

        public void ResetMatrices()
        {
            foreach (SetObject s in setObjects)
                s.CreateTransformMatrix();
        }

        public void RenderAllSetObjects(bool drawEveryObject)
        {
            foreach (SetObject s in setObjects)
                if (SharpRenderer.frustum.Intersects(ref s.boundingBox))
                    s.Draw(drawEveryObject);
        }

        #endregion

        #region Object Editing Methods

        public void SelectedIndexChanged(int selectedIndex)
        {
            if (CurrentlySelectedIndex > 0 & CurrentlySelectedIndex < setObjects.Count)
                GetSelectedObject().isSelected = false;

            currentlySelectedIndex = selectedIndex;

            if (currentlySelectedIndex < 0)
                return;

            GetSelectedObject().isSelected = true;
        }

        public void AddNewSetObject()
        {
            if (isShadow)
            {
                SetObjectShadow newObject = new SetObjectShadow(0, 0, shadowObjectEntries, SharpRenderer.Camera.GetPosition() + 100 * SharpRenderer.Camera.GetForward(), Vector3.Zero, 0, 10, 0);
                newObject.CreateTransformMatrix();

                setObjects.Add(newObject);
            }
            else
            {
                SetObjectHeroes newObject = new SetObjectHeroes(0, 0, heroesObjectEntries, SharpRenderer.Camera.GetPosition() + 100 * SharpRenderer.Camera.GetForward(), Vector3.Zero, 0, 10);
                newObject.CreateTransformMatrix();

                setObjects.Add(newObject);
            }
        }

        public bool CopySetObject()
        {
            if (currentlySelectedIndex < 0)
                return false;

            SetObject original = GetSelectedObject();
            SetObject destination;

            if (isShadow)
            {
                destination = new SetObjectShadow(original.objectEntry, original.Position, original.Rotation, original.Link, original.Rend, (original as SetObjectShadow).MiscSettingCount);

                for (int i = 0; i < (destination as SetObjectShadow).objectManager.MiscSettings.Length; i++)
                    (destination as SetObjectShadow).objectManager.MiscSettings[i] = (original as SetObjectShadow).objectManager.MiscSettings[i];
            }
            else
            {
                destination = new SetObjectHeroes(original.objectEntry, original.Position, original.Rotation, original.Link, original.Rend);

                for (int i = 0; i < (destination as SetObjectHeroes).objectManager.MiscSettings.Length; i++)
                    (destination as SetObjectHeroes).objectManager.MiscSettings[i] = (original as SetObjectHeroes).objectManager.MiscSettings[i];
            }

            destination.CreateTransformMatrix();

            setObjects.Add(destination);
            return true;
        }

        public int RemoveSetObject()
        {
            setObjects.RemoveAt(currentlySelectedIndex);
            return currentlySelectedIndex;
        }

        public void ClearList()
        {
            setObjects.Clear();
            currentlySelectedIndex = -1;
        }

        public void ComboBoxObjectChanged(ObjectEntry entry)
        {
            if (currentlySelectedIndex < 0)
                return;

            SetObject current = GetSelectedObject();

            current.objectEntry = entry;
            current.FindNewObjectManager();
            current.CreateTransformMatrix();
        }

        public void SetObjectPosition(float x, float y, float z)
        {
            if (currentlySelectedIndex < 0) return;
            
            SetObjectPosition(new Vector3(x, y, z));
        }

        public void SetObjectPosition(Vector3 v)
        {
            if (currentlySelectedIndex < 0) return;

            GetSelectedObject().Position = v;
            GetSelectedObject().CreateTransformMatrix();
        }

        public void SetObjectRotation(float x, float y, float z)
        {
            if (currentlySelectedIndex < 0) return;

            if (isShadow)
                GetSelectedObject().Rotation = new Vector3(x, y, z);
            else
                GetSelectedObject().Rotation = new Vector3(DegreesToBAMS(x), DegreesToBAMS(y), DegreesToBAMS(z));

            GetSelectedObject().CreateTransformMatrix();
        }

        private void SetObjectRotationDefault(float x, float y, float z)
        {
            if (currentlySelectedIndex < 0) return;

            SetObjectRotationDefault(new Vector3(x, y, z));
        }

        private void SetObjectRotationDefault(Vector3 v)
        {
            if (currentlySelectedIndex < 0) return;
            GetSelectedObject().Rotation = v;
            GetSelectedObject().CreateTransformMatrix();
        }

        public void SetObjectLink(byte value)
        {
            if (currentlySelectedIndex < 0) return;
            GetSelectedObject().Link = value;
        }

        public void SetObjectRend(byte value)
        {
            if (currentlySelectedIndex < 0) return;
            GetSelectedObject().Rend = value;
        }

        public void Drop()
        {
            if (currentlySelectedIndex < 0) return;

            Ray ray = new Ray(GetSelectedObject().Position, Vector3.Down);
            float smallerDistance = 10000f;
            bool change = false;

            for (int i = 0; i < BSPRenderer.BSPStream.Count; i++)
            {
                foreach (RenderWareFile.RWSection rw in BSPRenderer.BSPStream[i].GetAsRWSectionArray())
                {
                    if (rw is RenderWareFile.Sections.World_000B world)
                    {
                        if (GetSelectedObject().Position.X < world.worldStruct.boxMinimum.X |
                            GetSelectedObject().Position.Y < world.worldStruct.boxMinimum.Y |
                            GetSelectedObject().Position.Z < world.worldStruct.boxMinimum.Z |
                            GetSelectedObject().Position.X > world.worldStruct.boxMaximum.X |
                            GetSelectedObject().Position.Y > world.worldStruct.boxMaximum.Y |
                            GetSelectedObject().Position.Z > world.worldStruct.boxMaximum.Z) continue;
                    }
                }

                foreach (RenderWareFile.Triangle t in BSPRenderer.BSPStream[i].triangleList)
                {
                    Vector3 v1 = BSPRenderer.BSPStream[i].vertexList[t.vertex1];
                    Vector3 v2 = BSPRenderer.BSPStream[i].vertexList[t.vertex2];
                    Vector3 v3 = BSPRenderer.BSPStream[i].vertexList[t.vertex3];

                    if (ray.Intersects(ref v1, ref v2, ref v3, out float distance))
                        if (distance < smallerDistance)
                        {
                            smallerDistance = distance;
                            change = true;
                        }
                }
            }

            if (change)
                GetSelectedObject().Position.Y -= smallerDistance;
        }

        #endregion

        #region GUI Return Methods

        public decimal GetPosX()
        {
            return (decimal)GetSelectedObject().Position.X;
        }

        public decimal GetPosY()
        {
            return (decimal)GetSelectedObject().Position.Y;
        }

        public decimal GetPosZ()
        {
            return (decimal)GetSelectedObject().Position.Z;
        }

        public decimal GetRotX()
        {
            if (isShadow)
                return (decimal)GetSelectedObject().Rotation.X;
            else
                return (decimal)BAMStoDegrees(GetSelectedObject().Rotation.X);
        }

        public decimal GetRotY()
        {
            if (isShadow)
                return (decimal)GetSelectedObject().Rotation.Y;
            else
                return (decimal)BAMStoDegrees(GetSelectedObject().Rotation.Y);
        }

        public decimal GetRotZ()
        {
            if (isShadow)
                return (decimal)GetSelectedObject().Rotation.Z;
            else
                return (decimal)BAMStoDegrees(GetSelectedObject().Rotation.Z);
        }

        public decimal GetSelectedObjectLink()
        {
            return GetSelectedObject().Link;
        }

        public decimal GetSelectedObjectRend()
        {
            return GetSelectedObject().Rend;
        }

        public SetObjectManager GetSelectedObjectManager()
        {
            if (CurrentlySelectedIndex < 0)
                return null;

            if (isShadow)
                return ((SetObjectShadow)GetSelectedObject()).objectManager;
            else
                return ((SetObjectHeroes)GetSelectedObject()).objectManager;
        }

        public int ScreenClicked(Ray r)
        {
            int index = currentlySelectedIndex;

            float smallerDistance = 10000f;
            for (int i = 0; i < setObjects.Count; i++)
            {
                if (setObjects[i].isSelected) continue;

                float? distance = setObjects[i].IntersectsWith(r);
                if (distance != null)
                    if (distance < smallerDistance)
                    {
                        smallerDistance = (float)distance;
                        index = i;
                    }
            }

            return index;
        }

        public int FindNext()
        {
            if (currentlySelectedIndex != setObjects.Count - 1)
                for (int i = currentlySelectedIndex + 1; i < setObjects.Count; i++)
                    if (setObjects[i].Link == GetSelectedObject().Link)
                        return i;

            for (int i = 0; i < currentlySelectedIndex; i++)
                if (setObjects[i].Link == GetSelectedObject().Link)
                    return i;

            return currentlySelectedIndex;
        }
        #endregion

        #region Sorting Methods
        public void SortObjectsByID()
        {
            setObjects.OrderBy(f => f.GetTypeAsOne()).ToList();
        }

        public void SortObjectsByDistance()
        {
            setObjects.OrderBy(f => f.GetDistanceFromOrigin()).ToList();
        }
        #endregion

        #region Memory Functions

        public bool GetSpeedMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(MemoryFunctions.GetPlayer0Position());
                return true;
            }
            return false;
        }

        public bool GetFlyMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(MemoryFunctions.GetPlayer1Position());
                return true;
            }
            return false;
        }

        public bool GetPowMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(MemoryFunctions.GetPlayer2Position());
                return true;
            }
            return false;
        }

        public bool GetSpeedRotMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(MemoryFunctions.GetPlayer0Rotation());
                return true;
            }
            return false;
        }

        public bool GetFlyRotMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(MemoryFunctions.GetPlayer1Rotation());
                return true;
            }
            return false;
        }

        public bool GetPowRotMemory()
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(MemoryFunctions.GetPlayer2Rotation());
                return true;
            }
            return false;
        }

        public bool Teleport()
        {
            return MemoryFunctions.Teleport(GetSelectedObject().Position.X, GetSelectedObject().Position.Y, GetSelectedObject().Position.Z);
        }
        #endregion
    }
}
