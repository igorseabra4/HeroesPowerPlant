using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000E_Checkpoint : SetObjectHeroes
    {
        private string checkpointbase = "OBJ_CHECKP";
        private string checkpointstar = "OBJ_STAR";
        private string checkpointeffect = "EFF_CHECKPOINT";

        public override void Draw(SharpRenderer renderer)
        {
            if (renderer.dffRenderer.DFFModels.ContainsKey(checkpointbase + ".DFF"))
            {
                Draw(renderer, checkpointbase + ".DFF", false);
            }

            if (renderer.dffRenderer.DFFModels.ContainsKey(checkpointbase + "_EF" + ".DFF"))
            {
                Draw(renderer, checkpointbase + "_EF" + ".DFF", true);
            }

            if (renderer.dffRenderer.DFFModels.ContainsKey(checkpointstar + ".DFF"))
            {
                Draw(renderer, checkpointstar + ".DFF", false);
            }

            if (renderer.dffRenderer.DFFModels.ContainsKey(checkpointeffect + ".DFF"))
            {
                Draw(renderer, checkpointeffect + ".DFF", true);
            }
        }

        protected void Draw(SharpRenderer renderer, string modelName, bool IsLight)
        {
            SetRendererStates(renderer);

            if (IsLight)
            {
                renderer.Device.SetBlendStateAdditive();
                renderer.Device.SetCullModeNone();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();
            }

            renderData.worldViewProjection = Matrix.Scaling(1.25f) * transformMatrix * renderer.viewProjection;
            renderer.Device.UpdateData(renderer.tintedBuffer, renderData);

            if (modelName == checkpointstar + ".DFF")
            {
                renderData.worldViewProjection = Matrix.Scaling(1.25f) * Matrix.Translation(0f, 20f, 0f) * transformMatrix * renderer.viewProjection;
                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
            }

            renderer.dffRenderer.DFFModels[modelName].Render(renderer.Device);
        }
        
        public short Priority
        {
            get => ReadShort(4);
            set => Write(4, value);
        }
    }
}
