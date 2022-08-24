using Heroes.SDK.Custom;
using Heroes.SDK.Definitions.Enums;
using System;
using System.Linq;
using System.Reflection;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Template
    {
        public Stage Stage;
        public string Team;
        public string Name;
        public string Text;
        public byte[] MiscSettings;

        public void SetTeam(string v)
        {
            switch (v.ToLower())
            {
                case "pb":
                case "db":
                    Team = "All";
                    break;
                case "p1":
                    Team = "Sonic";
                    break;
                case "p2":
                    Team = "Dark";
                    break;
                case "p3":
                    Team = "Rose";
                    break;
                case "p4":
                    Team = "Chaotix";
                    break;
                case "p5":
                    Team = "SuperHard";
                    break;
            }
        }

        public void SetLevel(string fileName)
        {
            var enumType = typeof(Stage);
            foreach (Stage s in Enum.GetValues(enumType))
            {
                var memberInfos = enumType.GetMember(s.ToString());
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
                var valueAttribute = enumValueMemberInfo.GetCustomAttribute(typeof(FileNameAttribute));
                if (valueAttribute != null && fileName == ((FileNameAttribute)valueAttribute).FileNameWithoutExtension)
                    Stage = s;
            }
        }

        public override string ToString()
        {
            return $"{Stage} - {Team} - {Name}";
        }
    }
}
