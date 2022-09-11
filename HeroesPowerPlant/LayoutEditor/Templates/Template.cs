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
            Stage = Extensions.StageFromFileNamePrefix(fileName);
        }

        public override string ToString()
        {
            return $"{Stage} - {Team} - {Name}";
        }
    }
}
