using System;
using System.Windows.Forms;
using HeroesPowerPlant.Shared.IO.Config;
using SharpDX;

namespace HeroesPowerPlant.MainForm
{
    public partial class ViewConfig : Form
    {
        public bool ProgramIsUpdatingValues = false;

        /*
            ------------
            Constructors
            ------------
        */

        // Default value assignments moved to SharpRenderer.

        public ViewConfig()
        {
            InitializeComponent();
            SharpRenderer.Camera.CameraChangedEvent += CameraChanged;
        }

        private void CameraChanged(SharpCamera camera)
        {
            ProgramIsUpdatingValues = true;
            NumericFOV.Value = (decimal) camera.ProjectionMatrix.FieldOfView;
            NumericDrawD.Value = (decimal) camera.ProjectionMatrix.FarPlane;
            NumericInterval.Value = (decimal) camera.Speed;
            NumericCameraX.Value = (decimal) camera.ViewMatrix.Position.X;
            NumericCameraY.Value = (decimal) camera.ViewMatrix.Position.Y;
            NumericCameraZ.Value = (decimal) camera.ViewMatrix.Position.Z;

            NumericCameraYaw.Value = (decimal)camera.ViewMatrix.Yaw;
            NumericCameraPitch.Value = (decimal)camera.ViewMatrix.Pitch;
            ProgramIsUpdatingValues = false;
        }


        /*
             ------
             Events
             ------
        */

        private void ViewConfig_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }
        
        private void NumericCamera_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsUpdatingValues)
                SharpRenderer.Camera.SetPosition(new Vector3((float)NumericCameraX.Value, (float)NumericCameraY.Value, (float)NumericCameraZ.Value));
        }

        private void NumericCameraRot_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsUpdatingValues)
            {
                SharpRenderer.Camera.ViewMatrix.Pitch = (float) NumericCameraPitch.Value;
                SharpRenderer.Camera.ViewMatrix.Yaw = (float)NumericCameraYaw.Value;
            }
        }

        private void NumericInterval_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsUpdatingValues)
                SharpRenderer.Camera.Speed = (float)NumericInterval.Value;
        }

        private void NumericDrawD_ValueChanged(object sender, EventArgs e)
        {
            SharpRenderer.Camera.ProjectionMatrix.FarPlane = (float)NumericDrawD.Value;
        }

        private void NumericFOV_ValueChanged(object sender, EventArgs e)
        {
            if (NumericFOV.Value < 1)
                NumericFOV.Value = 1;
            SharpRenderer.Camera.ProjectionMatrix.FieldOfView = (float)NumericFOV.Value;
        }

        private void NumericQuadHeight_ValueChanged(object sender, EventArgs e)
        {
            CollisionEditor.CollisionRendering.SetQuadHeight((float)NumericQuadHeight.Value);
        }

        private void ViewConfig_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                UpdateValues();
        }

        /*
            -------
            Methods
            -------
        */

        /// <summary>
        /// Obtains the values from the current Power Plant instance and applies them to the
        /// View Config.
        /// </summary>
        public void UpdateValues()
        {
            var currentConfig = ProjectConfig.FromCurrentInstance();
            NumericFOV.Value = (decimal)currentConfig.CameraSettings.FieldOfView;
            NumericDrawD.Value = (decimal)currentConfig.CameraSettings.DrawDistance;
            NumericInterval.Value = (decimal) currentConfig.CameraSettings.Speed;
            NumericCameraX.Value = (decimal)currentConfig.CameraSettings.CameraPosition.X;
            NumericCameraY.Value = (decimal)currentConfig.CameraSettings.CameraPosition.Y;
            NumericCameraZ.Value = (decimal)currentConfig.CameraSettings.CameraPosition.Z;

            NumericCameraYaw.Value = (decimal)currentConfig.CameraSettings.Yaw;
            NumericCameraPitch.Value = (decimal)currentConfig.CameraSettings.Pitch;
        }

        public void SetValues(Vector3 position, float yaw, float pitch, float speed)
        {
            if (!Visible)
                return;

            ProgramIsUpdatingValues = true;

            NumericCameraX.Value = (decimal)position.X;
            NumericCameraY.Value = (decimal)position.Y;
            NumericCameraZ.Value = (decimal)position.Z;
            NumericInterval.Value = (decimal)speed;
            NumericCameraPitch.Value = (decimal)pitch;
            NumericCameraYaw.Value = (decimal)yaw;

            ProgramIsUpdatingValues = false;
        }
    }
}
