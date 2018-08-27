using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using HeroesPowerPlant.Shared.IO.Config;
using SharpDX;

namespace HeroesPowerPlant.MainForm
{
    public partial class ViewConfig : Form
    {
        public static bool ProgramIsUpdatingValues { get; set; } = true;
        private static bool _invalidCameraValues { get; set; } = false;
        private Thread _updateViewValuesThread;

        /*
            ------------
            Constructors
            ------------
        */

        // Default value assignments moved to Program.MainForm.renderer.

        public ViewConfig()
        {
            InitializeComponent();
            Program.MainForm.renderer.Camera.CameraChangedEvent += CameraChanged;
        }

        /// <summary>
        /// Updates the GUI if the invalid flag has been set.
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateGUIValues(object obj)
        {
            while (true)
            {
                if (_invalidCameraValues)
                {
                    NumericFOV.Invoke((MethodInvoker)UpdateValues);
                }

                Thread.Sleep(33);
            }
        }

        /// <summary>
        /// Signal no longer valid camera values when event thrown.
        /// </summary>
        private void CameraChanged(SharpCamera camera)
        {
            _invalidCameraValues = true;
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
                Program.MainForm.renderer.Camera.SetPosition(new Vector3((float)NumericCameraX.Value, (float)NumericCameraY.Value, (float)NumericCameraZ.Value));
        }

        private void NumericCameraRot_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsUpdatingValues)
            {
                Program.MainForm.renderer.Camera.ViewMatrix.Pitch = (float) NumericCameraPitch.Value;
                Program.MainForm.renderer.Camera.ViewMatrix.Yaw = (float)NumericCameraYaw.Value;
            }
        }

        private void NumericInterval_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsUpdatingValues)
                Program.MainForm.renderer.Camera.Speed = (float)NumericInterval.Value;
        }

        private void NumericDrawD_ValueChanged(object sender, EventArgs e)
        {
            Program.MainForm.renderer.Camera.ProjectionMatrix.FarPlane = (float)NumericDrawD.Value;
        }

        private void NumericFOV_ValueChanged(object sender, EventArgs e)
        {
            if (NumericFOV.Value < 1)
                NumericFOV.Value = 1;
            Program.MainForm.renderer.Camera.ProjectionMatrix.FieldOfView = (float)NumericFOV.Value;
        }

        private void NumericQuadHeight_ValueChanged(object sender, EventArgs e)
        {
            CollisionEditor.CollisionRendering.SetQuadHeight((float)NumericQuadHeight.Value);
        }

        private void ViewConfig_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateValues();
                _updateViewValuesThread = new Thread(UpdateGUIValues);
                _updateViewValuesThread.IsBackground = true;
                _updateViewValuesThread.Start();
            }
            else
            {
                _updateViewValuesThread?.Abort();
            }
                
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
            ProgramIsUpdatingValues = true;
            NumericFOV.Value = (decimal)Program.MainForm.renderer.Camera.ProjectionMatrix.FieldOfView;
            NumericDrawD.Value = (decimal)Program.MainForm.renderer.Camera.ProjectionMatrix.FarPlane;
            NumericInterval.Value = (decimal)Program.MainForm.renderer.Camera.Speed;
            NumericCameraX.Value = (decimal)Program.MainForm.renderer.Camera.ViewMatrix.Position.X;
            NumericCameraY.Value = (decimal)Program.MainForm.renderer.Camera.ViewMatrix.Position.Y;
            NumericCameraZ.Value = (decimal)Program.MainForm.renderer.Camera.ViewMatrix.Position.Z;

            NumericCameraYaw.Value = (decimal)Program.MainForm.renderer.Camera.ViewMatrix.Yaw;
            NumericCameraPitch.Value = (decimal)Program.MainForm.renderer.Camera.ViewMatrix.Pitch;
            ProgramIsUpdatingValues = false;
            _invalidCameraValues = false;
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
            _invalidCameraValues = false;
        }
    }
}
