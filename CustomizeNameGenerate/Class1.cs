using BepInEx;
using BepInEx.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;

[BepInPlugin("cs.iccth.HoL.CustomizeNameGenerate", "Customize Name Generate (自定义姓名生成)", "1.0.0")]
public class CustomizeNameGenerate : BaseUnityPlugin
{
    private ConfigEntry<string> cfgFamilyName;
    private ConfigEntry<string> cfgMaleFirstNameA;
    private ConfigEntry<string> cfgMaleFirstNameB;
    private ConfigEntry<string> cfgFemaleFirstNameA;
    private ConfigEntry<string> cfgFemaleFirstNameB;

    private const char Separator = ',';

    private void Awake()
    {
        // ----------------------------------------------------
        // 1. 定义配置项并载入原版数据作为默认值
        //    使用英文逗号 "," 作为默认值的连接符，以保证在配置文件中格式简洁。
        //    源文件: "Assembly-CSharp.dll.RandName.cs"
        // ----------------------------------------------------

        // 姓氏配置 (FamilyName)
        string defaultFamilyName = string.Join(Separator.ToString(), new string[]
        {
            "赵|Zhao", "钱|Qian", "孙|Sun", "李|Li", "周|Zhou", "吴|Wu", "郑|Zheng", "王|Wang", "冯|Feng", "陈|Chen",
            "褚|Chu", "卫|Wei", "蒋|Jiang", "沈|Shen", "韩|Han", "杨|Yang", "朱|Zhu", "秦|Qin", "尤|You", "许|Xu",
            "何|He", "吕|Lv", "施|Shi", "张|Zhang", "孔|Kong", "曹|Cao", "严|Yan", "华|Hua", "金|Jin", "魏|Wei",
            "陶|Tao", "姜|Jiang", "戚|Qi", "谢|Xie", "邹|Zou", "喻|Yu", "柏|Bai", "水|Shui", "窦|Dou", "章|Zhang",
            "骆|Luo", "蔡|Cai", "胡|Hu", "虞|Yu", "柯|Ke", "卢|Lu", "房|Fang", "干|Gan", "宗|Zong", "贲|Ben",
            "杭|Hang", "左|Zuo", "钮|Niu", "邢|Xing", "荣|Rong", "舒|Shu", "祝|Zhu", "杜|Du", "闵|Min", "麻|Ma",
            "路|Lu", "江|Jiang", "郭|Guo", "林|Lin", "徐|Xu", "高|Gao", "田|Tian", "凌|Ling", "万|Wan", "昝|Zan",
            "莫|Mo", "裘|Qiu", "解|Jie", "丁|Ding", "邓|Deng", "洪|Hong", "石|Shi", "龚|Gong", "滑|Hua", "翁|Weng",
            "夜刀神|Yatogami", "大道寺|Daidoji", "椎名|Shiina", "风见|Kazami", "橘|Tachibana", "伊地知|Ijichi", "五更|Goko", "阿良良木|Araragi", "立华|Tachibana", "音无|Otonashi", "绯村|Himura", "鸢一|Tobiichi", "白银|Shirogane", "四宫|Shinomiya", "五河|Itsuka", "藤原|Fujiwara", "雪之下|Yukinoshita", "朽木|Kuchiki", "霞之丘|Kasumigaoka", "结城|Yuuki", "桐之谷|Kirigaya", "白石|Shiraishi", "三千院|Sanzenin", "蛰殿|Kabutodono", "雨宫|Amamiya", "初音|Hatsune", "镜野|Kyono", "千反田|Chitanda", "一之濑|Ichinose", "七海|Nanami", "星野|Hoshino", "天野|Amano", "星宫|Hoshimiya", "坂柳|Sakayanagi", "赤座|Akaza", "卧烟|Gaen", "伊藤|Ito", "风浦|Kaura", "千石|Sengoku", "桐崎|Kirisaki", "樱岛|Sakurajima", "折原|Orihara", "苍崎|Aozaki", "间桐|Matou", "伊吹|Ibuki", "新垣|Aragaki", "朝田|Asada", "雾间|Kirima", "千早|Chihaya", "小森|Komori", "小节|Kobushi", "日塔|Hito", "鑢|Yasuri", "荻野|Ogino", "平和岛|Heiwajima", "一濑|Ichinose", "初春|Uiharu", "麦野|Mugino", "青沼|Aonuma", "牧之原|Makinohara", "高町|Takamachi", "七草|Saegusa", "千叶|Chiba", "渡边|Watanabe", "津久叶|Tsukuba", "白崎|Shirasaki", "安室|Amuro", "望月|Mochizuki", "星川|Hoshikawa", "早坂|Hayasaka", "草薙|Kusanagi", "清秋院|Seishuin", "一色|Isshiki", "川崎|Kawasaki", "城廻|Meguri", "相川|Aikawa", "贝木|Kaiki", "沼地|Numachi", "笹川|Sasagawa", "朝雾|Asagiri", "仓屋敷|Kurayashiki", "二阶堂|Nikaido", "九重|Kujou", "时坂|Tokisaka", "佐伯|Saeki", "二海堂|Nikaido", "皇|Sumeragi", "富士宫|Fujinomiya", "一条|Ichijo", "越谷|Koshigaya", "堀|Hori", "爱泽|Aizawa", "鹭之宫|Saginomiya", "鱼见|Uomi", "八舞|Yamai", "镜野|Kyono", "村雨|Murasame", "芦屋|Ashiya", "镰月|Kamatsuki", "小野寺|Onodera", "乌间|Karasuma", "小糸|Koito", "千代田|Chiyoda", "如月|Kisaragi", "唐泽|Karasawa", "浅野|Asano", "笹森|Sasamori", "因幡|Inaba", "冢本|Tsukamoto", "高野|Takano", "继国|Tsugikuni", "风间|Kazama", "九条|Kujo", "西行寺|Saigyouji", "七瀬|Nanase", "江户川|Edogawa", "道明寺|Domyoji", "千鸟渊|Chidorigafuchi", "森岛|Morishima", "白鬼院|Shirakiin", "折木|Oreki", "雾雨|Kirisame", "木之本|Kinomoto",
            "斯卡雷特|Scarlet", "刃下心|Heart-Under-Blade", "布兰度|Brando", "阿克曼|Ackerman", "加德纳|Gardner", "佛斯特|Foster", "阿让|Arjean", "冯·爱因兹贝伦|von Einzbern", "布里兹涅夫|Brezhnev", "塞维伦|Sevilun", "特恩佩斯特|Tempest", "欧贝鲁|Oberu", "奥哈拉|Ohara", "拉塞尔|Russell", "爱雷克西亚|Alexia", "冯·里斯妃特|von Riessfeld", "阿波根|Apocynum", "布朗特里|Blountree", "克兰尼查尔|Klannig", "伯恩斯坦|Bernstein", "爱因兹渥司|Ainsworth", "阿茹巴斯|Arubas", "海渥卡|Hawkeye", "皇|Kou", "麦克白|Macbeth", "A·斯塔菲斯|A. Sturlusson", "诺塔|Nota", "瑟尼欧里斯|Senioris", "克梅修|Krumscheid", "杜夫纳|Dufner", "波特|Potter", "特瓦伊莱特|Twilight", "阿修贝尔|Asubel", "梵格|Vongola", "兰格雷|Langley", "威震天|Megatron"
        });
        cfgFamilyName = Config.Bind(
            "Name Lists (姓名词库)",
            "FamilyName",
            defaultFamilyName,
            "Surname list. Format: Hanzi|Pinyin. Separate entries with a comma (e.g., 赵|Zhao, 钱|Qian).\n姓氏列表，格式：汉字|英语。词条之间请使用英文逗号“,”分隔。"
        );

        // 男名前缀 (MaleFirstNameA)
        string defaultMaleFirstNameA = string.Join(Separator.ToString(), new string[]
        {
            " 吉·|Ji.", " 祥·|Xiang.", " 庆·|Qing.", " 福·|Fu.", " 禄·|Lu.", " 祯·|Zhen.", " 裕·|Yu.", " 兴·|Xing.", " 隆·|Long.", " 泰·|Tai.",
            " 振·|Zhen.", " 梓·|Zi.", " 瑞·|Rui.", " 金·|Jin.", " 家·|Jia.", " 奕·|Yi.", " 轩·|Xuan.", " 楠·|Nan.", " 乐·|Le.", " 钦·|Qin.",
            " 华·|Hua.", " 宁·|Ning.", " 阳·|Yang.", " 江·|Jiang.", " 渊·|Yuan.", " 瑜·|Yu.", " 君·|Jun.", " 成·|Cheng.", " 铭·|Ming.", " 昊·|Hao.",
            " 旭·|Xu.",
            " 仁·|Ren.", " 义·|Yi.", " 礼·|Li.", " 智·|Zhi.", " 信·|Xin.", " 忠·|Zhong.", " 孝·|Xiao.", " 节·|Jie.", " 温·|Wen.", " 良·|Liang.",
            " 恭·|Gong.", " 俭·|Jian.", " 让·|Rang.", " 敬·|Jing.", " 正·|Zheng.", " 恩·|En.", " 德·|De.", " 贤·|Xian.", " 明·|Ming.", " 慈·|Ci.",
            " 善·|Shan.", " 悌·|Ti.", " 惠·|Hui.", " 谦·|Qian.", " 廉·|Lian.", " 勤·|Qin.", " 敏·|Min.", " 奉·|Feng.", " 慕·|Mu.", " 贞·|Zhen.",
            " 龙·|Long.",
        });
        cfgMaleFirstNameA = Config.Bind(
            "Name Lists (姓名词库)",
            "MaleFirstNameA",
            defaultMaleFirstNameA,
            "Male given name prefix list. Separate entries with a comma (e.g., 吉|Ji, 祥|Xiang).\n男名中段词汇列表，格式：汉字|英语。词条之间请使用英文逗号“,”分隔。\nOrigin(原版): 吉|Ji,祥|Xiang,庆|Qing,福|Fu,禄|Lu,祯|Zhen,裕|Yu,兴|Xing,隆|Long,泰|Tai,振|Zhen,梓|Zi,瑞|Rui,金|Jin,家|Jia,奕|Yi,轩|Xuan,楠|Nan,乐|Le,钦|Qin,华|Hua,宁|Ning,阳|Yang,江|Jiang,渊|Yuan,瑜|Yu,君|Jun,成|Cheng,铭|Ming,昊|Hao,旭|Xu"
        );

        // 男名后缀 (MaleFirstNameB)
        string defaultMaleFirstNameB = string.Join(Separator.ToString(), new string[]
        {
            "结弦|Yuzuru", "剑心|Kenshin", "桃矢|Touya", "恭弥|Kyoya", "遮那|Vairocana", "焰|Homura", "枫|Kaede", "绮礼|Kirei", "临也|Izaya", "六枝|Mutsu", "静雄|Shizuo", "红莲|Guren", "瞬|Shun", "新一|Shinichi", "柯南|Conan", "秀一|Shuichi", "泥舟|Deishu", "武|Takeru", "了平|Ryohei", "吾郎|Goro", "半藏|Hanzo", "四郎|Shiro", "惟臣|Koreomi", "浩二|Koji", "缘一|Yoriichi", "奈落|Naraku", "清玄|Seigen",
            "利威尔|Levi", "艾瑞尔|Ariel", "兰顿|Renton", "安灼拉|Enjolras", "赛门|Simon", "爱华斯|Aiwass", "利姆露|Rimuru", "杰诺斯|Genos", "雷|Ray", "仁|Jin", "冉|Ran", "迪诺|Dino", "里包恩|Reborn", "艾略特|Elliot", "尤西斯|Jusis", "马奇亚斯|Machias", "阿加特|Agate", "库尔特|Kurt", "罗伊德|Lloyd", "约书亚|Joshua", "雷欧|Leo", "克劳斯|Klaus", "威廉|William", "拉斯|Rath"
        });
        cfgMaleFirstNameB = Config.Bind(
            "Name Lists (姓名词库)",
            "MaleFirstNameB",
            defaultMaleFirstNameB,
            "Male given name suffix list. Separate entries with a comma.\n男名后段词汇列表，格式：汉字|英语。词条之间请使用英文逗号“,”分隔。\nOrigin(原版): 仁|ren,义|yi,礼|li,智|zhi,信|xin,忠|zhong,孝|xiao,节|jie,温|wen,良|liang,恭|gong,俭|jian,让|rang,敬|jing,正|zheng,恩|en,德|de,贤|xian,明|ming,慈|ci,善|shan,悌|ti,惠|hui,谦|qian,廉|lian,勤|qin,敏|min,奉|feng,慕|mu,贞|zhen,龙|long"
        );

        // 女名前缀 (FemaleFirstNameA)
        string defaultFemaleFirstNameA = string.Join(Separator.ToString(), new string[]
        {
            " 瑟·|Se.", " 舞·|Wu.", " 萤·|Ying.", " 萱·|Xuan.", " 樱·|Ying.", " 雪·|Xue.", " 华·|Hua.", " 裳·|Shang.", " 蒹·|Jian.", " 葭·|Jia.",
            " 依·|Yi.", " 雯·|Wen.", " 菲·|Fei.", " 灵·|Ling.", " 逸·|Yi.", " 蕴·|Yun.", " 悦·|Yue.", " 美·|Mei.", " 碧·|Bi.", " 璟·|Jing.",
            " 若·|Ruo.", " 婧·|Jing.", " 凌·|Ling.", " 雅·|Ya.", " 雨·|Yu.", " 舒·|Shu.", " 梵·|Fan.", " 欣·|Xin.", " 可·|Ke.", " 媛·|Yuan.",
            " 玥·|Yue.", " 歆·|Xin.", " 瑾·|Jin.", " 佑·|You.", " 檀·|Tan.", " 熙·|Xi.", " 语·|Yu.", " 沐·|Mu.", " 伶·|Ling.", " 清·|Qing.",
            " 絮·|Xu.", " 妙·|Miao.", " 露·|Lu.", " 婉·|Wan.", " 珍·|Zhen.", " 紫·|Zi.", " 艺·|Yi.", " 莎·|Sha.", " 媚·|Mei.", " 熙·|Xi.",
            " 黛·|Dai.", " 瑶·|Yao.", " 姗·|Shan.", " 甜·|Tian.", " 淑·|Shu.", " 菏·|He.", " 心·|Xin.", " 玉·|Yu.", " 慧·|Hui.", " 贞·|Zhen.",
            " 鸾·|Luan.", " 娥·|Miao.", " 娇·|Jiao.", " 素·|Su.", " 巧·|Qiao.", " 芳·|Fang.", " 芝·|Zhi.", " 花·|Hua.", " 华·|Hua.", " 芬·|Fen.",
            " 英·|Ying.", " 芷·|Zhi.", " 玫·|Mei.", " 瑰·|Gui.", " 素·|Su.", " 倾·|Qing.",
            " 乔·|Qiao.", " 馨·|Xin.", " 琪·|Qi.", " 颖·|Ying.", " 妍·|Yan.", " 菲·|Fei.", " 琳·|Lin.", " 涵·|Han.", " 滢·|Ying.", " 雨·|Yu.",
            " 静·|Jing.", " 香·|Xiang.", " 梦·|Meng.", " 洁·|Jie.", " 薇·|Wei.", " 莲·|Lian.", " 丽·|Li.", " 依·|Yi.", " 娜·|Na.", " 芙·|Fu.",
            " 婷·|Ting.", " 秀·|Xiu.", " 琼·|Qiong.", " 娴·|Xian.", " 笑·|Xiao.", " 梅·|Mei.", " 然·|Ran.", " 岚·|Lan.", " 琪·|Qi.", " 馨·|Xin.",
            " 影·|Ying.", " 烟·|Yan.", " 萝·|Luo.", " 欢·|Huan.", " 书·|Shu.", " 溪·|Xi.", " 晚·|Wan.", " 歌·|Ge.", " 枝·|Zhi.", " 璧·|Bi.",
            " 柳·|Liu.", " 雯·|Wen.", " 钗·|Mei.", " 倩·|Qian.", " 玉·|Yu.", " 君·|Jun.", " 宁·|Ning.", " 苏·|Su.",
        });
        cfgFemaleFirstNameA = Config.Bind(
            "Name Lists (姓名词库)",
            "FemaleFirstNameA",
            defaultFemaleFirstNameA,
            "Female given name prefix list. Separate entries with a comma.\n女名中段词汇列表，格式：汉字|英语。词条之间请使用英文逗号“,”分隔。\nOrigin(原版): 瑟|Se,舞|Wu,萤|Ying,萱|Xuan,樱|Ying,雪|Xue,华|Hua,裳|Shang,蒹|Jian,葭|Jia,依|Yi,雯|Wen,菲|Fei,灵|Ling,逸|Yi,蕴|Yun,悦|Yue,美|Mei,碧|Bi,璟|Jing,若|Ruo,婧|Jing,凌|Ling,雅|Ya,雨|Yu,舒|Shu,梵|Fan,欣|Xin,可|Ke,媛|Yuan,玥|Yue,歆|Xin,瑾|Jin,佑|You,檀|Tan,熙|Xi,语|Yu,沐|Mu,伶|Ling,清|Qing,絮|Xu,妙|Miao,露|Lu,婉|Wan,珍|Zhen,紫|Zi,艺|Yi,莎|Sha,媚|Mei,熙|Xi,黛|Dai,瑶|Yao,姗|Shan,甜|Tian,淑|Shu,菏|He,心|Xin,玉|Yu,慧|Hui,贞|Zhen,鸾|Luan,娥|Miao,娇|Jiao,素|Su,巧|Qiao,芳|Fang,芝|Zhi,花|Hua,华|Hua,芬|Fen,英|Ying,芷|Zhi,玫|Mei,瑰|Gui,素|Su,倾|Qing"
        );

        // 女名后缀 (FemaleFirstNameB)
        string defaultFemaleFirstNameB = string.Join(Separator.ToString(), new string[]
        {
            "十香|Tohka", "知世|Tomoyo", "真白|Mashiro", "幽香|Yuuka", "万里花|Marika", "虹夏|Nijika", "瑠璃|Ruri", "火怜|Karen", "月火|Tsukihi", "奏|Kanade", "红叶|Momiji", "折纸|Origami", "辉夜|Kaguya", "千花|Chika", "雪乃|Yukino", "诗羽|Utaha", "明日奈|Asuna", "樱|Sakura", "直叶|Suguha", "直子|Naoko", "十六夜|Izayoi", "日和|Hiyori", "凪|Nagi", "夏娜|Shana", "结衣|Yui", "未来|Miku", "礼弥|Rea", "七罪|Natsumi", "静香|Shizuka", "明日香|Asuka", "桔梗|Kikyo", "千夏|Chinatsu", "千寻|Chihiro", "杏子|Kyoko", "伊澄|Isumi", "澪|Mio", "魔理沙|Marisa", "穹|Sora", "两仪式|Ryogi Shiki", "六喰|Mukuro", "有栖|Arisu", "沙耶|Saya", "灯里|Akari", "伊豆湖|Izuko", "静|Shizu", "千代|Chiyo", "可符香|Kafuka", "青叶|Aoba", "此方|Konata", "千姫|Chihime", "抚子|Nadeko", "薰|Kaoru", "戈薇|Kagome", "千棘|Chitoge", "初雪|Hatsuyuki", "青子|Aoko", "凛|Rin", "瑠依|Rui", "茜|Akane", "真纯|Masumi", "诗乃|Shino", "爱音|Aine", "千里|Chisato", "雾|Kiri", "奈美|Nami", "七实|Nanami", "白|Shiro", "九琉璃|Kururi", "舞流|Mairu", "杏里|Anri", "日向|Hinata", "雏菊|Hinagiku", "奈叶|Nanoha", "雫|Shizuku", "早季|Saki", "夕歌|Yuuka", "香澄|Kasumi", "和叶|Kazuha", "萤|Hotaru", "圭|Kei", "伊吕波|Iroha", "京华|Kyouka", "春奈|Haruna", "丽华|Reika", "妙|Tae", "月|Mutsuki", "纱路|Chino", "麻耶|Maya", "真央|Mao", "紫音|Shion", "季奈|Kina", "木实|Konomi", "京子|Kyoko", "嘉儿|Hikari", "沙希|Saki", "千樱|Chiou", "耶俱矢|Yuzuru", "夕|Yae", "真那|Mana", "奈亚子|Nyaruko", "筝|Koto", "小咲|Kosaki", "侑|Yuu", "灯子|Touko", "时雨|Shigure", "花子|Hanako", "夏实|Natsumi", "千佳|Chika", "莲季|Renki", "花梨|Karin", "道子|Michiko", "八云|Yakumo", "晶|Akira", "铃音|Suzune", "沙耶香|Sayaka", "礼奈|Rena",
            "芙兰朵露|Flandre", "蕾米莉亚|Remilia", "亚丝娜|Asuna", "阿丝娜|Asuna", "伊莉雅|Illya", "爱丽丝|Alice", "伊卡洛斯|Ikaros", "阿尔托莉雅|Artoria", "珂朵莉|Chtholly", "艾米莉亚|Emilia", "克里斯蒂娜|Christina", "尤菲莉亚|Euphyllia", "伊蕾娜|Elaina", "达克尼斯|Darkness", "阿库娅|Aqua", "露琪亚|Rukia", "苏芳|Suo", "休比|Schwi", "克里斯塔|Krista", "瑞吉儿|Rachel", "玛奇亚|Machia", "娜乌西卡|Nausicaa", "苏菲|Sophie", "艾蕾卡|Eureka", "珂赛特|Cosette", "露易丝|Louise", "吉普莉尔|Jibril", "克拉米|Clammy", "爱丽丝菲尔|Irisviel", "梵萝娜|Vorona", "芙兰达|Frenda", "朱菜|Shuna", "拉米莉丝|Ramiris", "克萝耶|Chloe", "布兰切|Blanche", "莉兹贝特|Lisbeth", "斯嘉丽|Scarlet", "爱丽莎|Alisa", "菲特|Fate", "蕾蒂西亚|Leticia", "艾莉卡|Erica", "莉娜|Lina", "伊莉丝|Iris", "尤莉丝|Julis", "亚莉安娜|Arianna", "瑟拉芬|Seraphim", "亚托莉|Atri", "安洁莉卡|Angelica", "克蕾雅|Claire", "拉碧丝|Lapis", "缇妲|Tita", "悠娜|Juna", "妙婕|Musse", "谢莉|Shera", "雪莉|Shirley", "艾丝蒂尔|Estelle", "缇欧|Tio", "蕾恩|Renne", "艾莉|Elie", "黎欧娜|Leona", "杰拉丁|Geraldine", "素娜|Sora", "丽塔|Rita", "艾莉尔|Ariel", "丽莎|Risa", "珍|Jane", "可可娜|Cocona", "琳恩|Lynne", "爱尔梅莉亚|Almelia", "莫妮卡|Monica", "芙兰|Fran", "莉莉丝|Lilith", "吉娜|Gina", "索菲|Sophie", "萝洛|Lolo", "艾琳|Erin", "艾莉森|Allison", "梅露可|Merc", "姬丝秀忒|Kiss-shot", "雅赛劳拉莉昂|Acerola-orion", "莲|Ren", "维多利加|Victorique", "雅典娜|Athena", "玛丽|Mary"
        });
        cfgFemaleFirstNameB = Config.Bind(
            "Name Lists (姓名词库)",
            "FemaleFirstNameB",
            defaultFemaleFirstNameB,
            "Female given name suffix list. Separate entries with a comma.\n女名后段词汇列表，格式：汉字|英语。词条之间请使用英文逗号“,”分隔。\nOrigin(原版): 乔|qiao,馨|xin,琪|qi,颖|ying,妍|yan,菲|fei,琳|lin,涵|han,滢|ying,雨|yu,静|jing,香|xiang,梦|meng,洁|jie,薇|wei,莲|lian,丽|li,依|yi,娜|na,芙|fu,婷|ting,秀|xiu,琼|qiong,娴|xian,笑|xiao,梅|mei,然|ran,岚|lan,琪|qi,馨|xin,影|ying,烟|yan,萝|luo,欢|huan,书|shu,溪|xi,晚|wan,歌|ge,枝|zhi,璧|bi,柳|liu,雯|wen,钗|mei,倩|qian,玉|yu,君|jun,宁|ning,苏|su"
        );

        // ----------------------------------------------------
        // 2. 解析配置并重新赋值给 RandName 静态字段
        // ----------------------------------------------------

        Logger.LogInfo("Loading custom name lists...");

        // 核心修改逻辑：将配置文件中的字符串解析后，重新赋值给 RandName 类的静态列表。
        UpdateRandNameList(RandName.XingShi, cfgFamilyName.Value, "FamilyName (姓氏)");
        UpdateRandNameList(RandName.Nan_MingA, cfgMaleFirstNameA.Value, "MaleFirstNameA (男名前)");
        UpdateRandNameList(RandName.Nan_MingB, cfgMaleFirstNameB.Value, "MaleFirstNameB (男名后)");
        UpdateRandNameList(RandName.Nv_MingA, cfgFemaleFirstNameA.Value, "FemaleFirstNameA (女名前)");
        UpdateRandNameList(RandName.Nv_MingB, cfgFemaleFirstNameB.Value, "FemaleFirstNameB (女名后)");

        Logger.LogInfo("Custom name lists loaded successfully.");
    }

    /// <summary>
    /// 解析配置字符串并更新 RandName 中的静态列表
    /// </summary>
    private void UpdateRandNameList(List<string> targetList, string configValue, string listName)
    {
        // 使用英文逗号作为分隔符进行分割
        var entries = configValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)
                                 .Where(s => !string.IsNullOrWhiteSpace(s) && !s.TrimStart().StartsWith("#"))
                                 .ToList();

        if (entries.Count > 0)
        {
            // 清空原列表并添加新词条
            targetList.Clear();
            targetList.AddRange(entries);
            Logger.LogInfo($"Updated {listName} with {targetList.Count} entries.");
        }
        else
        {
            Logger.LogWarning($"Config for {listName} is empty or only contains comments. Using original list entries.");
        }
    }
}