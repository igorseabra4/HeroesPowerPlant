using static HeroesPowerPlant.CollisionEditor.CollisionFunctions;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionEditorSystem
    {
        public string CurrentCLfileName { get => CurrentCLfileName; private set => CurrentCLfileName = value; }
        public int NumVertices { get => data.numVertices; }
        public int NumTriangles { get => data.numTriangles; }
        public int NumQuadNodes { get => data.numQuadnodes; }
        public byte DepthLevel { get => data.MaxDepth; }

        private CLFile data;

        public void Import(string sourceOBJfile, byte depthLevel)
        {
            ConvertOBJtoCL(sourceOBJfile, CurrentCLfileName, depthLevel);
        }

        public void NewFile(string sourceOBJfile, string destinationCLfile, byte depthLevel)
        {
            CurrentCLfileName = destinationCLfile;
            Import(sourceOBJfile, depthLevel);
        }

        public void Open(string fileName)
        {
            CurrentCLfileName = fileName;
        }

        public void ConvertCLtoOBJ(string fileName)
        {
            CollisionFunctions.ConvertCLtoOBJ(fileName, ref data);
        }

        public void Close()
        {
            CurrentCLfileName = null;
            CollisionRendering.Dispose();
        }

        public bool HasOpenedFile()
        {
            return CurrentCLfileName != null;
        }

        public void LoadCLFile()
        {
            CollisionRendering.Dispose();

            data = CollisionRendering.LoadCLFile(CurrentCLfileName);
        }
    }
}
