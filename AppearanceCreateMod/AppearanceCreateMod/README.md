## 说明

自定义NPC外观生成（本家族/其他世家小孩以及郡城人员面板寒门子弟）

运行一次游戏生成配置后可在配置文件中自定义新NPC的随机外观范围，包括后发、身体、脸型、前发、品性（眼睛）

可以将你喜欢的外观ID加入配置，删除你不喜欢的外观ID，不在配置列表内的外观将不会出现在新生成NPC身上

（外观ID就是开启新存档创建角色时捏人界面的后发、身体、脸型、前发对应数字，品性对应数字：1-骄傲 2-刚正 3-活泼 4-善良 5-真诚 6-洒脱 7-高冷 8-自卑 9-怯懦 10-腼腆 11-凶狠 12-善变 13-忧郁 14-多疑）

配置文件中可修改是否对其他世家新成员、郡城寒门子弟生效

身体ID可以添加皇室服装（从30到49，一共30种，男女都有），可以在配置文件中分别修改是否对本家族小孩、其他世家小孩、郡城人员面板寒门子弟启用皇室服装

### 📦 安装

**推荐使用模组管理器自动安装**

- [GaleModManager](https://thunderstore.io/c/dyson-sphere-program/p/Kesomannen/GaleModManager/)
- [r2modman](https://thunderstore.io/c/dyson-sphere-program/p/ebkr/r2modman/)
- [ThunderstoreModManager](https://www.overwolf.com/app/thunderstore-thunderstore_mod_manager)

**手动安装**

- 首先需要安装[BepInExPack](https://thunderstore.io/c/house-of-legacy/p/BepInEx/BepInExPack/)
- 使用本包中BepInEx文件夹覆盖游戏根目录BepInEx文件夹

### 🔧 配置

运行一次游戏后生成配置，位于：

```shell
BepInEx\config\cs.iccth.HoL.AppearanceCreateMod.cfg
```

修改完保存配置，需要重启游戏生效



## English Introduction

Customize the appearance generation range for NPCs. This includes new children born into your clan, children of other clan, and civilian members appearing on the citizen panel.
After running the game once to generate the configuration, you can customize the range of random appearances for new NPCs in the config file. This includes back hair, body, face type, front hair, and personality (eye expression).
You can add the appearance IDs you like to the configuration, and remove the IDs you don't like. Any appearances not listed in the configuration will not appear on newly generated NPCs.
Appearance IDs correspond to the numbers shown in the character-creation screen when starting a new save for back hair, body, face type, and front hair.
Scope Control: Within the configuration file, you can modify whether these customizations apply to new members of other clan and civilian members from the citizen panel.
Royal Outfits: Body IDs can include royal outfits (ranging from 30 to 49, a total of 30 varieties), available for both male and female NPCs. You can separately modify in the configuration file whether to enable these royal outfits for:
- Children of your clan.
- Children of other clan.
- Civilian members from the citizen panel.

Personality IDs (Eyes):
- 1-Proud
- 2-Righteous
- 3-Lively
- 4-Kind
- 5-Honest
- 6-Carefree
- 7-Cold
- 8-Insecure
- 9-Timid
- 10-Shy
- 11-Mean
- 12-Fickle
- 13-Gloomy
- 14-Paranoid

### 📦 Installation

**Use Mod Manager**

- [GaleModManager](https://thunderstore.io/c/dyson-sphere-program/p/Kesomannen/GaleModManager/)
- [r2modman](https://thunderstore.io/c/dyson-sphere-program/p/ebkr/r2modman/)
- [ThunderstoreModManager](https://www.overwolf.com/app/thunderstore-thunderstore_mod_manager)

**Manual**

- First, install [BepInExPack](https://thunderstore.io/c/house-of-legacy/p/BepInEx/BepInExPack/)
- Overwrite the BepInEx folder in the game's root directory with the one from this package

### 🔧 Configuration

The configuration file is generated after running the game once, located at:

```shell
BepInEx\config\cs.iccth.HoL.AppearanceCreateMod.cfg
```

Save the changes after modification and restart the game for them to take effect.