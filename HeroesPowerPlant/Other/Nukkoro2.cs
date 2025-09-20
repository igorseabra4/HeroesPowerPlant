using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// Originally written by MainMemory for ShadowRando
/// https://github.com/ShadowTheHedgehogHacking/ShadowRando/blob/master/ShadowRando/Core/Nukkoro2.cs
namespace HeroesPowerPlant.Other
{
    public static class Nukkoro2
    {
        static Nukkoro2()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static readonly Dictionary<string, string> Nukkoro2StageIdentifiers = new()
        {
            { "stg0100", "City1" },
            { "stg0200", "Circuit" },
			{ "stg0201", "Canyon1" },
            { "stg0202", "Highway" },
            { "stg0210", "BossBK1" },
            { "stg0300", "HorrorCastle" },
			{ "stg0301", "PrisonIsland" },
			{ "stg0302", "Circus" },
            { "stg0310", "BossEggMeka1" },
            { "stg0400", "City2" },
			{ "stg0401", "ARKPast1" },
            { "stg0402", "canyon2" },
            { "stg0403", "eWorld" },
            { "stg0404", "Ruins" },
            { "stg0410", "BossGUN1" },
            { "stg0411", "BossEggMeka2" },
            { "stg0412", "BossBK2" },
            { "stg0500", "ARKRuins1" },
            { "stg0501", "Sky" },
			{ "stg0502", "Jungle" },
            { "stg0503", "Space" },
			{ "stg0504", "ARKPast2" },
			{ "stg0510", "BossGUN2" },
			{ "stg0511", "BossEggMeka3" },
            { "stg0600", "GunsBase" },
            { "stg0601", "DoomsBase1" },
            { "stg0602", "EggmansBase" },
            { "stg0603", "ARKRuins2" },
            { "stg0604", "DoomsBase2" },
            { "stg0610", "BossBlack_0610" },
            { "stg0611", "BossSonic_0611" },
            { "stg0612", "BossEggmanRobbo_0612" },
            { "stg0613", "BossSonic_0613" },
            { "stg0614", "BossEggmanRobbo_0614" },
            { "stg0615", "BossEggmanRobbo_0615" },
            { "stg0616", "BossBlack_0616" },
            { "stg0617", "BossBlack_0617" },
            { "stg0618", "BossSonic_0618" },
            { "stg0700", "DoomsCore" },
            { "stg0710", "BossLast" },
            { "stg0800", "GUN2P_0800" },
            { "stg0801", "GUN2P_0801" },
            { "stg0802", "GUN2P_0802" }
        };

        public static Dictionary<string, Nukkoro2Stage> ReadFile(string path)
        {
            Dictionary<string, Nukkoro2Stage> result = new Dictionary<string, Nukkoro2Stage>();
            string[] data = File.ReadAllLines(path, Encoding.GetEncoding(932));
            Nukkoro2Stage? stage = null;
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i].Split('#')[0];
                int endbracket;
                if (line.Length > 0 && line[0] == '[' && (endbracket = line.IndexOf(']')) != -1)
                {
                    stage = new Nukkoro2Stage();
                    try
                    {
                        result.Add(line.Substring(1, endbracket - 1), stage);
                    }
                    catch
                    {
                        // ignored
                    }
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] split = line.Split(new[] { " : " }, StringSplitOptions.None);
                    if (split.Length == 2)
                    {
                        List<int> ints = new List<int>();
                        int st = 0;
                        bool num = false;
                        for (int c = 0; c < split[1].Length; c++)
                        {
                            if (num)
                            {
                                if (!char.IsDigit(split[1][c]))
                                {
                                    ints.Add(int.Parse(split[1].Substring(st, c - st)));
                                    if (split[1][c] == '-')
                                        st = c;
                                    else
                                        num = false;
                                }
                            }
                            else if (char.IsDigit(split[1][c]) || split[1][c] == '-')
                            {
                                num = true;
                                st = c;
                            }
                        }
                        if (num)
                            ints.Add(int.Parse(split[1].Substring(st)));
                        switch (split[0])
                        {
                            case "PLAYER":
                                stage.Player.Add(ints[0], new Nukkoro2Player(ints.Skip(1).ToArray()));
                                break;
                            case "STARTSPD":
                                stage.StartSpeed.Add(ints[0], new Nukkoro2Vector(ints.Skip(1).ToArray()));
                                break;
                            case "STARTDEMO":
                                stage.StartDemo = ints[0];
                                break;
                            case "RANK_H":
                                stage.RankHero = new Nukkoro2Rank(ints.ToArray());
                                break;
                            case "RANK_D":
                                stage.RankDark = new Nukkoro2Rank(ints.ToArray());
                                break;
                            case "RANK_N":
                                stage.RankNeutral = new Nukkoro2Rank(ints.ToArray());
                                break;
                            case "GOALEVENTPOS_H":
                                stage.GoalEventPosHero = new Nukkoro2Vector(ints.ToArray());
                                break;
                            case "GOALEVENTPOS_D":
                                stage.GoalEventPosDark = new Nukkoro2Vector(ints.ToArray());
                                break;
                            case "GOALEVENTPOS_N":
                                stage.GoalEventPosNeutral = new Nukkoro2Vector(ints.ToArray());
                                break;
                            case "GOALEVENTPOS_X":
                                stage.GoalEventPosExpert = new Nukkoro2Vector(ints.ToArray());
                                break;
                            case "MISSIONCOUNT_H":
                                stage.MissionCountHero = new Nukkoro2Mission(ints.ToArray());
                                break;
                            case "MISSIONCOUNT_D":
                                stage.MissionCountDark = new Nukkoro2Mission(ints.ToArray());
                                break;
                            case "MISSIONCOUNT_N":
                                stage.MissionCountNeutral = new Nukkoro2Mission(ints.ToArray());
                                break;
                            case "MISSIONCOUNT_HARD":
                                stage.MissionCountExpert = new Nukkoro2Mission(ints.ToArray());
                                break;
                            case "MIPMAPK":
                                stage.MipmapK = ints[0];
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public static void WriteFile(string path, Dictionary<string, Nukkoro2Stage> map)
        {
            using (var fs = File.Create(path))
            using (var sw = new StreamWriter(fs, Encoding.GetEncoding(932)))
            {
                foreach (var stage in map)
                {
                    sw.WriteLine("[{0}]", stage.Key);
                    foreach (var pl in stage.Value.Player)
                        sw.WriteLine("PLAYER : {0} {1}", pl.Key, pl.Value);
                    foreach (var pl in stage.Value.StartSpeed)
                        sw.WriteLine("STARTSPD : {0} {1}", pl.Key, pl.Value);
                    if (stage.Value.StartDemo != 0)
                        sw.WriteLine("STARTDEMO : {0}", stage.Value.StartDemo);
                    if (stage.Value.RankHero != null)
                        sw.WriteLine("RANK_H : {0}", stage.Value.RankHero);
                    if (stage.Value.RankDark != null)
                        sw.WriteLine("RANK_D : {0}", stage.Value.RankDark);
                    if (stage.Value.RankNeutral != null)
                        sw.WriteLine("RANK_N : {0}", stage.Value.RankNeutral);
                    if (stage.Value.GoalEventPosHero != null)
                        sw.WriteLine("GOALEVENTPOS_H : {0}", stage.Value.GoalEventPosHero);
                    if (stage.Value.GoalEventPosDark != null)
                        sw.WriteLine("GOALEVENTPOS_D : {0}", stage.Value.GoalEventPosDark);
                    if (stage.Value.GoalEventPosNeutral != null)
                        sw.WriteLine("GOALEVENTPOS_N : {0}", stage.Value.GoalEventPosNeutral);
                    if (stage.Value.GoalEventPosExpert != null)
                        sw.WriteLine("GOALEVENTPOS_X : {0}", stage.Value.GoalEventPosExpert);
                    if (stage.Value.MissionCountHero != null)
                        sw.WriteLine("MISSIONCOUNT_H : {0}", stage.Value.MissionCountHero);
                    if (stage.Value.MissionCountDark != null)
                        sw.WriteLine("MISSIONCOUNT_D : {0}", stage.Value.MissionCountDark);
                    if (stage.Value.MissionCountNeutral != null)
                        sw.WriteLine("MISSIONCOUNT_N : {0}", stage.Value.MissionCountNeutral);
                    if (stage.Value.MissionCountExpert != null)
                        sw.WriteLine("MISSIONCOUNT_HARD : {0}", stage.Value.MissionCountExpert);
                    if (stage.Value.MipmapK != 0)
                        sw.WriteLine("MIPMAPK : {0}", stage.Value.MipmapK);
                    sw.WriteLine();
                }
            }
        }
    }

    public class Nukkoro2Stage
    {
        public Dictionary<int, Nukkoro2Player> Player { get; set; } = new Dictionary<int, Nukkoro2Player>();
        public Dictionary<int, Nukkoro2Vector> StartSpeed { get; set; } = new Dictionary<int, Nukkoro2Vector>();
        public int StartDemo { get; set; }
        public Nukkoro2Rank RankHero { get; set; }
        public Nukkoro2Rank RankDark { get; set; }
        public Nukkoro2Rank RankNeutral { get; set; }
        public Nukkoro2Vector GoalEventPosHero { get; set; }
        public Nukkoro2Vector GoalEventPosDark { get; set; }
        public Nukkoro2Vector GoalEventPosNeutral { get; set; }
        public Nukkoro2Vector GoalEventPosExpert { get; set; }
        public Nukkoro2Mission MissionCountHero { get; set; }
        public Nukkoro2Mission MissionCountDark { get; set; }
        public Nukkoro2Mission MissionCountNeutral { get; set; }
        public Nukkoro2Mission MissionCountExpert { get; set; }
        public int MipmapK { get; set; }
    }

    public class Nukkoro2Player
    {
        public Nukkoro2Vector Position { get; set; }
        public Nukkoro2Vector Rotation { get; set; }

        public Nukkoro2Player() { }

        public Nukkoro2Player(int[] data)
        {
            Position = new Nukkoro2Vector(data[0], data[1], data[2]);
            Rotation = new Nukkoro2Vector(data[3], data[4], data[5]);
        }

        public override string ToString() => $"{Position} {Rotation}";
    }

    public class Nukkoro2Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Nukkoro2Vector() { }

        public Nukkoro2Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Nukkoro2Vector(int[] data)
        {
            X = data[0];
            Y = data[1];
            Z = data[2];
        }

        public override string ToString() => $"{X} {Y} {Z}";
    }

    public class Nukkoro2Rank
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        public Nukkoro2Rank() { }

        public Nukkoro2Rank(int[] data)
        {
            A = data[0];
            B = data[1];
            C = data[2];
            D = data[3];
        }

        public override string ToString() => $"{A} {B} {C} {D}";
    }

    public class Nukkoro2Mission
    {
        public int Success { get; set; }
        public int Failure { get; set; }

        public Nukkoro2Mission() { }

        public Nukkoro2Mission(int[] data)
        {
            Success = data[0];
            Failure = data[1];
        }

        public override string ToString() => $"{Success} {Failure}";
    }
}
