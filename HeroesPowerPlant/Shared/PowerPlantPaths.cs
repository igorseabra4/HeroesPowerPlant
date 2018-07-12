using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant
{
    public class PowerPlantPaths
    {
        public struct Paths
        {
            public bool isShadow;
            public string configCCpath;
            public string levelEditorPath;
            public string visibilityPath;
            public string collisionEditorPath;
            public string layoutEditorPath;
            public string cameraEditorPath;

            public List<string> DFFONEPaths;
        }

        public static void save(string fileName)
        {
            writePathsINIfile(fileName, createPaths());
        }

        public static string saveAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "INI Files|*.ini"
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                save(saveFile.FileName);
                return saveFile.FileName;
            }
            else return null;
        }

        public static Paths createPaths()
        {
            Paths paths = new Paths
            {
                DFFONEPaths = new List<string>()
            };

            if (Program.levelEditor.GetOpenONEFilePath() != null)
            {
                paths.levelEditorPath = Program.levelEditor.GetOpenONEFilePath();
                paths.isShadow = Program.levelEditor.isShadowMode;
            }

            if (!paths.isShadow)
            {
                if (Program.configEditor.OpenConfigFileName != null)
                {
                    paths.configCCpath = Program.configEditor.OpenConfigFileName;
                }
                if (Program.levelEditor.openVisibilityFile != null)
                {
                    paths.visibilityPath = Program.levelEditor.openVisibilityFile;
                }
                if (Program.collisionEditor.GetFileName() != null)
                {
                    paths.collisionEditorPath = Program.collisionEditor.GetFileName();
                }
            }

            if (Program.layoutEditor.GetOpenFileName() != null)
            {
                paths.layoutEditorPath = Program.layoutEditor.GetOpenFileName();
            }

            if (Program.cameraEditor.currentCameraFile != null)
            {
                paths.cameraEditorPath = Program.cameraEditor.currentCameraFile;
            }

            foreach (string s in DFFRenderer.filePaths)
            {
                paths.DFFONEPaths.Add(s);
            }

            return paths;
        }

        public static void writePathsINIfile(string fileName, Paths paths)
        {
            StreamWriter iniWriter = new StreamWriter(new FileStream(fileName, FileMode.Create));

            iniWriter.WriteLine("#HeroesPowerPlant Paths File");
            
            if (paths.levelEditorPath != null)
                iniWriter.WriteLine("Level=" + paths.levelEditorPath);
            if (paths.layoutEditorPath != null)
                iniWriter.WriteLine("Layout=" + paths.layoutEditorPath);
            if (paths.cameraEditorPath != null)
                iniWriter.WriteLine("Camera=" + paths.cameraEditorPath);

            if (paths.isShadow)
            {
                iniWriter.WriteLine("Shadow=True");
            }
            else
            {
                if (paths.configCCpath != null)
                    iniWriter.WriteLine("Config=" + paths.configCCpath);
                if (paths.visibilityPath != null)
                    iniWriter.WriteLine("Visibility=" + paths.visibilityPath);
                if (paths.collisionEditorPath != null)
                    iniWriter.WriteLine("Collision=" + paths.collisionEditorPath);
            }

            foreach (string s in paths.DFFONEPaths)
                iniWriter.WriteLine("DFFPath=" + s);

            iniWriter.Close();
        }

        public static string open()
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "INI Files|*.ini"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                applyPaths(readPathsINI(openFile.FileName));
                return openFile.FileName;
            }
            else return null;
        }

        public static Paths readPathsINI(string fileName)
        {
            string[] file = File.ReadAllLines(fileName);
            Paths paths = new Paths
            {
                DFFONEPaths = new List<string>()
            };

            foreach (string s in file)
            {
                if (s.StartsWith("Config=")) paths.configCCpath = s.Split('=')[1];
                else if (s.StartsWith("Level=")) paths.levelEditorPath = s.Split('=')[1];
                else if (s.StartsWith("Visibility=")) paths.visibilityPath = s.Split('=')[1];
                else if (s.StartsWith("Collision=")) paths.collisionEditorPath = s.Split('=')[1];
                else if (s.StartsWith("Layout=")) paths.layoutEditorPath = s.Split('=')[1];
                else if (s.StartsWith("Camera=")) paths.cameraEditorPath = s.Split('=')[1];
                else if (s.StartsWith("DFFPath=")) paths.DFFONEPaths.Add(s.Split('=')[1]);
                else if (s.StartsWith("Shadow=")) paths.isShadow = true;
            }

            return paths;
        }

        public static void applyPaths(Paths paths)
        {
            if (paths.isShadow)
            {
                if (paths.levelEditorPath != null)
                {
                    if (File.Exists(paths.levelEditorPath))
                        Program.levelEditor.OpenONEFolder(paths.levelEditorPath);
                    else
                        MessageBox.Show(paths.levelEditorPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (paths.configCCpath != null)
                {
                    if (File.Exists(paths.configCCpath))
                        Program.configEditor.ConfigEditorOpen(paths.configCCpath);
                    else
                        MessageBox.Show(paths.configCCpath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (paths.levelEditorPath != null)
                {
                    if (File.Exists(paths.levelEditorPath))
                        Program.levelEditor.OpenONEFile(paths.levelEditorPath);
                    else
                        MessageBox.Show(paths.levelEditorPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                if (paths.visibilityPath != null)
                {
                    if (File.Exists(paths.visibilityPath))
                        Program.levelEditor.initVisibilityEditor(false, paths.visibilityPath);
                    else
                        MessageBox.Show(paths.visibilityPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (paths.collisionEditorPath != null)
                {
                    if (File.Exists(paths.collisionEditorPath))
                        Program.collisionEditor.Open(paths.collisionEditorPath);
                    else
                        MessageBox.Show(paths.collisionEditorPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (paths.layoutEditorPath != null)
            {
                if (File.Exists(paths.layoutEditorPath))
                    Program.layoutEditor.OpenLayoutFile(paths.layoutEditorPath);
                else
                    MessageBox.Show(paths.layoutEditorPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (paths.cameraEditorPath != null)
            {
                if (File.Exists(paths.cameraEditorPath))
                    Program.cameraEditor.open(paths.cameraEditorPath);
                else
                    MessageBox.Show(paths.cameraEditorPath + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (string s in paths.DFFONEPaths)
            {
                if (File.Exists(s))
                {
                    DFFRenderer.filePaths.Add(s);
                    DFFRenderer.AddDFFFiles(s);
                }
                else
                    MessageBox.Show(s + " could not be loaded", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
