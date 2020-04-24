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
        private static bool isShadow;
        public bool IsShadow => isShadow;

        private string currentlyOpenFileName;
        public string CurrentlyOpenFileName => currentlyOpenFileName;

        private static ObjectEntry[] heroesObjectEntries;
        private static ObjectEntry[] shadowObjectEntries;

        public bool autoUnkBytes;

        public static void SetupLayoutEditorSystem()
        {
            heroesObjectEntries = ReadObjectListData("Resources/Lists/HeroesObjectList.ini");
            shadowObjectEntries = ReadObjectListData("Resources/Lists/ShadowObjectList.ini");

            string extraObjectEntriesPath = "Resources/Lists/HeroesObjectListCustom.ini";
            if (File.Exists(extraObjectEntriesPath))
            {
                List<ObjectEntry> temp = heroesObjectEntries.ToList();
                temp.AddRange(ReadObjectListData(extraObjectEntriesPath));
                heroesObjectEntries = temp.ToArray();
            }
        }

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

        public static IEnumerable<ObjectEntry> GetAllObjectEntries()
        {
            List<ObjectEntry> list = new List<ObjectEntry>();
            list.AddRange(heroesObjectEntries);
            list.AddRange(shadowObjectEntries);

            return list;
        }

        public static ObjectEntry[] GetActiveObjectEntries()
        {
            if (isShadow) return shadowObjectEntries;
            else return heroesObjectEntries;
        }

        public static ObjectEntry[] HeroesObjectEntries => heroesObjectEntries;
        public static ObjectEntry[] ShadowObjectEntries => shadowObjectEntries;
        
        public (byte, byte)[] GetAllCurrentObjectEntries()
        {
            HashSet<(byte, byte)> objectEntries = new HashSet<(byte, byte)>();

            foreach (SetObject s in setObjects)
            {
                if (!objectEntries.Contains((s.List, s.Type)))
                    objectEntries.Add((s.List, s.Type));
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
                GetHeroesLayout(CurrentlyOpenFileName).ForEach(setObjects.Add);
            }
            else if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".dat")
            {
                isShadow = true;
                GetShadowLayout(CurrentlyOpenFileName).ForEach(setObjects.Add);
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
            foreach (var v in GetHeroesLayoutFromINI(fileName))
                setObjects.Add(v);
        }

        public void ImportLayoutFile(string fileName)
        {
            if (isShadow)
            {
                GetShadowLayout(fileName).ForEach(setObjects.Add);
            }
            else
            {
                if (Path.GetExtension(fileName).ToLower() == ".bin")
                {
                    GetHeroesLayout(fileName).ForEach(setObjects.Add);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".dat")
                {
                    GetHeroesLayoutFromShadow(fileName).ForEach(setObjects.Add);
                }
                else throw new InvalidDataException("Unknown file type");
            }
        }

        public void ImportOBJ(string fileName)
        {
            GetObjectsFromObjFile(fileName, 0, 3).ForEach(setObjects.Add);
        }

        public void ImportSALayout(string fileName)
        {
            foreach (var c in Other.OtherFunctions.ConvertSASetToHeroes(fileName))
                setObjects.Add(c);
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
                if (s.List == 0x01 & s.Type == 0xFF)
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
            byte currentNum = 0;

            if (isShadow)
            {
                if (!string.IsNullOrEmpty(currentlyOpenFileName))
                {
                    if (currentlyOpenFileName.ToLower().Contains("cmn")) currentNum = 0x10;
                    else if (currentlyOpenFileName.ToLower().Contains("nrm")) currentNum = 0x20;
                    else if (currentlyOpenFileName.ToLower().Contains("hrd")) currentNum = 0x40;
                    else if (currentlyOpenFileName.ToLower().Contains("ds1")) currentNum = 0x80;
                }

                var unkBytes = new List<byte>() { 1, currentNum };
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0x80 });
                unkBytes.AddRange(new byte[] { 1, currentNum });
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0 });

                setObjects.Add(CreateShadowObject(0, 0, Position, Vector3.Zero, 0, 10, 0, unkBytes.ToArray()));
            }
            else
            {
                if (!string.IsNullOrEmpty(currentlyOpenFileName))
                {
                    if (currentlyOpenFileName.Contains("P1")) currentNum = 0x20;
                    else if (currentlyOpenFileName.Contains("P2")) currentNum = 0x40;
                    else if (currentlyOpenFileName.Contains("P3")) currentNum = 0x60;
                    else if (currentlyOpenFileName.Contains("P4")) currentNum = 0x80;
                    else if (currentlyOpenFileName.Contains("P5")) currentNum = 0xA0;
                }

                var unkBytes = new byte[8] { 0, 2, currentNum, 9, 0, 2, currentNum, 9 };

                setObjects.Add(CreateHeroesObject(0, 0, Position, Vector3.Zero, 0, 10, unkBytes));
            }
        }

        public void DuplicateSetObject(int index)
        {
            PasteSetObject(JsonConvert.SerializeObject(GetSetObjectAt(index)));
        }

        public void DuplicateSetObjectAt(int index, Vector3 Position)
        {
            PasteSetObject(JsonConvert.SerializeObject(GetSetObjectAt(index)));
            setObjects.Last().Position = Position;
            setObjects.Last().CreateTransformMatrix();
        }

        public void CopySetObject(int index)
        {
            Clipboard.SetText(JsonConvert.SerializeObject(GetSetObjectAt(index)));
        }

        public void PasteSetObject(string text = null)
        {
            if (text == null)
                text = Clipboard.GetText();

            try
            {
                SetObject src;
                SetObject dest;
                if (isShadow)
                {
                    src = JsonConvert.DeserializeObject<Object_ShadowDefault>(text);
                    dest = CreateShadowObject(src.List, src.Type, src.Position, src.Rotation, src.Link, src.Rend, src.MiscSettingCount, src.UnkBytes);
                }
                else
                {
                    src = JsonConvert.DeserializeObject<Object_HeroesDefault>(text);
                    dest = CreateHeroesObject(src.List, src.Type, src.Position, src.Rotation, src.Link, src.Rend, src.UnkBytes);
                }
                dest.MiscSettings = src.MiscSettings;
                dest.CreateTransformMatrix();
                setObjects.Add(dest);
            }
            catch
            {
                MessageBox.Show($"Error pasting object from clipboard. Are you sure you have a {(isShadow ? "Shadow The Hedgehog" : "Sonic Heroes")} object copied?");
            }
        }

        public void RemoveSetObject(int index)
        {
            setObjects.RemoveAt(index);
        }

        public void ClearList()
        {
            setObjects.Clear();
        }

        public void ChangeObjectType(int index, ObjectEntry newEntry)
        {
            SetObject current = GetSetObjectAt(index);

            Vector3 pos = current.Position;
            Vector3 rot = current.Rotation;
            byte link = current.Link;
            byte rend = current.Rend;
            int msc = current.MiscSettingCount;
            byte[] unkb = current.UnkBytes;
            bool isSelected = current.isSelected;
            
            if (isShadow)
                setObjects[index] = CreateShadowObject(newEntry.List, newEntry.Type, pos, rot, link, rend, msc, unkb);
            else
                setObjects[index] = CreateHeroesObject(newEntry.List, newEntry.Type, pos, rot, link, rend, unkb);
            
            setObjects[index].isSelected = isSelected;
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

        public void CopyMisc(int index)
        {
            Clipboard.SetText(JsonConvert.SerializeObject(GetSetObjectAt(index).MiscSettings));
        }

        public void PasteMisc(int index)
        {
            try
            {
                byte[] misc = JsonConvert.DeserializeObject<byte[]>(Clipboard.GetText());
                GetSetObjectAt(index).MiscSettings = misc;
                GetSetObjectAt(index).CreateTransformMatrix();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error pasting misc. settings: " + ex.Message);
            }
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
        
        public void ScreenClicked(Vector3 camPos, Ray r, bool seeAllObjects, int currentlySelectedIndex, out int index, out float smallerDistance)
        {
            index = -1;
            smallerDistance = 40000f;

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
            sorted = sorted.OrderBy(f => f.Type).ToList();
            sorted = sorted.OrderBy(f => f.List).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
        }

        public void SortObjectsByDistance()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.GetDistanceFrom()).ToList();
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
