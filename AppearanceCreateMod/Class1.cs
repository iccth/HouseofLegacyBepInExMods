using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

namespace AppearanceCreateMod
{
    [BepInPlugin("cs.iccth.HoL.AppearanceCreateMod", "AppearanceCreateMod", "1.1.0")]
    public class AppearanceCreateMod : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        // 女性配置
        private static ConfigEntry<string> femaleBackHairOptions;
        private static ConfigEntry<string> femaleBodyOptions;
        private static ConfigEntry<string> femaleFaceOptions;
        private static ConfigEntry<string> femaleFrontHairOptions;
        private static ConfigEntry<string> femalePersonalitysOptions;

        // 男性配置
        private static ConfigEntry<string> maleBackHairOptions;
        private static ConfigEntry<string> maleBodyOptions;
        private static ConfigEntry<string> maleFaceOptions;
        private static ConfigEntry<string> maleFrontHairOptions;
        private static ConfigEntry<string> malePersonalitysOptions;

        private static ConfigEntry<bool> enableNewMemberOtherOption;
        private static ConfigEntry<bool> enableNewMemberOtherDebug;
        void Awake()
        {
            Log = Logger;

            // 初始化配置
            femaleBackHairOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "女性后发(female Back Hair)", "0 1 2 7 11 13 16 18 19", "自定义女性后发ID列表，用空格分隔。");
            femaleBodyOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "女性身体(female Body)", "0 1 4 5 6 7 8 9 10 11 12 13 14 15 18 19 21 22 23 27 28 30 31 32 33 34 35 36 37 38 39 40 41 46 47", "自定义女性身体ID列表，用空格分隔。");
            femaleFaceOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "女性脸型(female Face)", "0 2", "自定义女性脸型ID列表，用空格分隔。");
            femaleFrontHairOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "女性前发(female Front Hair)", "0 5 7 12 16", "自定义女性前发ID列表，用空格分隔。");
            femalePersonalitysOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "女性品性(female Eyes)", "1 2 3 4 5 6 9 11 12 14", "自定义女性品性(眼睛)列表，用空格分隔。\n1-骄傲 2-刚正 3-活泼 4-善良 5-真诚 6-洒脱 7-高冷 8-自卑 9-怯懦 10-腼腆 11-凶狠 12-善变 13-忧郁 14-多疑\n1-Proud 2-Righteous 3-Lively 4-Kind 5-Honest 6-Carefree 7-Cold 8-Insecure 9-Timid 10-Shy 11-Mean 12-Fickle 13-Gloomy 14-Paranoid");

            maleBackHairOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "男性后发(male Back Hair)", "1 2 3 7 8 9 12 14 17 18", "自定义男性后发ID列表，用空格分隔。");
            maleBodyOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "男性身体(male Body)", "0 1 2 3 5 6 7 8 12 13 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 40 41 45 46 47", "自定义男性身体ID列表，用空格分隔。");
            maleFaceOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "男性脸型(male Face)", "0 1 2", "自定义男性脸型ID列表，用空格分隔。");
            maleFrontHairOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "男性前发(male Front Hair)", "6 7 11 12 13 14 16 17 18 19", "自定义男性前发ID列表，用空格分隔。");
            // malePersonalitysOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "品性(Personalitys)", "骄傲 刚正 活泼 善良 真诚 洒脱 高冷 自卑 怯懦 腼腆 凶狠 善变 忧郁 多疑", "自定义男性品性列表，用空格分隔。");
            malePersonalitysOptions = Config.Bind("自定义NPC外观生成 (Customize Appearance)", "男性品性(male Eyes)", "1 2 4 5 6 7 11 12 14", "自定义男性品性(眼睛)列表，用空格分隔。\n1-骄傲 2-刚正 3-活泼 4-善良 5-真诚 6-洒脱 7-高冷 8-自卑 9-怯懦 10-腼腆 11-凶狠 12-善变 13-忧郁 14-多疑\n1-Proud 2-Righteous 3-Lively 4-Kind 5-Honest 6-Carefree 7-Cold 8-Insecure 9-Timid 10-Shy 11-Mean 12-Fickle 13-Gloomy 14-Paranoid");

            enableNewMemberOtherOption = Config.Bind<bool>("自定义NPC外观生成 (Customize Appearance)", "是否对其他世家新诞生角色生效 (Enable customize appearance for other families)", true, "启用后，其他世家的新生成员外观也将根据自定义外观列表生成");
            enableNewMemberOtherDebug = Config.Bind<bool>("自定义NPC外观生成 (Customize Appearance)", "是否在控制台输出其他世家新NPC信息 (Enable new member information log in console for other families)", false, "启用后，在控制台显示其他世家新成员数据信息（非常频繁，建议关闭）");

            Harmony.CreateAndPatchAll(typeof(AppearanceCreateMod));
            Log.LogInfo("自定义立绘模组加载成功！");
        }

        // 根据性别和自定义立绘列表生成随机立绘ID组合
        public static string GenerateCustomLiHui(int sex)
        {
            string backHairStr, bodyStr, faceStr, frontHairStr;

            // 0女1男
            if (sex == 0)
            {
                backHairStr = femaleBackHairOptions.Value;
                bodyStr = femaleBodyOptions.Value;
                faceStr = femaleFaceOptions.Value;
                frontHairStr = femaleFrontHairOptions.Value;
            }
            else
            {
                backHairStr = maleBackHairOptions.Value;
                bodyStr = maleBodyOptions.Value;
                faceStr = maleFaceOptions.Value;
                frontHairStr = maleFrontHairOptions.Value;
            }

            try
            {
                int[] backHairOptions = Array.ConvertAll(backHairStr.Split(' '), int.Parse);
                int[] bodyOptions = Array.ConvertAll(bodyStr.Split(' '), int.Parse);
                int[] faceOptions = Array.ConvertAll(faceStr.Split(' '), int.Parse);
                int[] frontHairOptions = Array.ConvertAll(frontHairStr.Split(' '), int.Parse);

                int randomBackHair = backHairOptions[TrueRandom.GetRanom(backHairOptions.Length)];
                int randomBody = bodyOptions[TrueRandom.GetRanom(bodyOptions.Length)];
                int randomFace = faceOptions[TrueRandom.GetRanom(faceOptions.Length)];
                int randomFrontHair = frontHairOptions[TrueRandom.GetRanom(frontHairOptions.Length)];

                //Log.LogInfo($"性别: {sex}, " +
                //    "立绘组合：" +
                //    $"后发: {randomBackHair}, " +
                //    $"身体: {randomBody}, " +
                //    $"脸型: {randomFace}, " +
                //    $"前发: {randomFrontHair}");
                return $"{randomBackHair}|{randomBody}|{randomFace}|{randomFrontHair}";
            }
            catch (Exception ex)
            {
                Log.LogError($"解析外观配置时出错: {ex.Message}。请检查配置文件中的ID是否为纯数字且用空格分隔。");
                return null;
            }
        }

        //根据性别和自定义品性列表生成随机品性
        public static string GeneratePersonality(int sex)
        {
            string personalitysStr;

            // 0女1男
            if (sex == 0)
            {
                personalitysStr = femalePersonalitysOptions.Value;
            }
            else
            {
                personalitysStr = malePersonalitysOptions.Value;
            }

            try
            {
                string[] personalitysOptions = personalitysStr.Split(' ');
                int randomIndex = TrueRandom.GetRanom(personalitysOptions.Length);
                string randomPersonality = personalitysOptions[randomIndex];

                // //检查随机选择的品性是否在官方配置列表中
                // string personalitysString = "不明 骄傲 刚正 活泼 善良 真诚 洒脱 高冷 自卑 怯懦 腼腆 凶狠 善变 忧郁 多疑";
                // string[] personalitysArray = personalitysString.Split(' ');
                // int index = Array.IndexOf(personalitysArray, randomPersonality);
                // if (index != -1)
                // {
                //     Log.LogInfo($"从自定义品性列表中生成随机品性：“{randomPersonality}” 品性ID: {index}");
                //     return index.ToString();
                // }
                // else
                // {
                //     Log.LogError($"解析品性配置时出错: 官方品性列表中不存在 “{randomPersonality}”，请检查配置文件中的自定义品性列表，当前自动设置为“骄傲”品性。");
                //     return "骄傲";
                // }

                if (int.TryParse(randomPersonality, out int personalityId) && personalityId >= 1 && personalityId <= 14)
                {
                    return randomPersonality;
                }
                else
                {
                    int defaultPersonalityId = TrueRandom.GetRanom(15);
                    if (defaultPersonalityId == 0) defaultPersonalityId = 1;
                    string defaultPersonality = defaultPersonalityId.ToString();
                    Log.LogError($"解析品性配置时出错: 自定义品性列表中存在无效品性ID \"{randomPersonality}\"，请检查配置文件中的自定义品性列表，当前使用默认随机品性\"{defaultPersonality}\"。");
                    return defaultPersonality;
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"解析品性配置时出错: {ex.Message}。请检查配置文件中的品性ID格式。");
                return null;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(FormulaData), "New_MemberNow")]
        public static void New_MemberNow_Postfix()
        {
            try
            {
                if (Mainload.Member_now.Count == 0) return;

                // 获取最后一个被添加的成员数据
                int lastMemberIndex = Mainload.Member_now.Count - 1;
                List<string> newMemberData = Mainload.Member_now[lastMemberIndex];

                //// 包含新成员所有数据的长字符串
                //string originalMemberString = string.Join("|", newMemberData);
                //Log.LogInfo($"新家族成员原始数据字符串: {originalMemberString}");
                //string[] memberInfoArray = originalMemberString.Split('|');

                // 获取性别ID (根据New_MemberNow源码，性别在newMemberData索引4.4<第5个字符串的第5个分割值>，或者memberInfoArray索引14<第15个分割值>)
                string fourthMemberInfo = newMemberData[4];
                string[] fourthMemberInfoArray = fourthMemberInfo.Split('|');
                int sex = int.Parse(fourthMemberInfoArray[4]);
                //int sex = int.Parse(memberInfoArray[14]);

                // 生成新的立绘ID组合字符串
                string newLiHui = GenerateCustomLiHui(sex);

                // 生成新的品性ID
                string personality = GeneratePersonality(sex);

                if (newLiHui != null && personality != null)
                {
                    // 替换旧的立绘ID组合字符串 (根据New_MemberNow源码，立绘在newMemberData索引1<第2个字符串>)
                    newMemberData[1] = newLiHui;
                    // 替换旧的品性ID (根据New_MemberNow源码，PinXingCreate_MemberNow(num)在newMemberData索引5<第6个字符串>)
                    newMemberData[5] = personality;
                    Mainload.Member_now[lastMemberIndex] = newMemberData;
                    Log.LogInfo($"【家族出现新成员】");
                    Log.LogInfo($"根据性别ID\"{sex}\"从自定义外观配置中生成随机立绘组合: \"{newLiHui}\" （后发|身体|脸型|前发）");
                    Log.LogInfo($"生成随机品性ID: \"{personality}\"");
                    Log.LogInfo($"该成员替换外观后的数据字符串为: \"{string.Join("|", newMemberData)}\"");
                }

            }
            catch (Exception ex)
            {
                // 使用Logger记录详细错误，这比Console.WriteLine更好
                Log.LogError($"处理New_MemberNow后置补丁时发生错误: {ex.Message}\n堆栈跟踪: {ex.StackTrace}");
            }
        }

        //修改其他世家新成员立绘
        [HarmonyPostfix]
        [HarmonyPatch(typeof(FormulaData), "New_MemberOther", new Type[] { typeof(int), typeof(int), typeof(string), typeof(string), typeof(string), typeof(bool) })]
        public static void New_MemberOther_Postfix(int shijiaIndex)
        {
            if (!enableNewMemberOtherOption.Value) return;
            try
            {
                if (shijiaIndex < 0 || shijiaIndex >= Mainload.Member_other.Count) return;

                // Mainload.Member_other类型是List<List<List<string>>>

                // 检查该世家是否有成员
                if (Mainload.Member_other[shijiaIndex].Count == 0) return;

                // 获取该世家最后一个成员的索引
                int lastMemberIndex = Mainload.Member_other[shijiaIndex].Count - 1;

                // 正确获取新成员数据
                List<string> newMemberData = Mainload.Member_other[shijiaIndex][lastMemberIndex];

                // 获取性别ID (根据New_MemberOther方法源码，性别在newMemberData索引2.4<第3个字符串的第5个分割值(num3)>，或者memberInfoArray索引14<第15个分割值>)
                string fourthMemberInfo = newMemberData[2];
                string[] fourthMemberInfoArray = fourthMemberInfo.Split('|');
                int sex = int.Parse(fourthMemberInfoArray[4]);

                string newLiHui = GenerateCustomLiHui(sex);

                string personality = GeneratePersonality(sex);

                if (newLiHui != null && personality != null)
                {
                    // 替换旧的立绘ID组合字符串 (根据New_MemberOther方法源码，立绘在newMemberData索引1<第2个字符串>)
                    newMemberData[1] = newLiHui;

                    // 替换旧的品性ID (根据New_MemberOther方法源码，品性ID（text7）在newMemberData索引2.8<第3个字符串第9个分割值>)
                    // 获取包含品性的字符串
                    string combinedInfo = newMemberData[2];
                    string[] infoArray = combinedInfo.Split('|');
                    infoArray[8] = personality;
                    newMemberData[2] = string.Join("|", infoArray);

                    // 将修改后的数据写回
                    Mainload.Member_other[shijiaIndex][lastMemberIndex] = newMemberData;
                    if (enableNewMemberOtherDebug.Value)
                    {
                        Log.LogInfo($"【其他世家出现新成员】");
                        Log.LogInfo($"根据性别ID\"{sex}\"从自定义外观配置中生成随机立绘组合: \"{newLiHui}\" （后发|身体|脸型|前发）");
                        Log.LogInfo($"生成随机品性ID: \"{personality}\"");
                        Log.LogInfo($"该成员替换外观后的数据字符串为: \"{string.Join("|", newMemberData)}\"");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError($"处理New_MemberOther后置补丁时发生错误: {ex.Message}\n堆栈跟踪: {ex.StackTrace}");
            }
        }

    }
}