using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.LayoutEditor.LayoutEditorFunctions;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public class LayoutEditorSystem
    {
        private bool isShadow;
        public bool IsShadow => isShadow;

        private string currentlyOpenFileName;
        public string CurrentlyOpenFileName => currentlyOpenFileName;

        private static ObjectEntry[] heroesObjectEntries = ReadObjectListData("Resources\\Lists\\HeroesObjectList.ini");
        private static ObjectEntry[] shadowObjectEntries = ReadObjectListData("Resources\\Lists\\ShadowObjectList.ini");

        public bool autoUnkBytes;

        public void BindControl(ListControl listControl)
        {
            listControl.DataSource = setObjects;
        }

        private BindingList<SetObject> setObjects { get; set; } = new BindingList<SetObject>();
        
        public int GetSetObjectAmount()
        {
            return setObjects.Count;
        }

        public SetObject GetSetObjectAt(int index)
        {
            return setObjects[index];
        }

        public ObjectEntry GetSetObjectEntry(int index)
        {
            return setObjects[index].objectEntry;
        }
        
        public static IEnumerable<ObjectEntry> GetAllObjectEntries()
        {
            List<ObjectEntry> list = new List<ObjectEntry>();
            list.AddRange(heroesObjectEntries);
            list.AddRange(shadowObjectEntries);

            return list;
        }

        public ObjectEntry[] GetActiveObjectEntries()
        {
            if (isShadow) return shadowObjectEntries;
            else return heroesObjectEntries;
        }

        public static ObjectEntry[] HeroesObjectEntries => heroesObjectEntries;
        public static ObjectEntry[] ShadowObjectEntries => shadowObjectEntries;
        
        public ObjectEntry[] GetAllCurrentObjectEntries()
        {
            HashSet<ObjectEntry> objectEntries = new HashSet<ObjectEntry>();

            foreach (SetObject s in setObjects)
            {
                if (!objectEntries.Contains(s.objectEntry))
                    objectEntries.Add(s.objectEntry);
            }

            return objectEntries.ToArray();
        }

        public void NewHeroesLayout()
        {
            isShadow = false;
            currentlyOpenFileName = null;
            setObjects.Clear();
        }

        public void NewShadowLayout()
        {
            isShadow = true;
            currentlyOpenFileName = null;
            setObjects.Clear();
        }

        #region Import/Export Methods

        public void OpenLayoutFile(string fileName)
        {
            currentlyOpenFileName = fileName;
            setObjects.Clear();

            if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".bin")
            {
                isShadow = false;
                GetHeroesLayout(CurrentlyOpenFileName, heroesObjectEntries).ForEach(setObjects.Add);
            }
            else if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".dat")
            {
                isShadow = true;
                GetShadowLayout(CurrentlyOpenFileName, shadowObjectEntries).ForEach(setObjects.Add);
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
                    SaveShadowLayout(setObjects.Cast<SetObjectShadow>(), CurrentlyOpenFileName, autoUnkBytes);
                else
                    SaveHeroesLayout(setObjects.Cast<SetObjectHeroes>(), CurrentlyOpenFileName, autoUnkBytes);
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
            foreach (var v in GetHeroesLayoutFromINI(fileName, heroesObjectEntries))
                setObjects.Add(v);
        }

        public void ImportLayoutFile(string fileName)
        {
            if (isShadow)
            {
                GetShadowLayout(fileName, shadowObjectEntries).ForEach(setObjects.Add);
            }
            else
            {
                if (Path.GetExtension(fileName).ToLower() == ".bin")
                {
                    GetHeroesLayout(fileName, heroesObjectEntries).ForEach(setObjects.Add);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".dat")
                {
                    GetHeroesLayoutFromShadow(fileName, heroesObjectEntries, shadowObjectEntries).ForEach(setObjects.Add);
                }
                else throw new InvalidDataException("Unknown file type");
            }
        }

        public void ImportOBJ(string fileName)
        {
            SetObjectHeroes ring = new SetObjectHeroes(0, 3, heroesObjectEntries, Vector3.Zero, Vector3.Zero, 0, 0);
            GetObjectsFromObjFile(fileName, ring.objectEntry).ForEach(setObjects.Add);
        }

        #endregion

        #region View/Rendering Methods

        public void ViewHere(int index)
        {
               Program.MainForm.renderer.Camera.SetPosition(GetSetObjectAt(index).Position - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        public void UpdateAllMatrices()
        {
            foreach (SetObject s in setObjects)
                s.CreateTransformMatrix();
        }

        public void RenderSetObjects(SharpRenderer renderer, bool drawEveryObject)
        {
            foreach (SetObject s in setObjects)
                if (renderer.frustum.Intersects(ref s.boundingBox))
                    s.Draw(renderer, drawEveryObject);
        }

        public void UpdateSetParticleMatrices()
        {
            foreach (SetObject s in setObjects)
                if (s.objectEntry.List == 0x01 & s.objectEntry.Type == 0xFF)
                    s.CreateTransformMatrix();
        }
        #endregion

        #region Object Editing Methods

        public void SelectedIndexChanged(int index)
        {
            for (int i = 0; i < setObjects.Count; i++)
                setObjects[i].isSelected = false;

            if (index > -1)
                GetSetObjectAt(index).isSelected = true;
        }

        public void AddNewSetObject()
        {
            Vector3 Position = Program.MainForm.renderer.Camera.GetPosition() + 100 * Program.MainForm.renderer.Camera.GetForward();
            SetObject newObject;
            
            if (isShadow)
            {
                byte currentNum;

                if (currentlyOpenFileName.ToLower().Contains("cmn")) currentNum = 0x10;
                else if (currentlyOpenFileName.ToLower().Contains("nrm")) currentNum = 0x20;
                else if (currentlyOpenFileName.ToLower().Contains("hrd")) currentNum = 0x40;
                else if (currentlyOpenFileName.ToLower().Contains("ds1")) currentNum = 0x80;
                else currentNum = 0;

                var unkBytes = new List<byte>() { 1, currentNum };
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0x80 });
                unkBytes.AddRange(new byte[] { 1, currentNum });
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0 });

                newObject = new SetObjectShadow(0, 0, shadowObjectEntries, Position, Vector3.Zero, 0, 10, 0, unkBytes.ToArray());
            }
            else
            {
                byte currentNum;

                if (currentlyOpenFileName.Contains("P1")) currentNum = 0x20;
                else if (currentlyOpenFileName.Contains("P2")) currentNum = 0x40;
                else if (currentlyOpenFileName.Contains("P3")) currentNum = 0x60;
                else if (currentlyOpenFileName.Contains("P4")) currentNum = 0x80;
                else if (currentlyOpenFileName.Contains("P5")) currentNum = 0xA0;
                else currentNum = 0;

                var unkBytes = new byte[8] { 0, 2, currentNum, 9, 0, 2, currentNum, 9 };

                newObject = new SetObjectHeroes(0, 0, heroesObjectEntries, Position, Vector3.Zero, 0, 10, unkBytes);
            }

            newObject.CreateTransformMatrix();
            setObjects.Add(newObject);
        }

        public void CopySetObject(int index)
        {
            SetObject original = GetSetObjectAt(index);
            CopySetObject(index, original.Position);
        }

        public void CopySetObject(int index, Vector3 Position)
        {
            SetObject original = GetSetObjectAt(index);
            SetObject destination;

            if (isShadow)
                destination = JsonConvert.DeserializeObject<SetObjectShadow>(JsonConvert.SerializeObject(original));
            else
                destination = JsonConvert.DeserializeObject<SetObjectHeroes>(JsonConvert.SerializeObject(original));

            destination.objectEntry = original.objectEntry;
            destination.Position = Position;

            destination.FindNewObjectManager(false);
            destination.CreateTransformMatrix();

            setObjects.Add(destination);
        }

        public void RemoveSetObject(int index)
        {
            setObjects.RemoveAt(index);
        }

        public void ClearList()
        {
            setObjects.Clear();
        }

        public void ComboBoxObjectChanged(int index, ObjectEntry newEntry)
        {
            SetObject current = GetSetObjectAt(index);

            current.objectEntry = newEntry;
            current.FindNewObjectManager();
            current.CreateTransformMatrix();
        }

        public void SetObjectPosition(int index, float x, float y, float z)
        {
            SetObjectPosition(index, new Vector3(x, y, z));
        }

        public void SetObjectPosition(int index, Vector3 v)
        {
            GetSetObjectAt(index).Position = v;
            GetSetObjectAt(index).CreateTransformMatrix();
        }

        public void SetObjectRotation(int index, float x, float y, float z)
        {
            if (isShadow)
                GetSetObjectAt(index).Rotation = new Vector3(x, y, z);
            else
                GetSetObjectAt(index).Rotation = new Vector3(DegreesToBAMS(x), DegreesToBAMS(y), DegreesToBAMS(z));

            GetSetObjectAt(index).CreateTransformMatrix();
        }

        private void SetObjectRotationDefault(int index, float x, float y, float z)
        {
            SetObjectRotationDefault(index, new Vector3(x, y, z));
        }

        private void SetObjectRotationDefault(int index, Vector3 v)
        {
            GetSetObjectAt(index).Rotation = v;
            GetSetObjectAt(index).CreateTransformMatrix();
        }

        public void SetObjectLink(int index, byte value)
        {
            GetSetObjectAt(index).Link = value;
        }

        public void SetObjectRend(int index, byte value)
        {
            GetSetObjectAt(index).Rend = value;
        }

        public void SetUnkBytes(int index, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6, byte v7, byte v8)
        {
            GetSetObjectAt(index).UnkBytes = new byte[] { v1, v2, v3, v4, v5, v6, v7, v8 };
        }        

        public void Drop(int index)
        {
            GetSetObjectAt(index).Position = Program.MainForm.LevelEditor.bspRenderer.GetDroppedPosition(GetSetObjectAt(index).Position);
            GetSetObjectAt(index).CreateTransformMatrix();
        }

        public void DropToCurrentView(int index)
        {
            GetSetObjectAt(index).Position = Program.MainForm.renderer.Camera.GetPosition() + 200 * Program.MainForm.renderer.Camera.GetForward();
            GetSetObjectAt(index).CreateTransformMatrix();
        }

        #endregion

        #region GUI Return Methods

        public decimal GetPosX(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.X;
        }

        public decimal GetPosY(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.Y;
        }

        public decimal GetPosZ(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.Z;
        }

        public decimal GetRotX(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.X;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.X);
        }

        public decimal GetRotY(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.Y;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.Y);
        }

        public decimal GetRotZ(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.Z;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.Z);
        }

        public decimal GetObjectLink(int index)
        {
            return GetSetObjectAt(index).Link;
        }

        public decimal GetObjectRend(int index)
        {
            return GetSetObjectAt(index).Rend;
        }

        public byte[] GetUnkBytes(int index)
        {
            return GetSetObjectAt(index).UnkBytes;
        }
        
        public SetObjectManager GetObjectManager(int index)
        {
            if (isShadow)
                return ((SetObjectShadow)GetSetObjectAt(index)).objectManager;
            else
                return ((SetObjectHeroes)GetSetObjectAt(index)).objectManager;
        }

        public int ScreenClicked(Vector3 camPos, Ray r, bool seeAllObjects, int currentlySelectedIndex)
        {
            int index = -1;

            float smallerDistance = 10000f;
            for (int i = 0; i < setObjects.Count; i++)
            {
                if (setObjects[i].isSelected || (!seeAllObjects && setObjects[i].DontDraw(camPos)))
                    continue;

                if (setObjects[i].IntersectsWith(r, out float distance))
                    if (distance < smallerDistance)
                    {
                        smallerDistance = distance;
                        index = i;
                    }
            }

            return index;
        }

        public void GetClickedModelPosition(Ray ray, Vector3 camPos, bool seeAllObjects, out bool hasIntersected, out float smallestDistance)
        {
            hasIntersected = false;
            smallestDistance = 40000f;

            foreach (SetObject s in setObjects)
                if ((seeAllObjects || !s.DontDraw(camPos)) && s.IntersectsWith(ray, out float distance))
                {
                    hasIntersected = true;
                    if (distance < smallestDistance)
                        smallestDistance = distance;
                }
            
        }

        public int FindNext(int index)
        {
            if (index != setObjects.Count - 1)
                for (int i = index + 1; i < setObjects.Count; i++)
                    if (setObjects[i].Link == GetSetObjectAt(index).Link)
                        return i;

            for (int i = 0; i < index; i++)
                if (setObjects[i].Link == GetSetObjectAt(index).Link)
                    return i;

            return index;
        }
        #endregion

        #region Sorting Methods
        public void SortObjectsByID()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.Link).ToList();
            sorted = sorted.OrderBy(f => f.objectEntry.Type).ToList();
            sorted = sorted.OrderBy(f => f.objectEntry.List).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
        }

        public void SortObjectsByDistance()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.GetDistanceFromOrigin()).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
        }
        #endregion

        #region Memory Functions

        public bool GetSpeedMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer0Position());
                return true;
            }
            return false;
        }

        public bool GetFlyMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer1Position());
                return true;
            }
            return false;
        }

        public bool GetPowMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer2Position());
                return true;
            }
            return false;
        }

        public bool GetSpeedRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer0Rotation());
                return true;
            }
            return false;
        }

        public bool GetFlyRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer1Rotation());
                return true;
            }
            return false;
        }

        public bool GetPowRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer2Rotation());
                return true;
            }
            return false;
        }

        public bool Teleport(int index)
        {
            return MemoryFunctions.Teleport(GetSetObjectAt(index).Position);
        }
        #endregion
    }
}
