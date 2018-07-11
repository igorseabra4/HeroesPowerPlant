using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeroesPowerPlant.CollisionEditor.CollisionFunctions;
using static HeroesPowerPlant.CollisionEditor.CollisionRendering;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionEditorSystem
    {
        public string CurrentCLfileName { get => CurrentCLfileName; private set => CurrentCLfileName = value; }
        public int NumVertices { get => NumVertices; private set => NumVertices = value; }
        public int NumTriangles { get => NumTriangles; private set => NumTriangles = value; }
        public int NumQuadNodes { get => NumQuadNodes; private set => NumQuadNodes = value; }
        public byte DepthLevel { get; private set; }

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
            ConvertCLtoOBJ(fileName);
        }

        public void Close()
        {
            CurrentCLfileName = null;
            if (collisionMesh != null)
                collisionMesh.Dispose();
        }

        public bool HasOpenedFile()
        {
            return CurrentCLfileName != null;
        }

        public void LoadCLFile()
        {
            if (collisionMesh != null)
                collisionMesh.Dispose();

            CLFile data = CollisionRendering.LoadCLFile(CurrentCLfileName);

            NumVertices = data.numVertices;
            NumTriangles = data.numTriangles;
            NumQuadNodes = data.numQuadnodes;
            DepthLevel = data.MaxDepth;
        }
    }
}
