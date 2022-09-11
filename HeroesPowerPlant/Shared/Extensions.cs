using Heroes.SDK.Custom;
using Heroes.SDK.Utilities.Math.Structs;
using System.Linq;
using System;
using Heroes.SDK.Definitions.Enums;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HeroesPowerPlant
{
    public static class Extensions
    {
        public static SharpDX.Vector3 ToSharpDXVector3(this Vector3 v)
        {
            return new SharpDX.Vector3(v.X, v.Y, v.Z);
        }

        public static Vector3 ToHeroesSDKVector(this SharpDX.Vector3 v)
        {
            return new Vector3 { X = v.X, Y = v.Y, Z = v.Z };
        }

        public static Stage StageFromFileNamePrefix(string fileName)
        {
            var enumType = typeof(Stage);
            foreach (Stage s in Enum.GetValues(enumType))
            {
                var memberInfos = enumType.GetMember(s.ToString());
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
                var valueAttribute = enumValueMemberInfo.GetCustomAttribute(typeof(FileNameAttribute));
                if (valueAttribute != null && fileName == ((FileNameAttribute)valueAttribute).FileNameWithoutExtension)
                    return s;
            }
            return Stage.Null;
        }

        public static DialogResult UnsavedChangesMessageBox(string editorName) =>
            MessageBox.Show($"You have unsaved changes on the {editorName}." +
                $"Do you wish to save before closing?",
                "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
    }
}
