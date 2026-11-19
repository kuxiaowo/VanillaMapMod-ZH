# Vanilla Map Mod 简体中文版

本版本基于 syyePhenomenol 的 [Vanilla Map Mod 2.1.6](https://github.com/syyePhenomenol/HollowKnight.VanillaMapMod/releases/tag/v2.1.6)。2026 年 11 月 19 日修改了暂停菜单、标记分类和按钮状态的文案，并为这些控件增加中文字体选择。源码和发布包继续采用 [GNU GPL v3](LICENSE)。

## 安装

1. 适用环境为 Windows Steam 版《空洞骑士》`1.5.12620`、Modding API v78 和 [MapChanger Unity 6 兼容补丁](https://github.com/kuxiaowo/MapChanger-Unity6/releases/tag/v1.3.11-unity6.1)。还需要原 Mod 的 MagicUI 和 ConnectionMetadataInjector 依赖。
2. 退出游戏，备份 `hollow_knight_Data/Managed/Mods/Vanilla Map Mod`。从 [Releases](../../releases) 下载简体中文版 ZIP，将其中的 `VanillaMapMod.dll`、`VanillaMapMod.pdb` 和 `language.json` 解压到该目录，替换同名文件。与原版共用同一个安装目录，只保留一份 DLL。
3. 进存档后打开暂停菜单即可看到中文标记设置。沿用 `Ctrl+M` 开关地图 Mod 和 `Ctrl+T` 切换地图模式。回退时换回备份目录里的文件即可。

汉化包优先使用操作系统已有的中文字体，例如微软雅黑。找不到覆盖字形的字体时，菜单会回退到英文。

## 如何收录到 Lumafly

Lumafly 的公开目录由 [Hollow Knight Modlinks](https://github.com/hk-modding/modlinks/blob/main/ReadMe.md) 维护。其投稿规则建议给已有 Mod 做增强时先向原项目提交改动。对这份汉化，可以先向原作者提交翻译及字体改动，获采纳后由原条目更新发布包。

如维护者接受独立收录，需先公开源码和可直接下载的发布 ZIP，再对索引仓库的 `ModLinks.xml` 发起 PR。清单包括唯一的 `Name`、`Description`、四段数字版号（本包为 `2.1.6.1`）、下载链接及 ZIP 的 SHA-256、`Repository`，以及现有条目的依赖名 `MapChanger`、`MagicUI` 和 `ConnectionMetadataInjector`。还需要与维护者确认独立条目与原版的替换安装方式。

默认发布渠道的 API 清单目前仍指向 v77。本包面向 v78，应先确认维护者指定的 Unity 6 索引及依赖是否就绪。 [版本索引拆分讨论](https://github.com/hk-modding/modlinks/pull/2761)

## 构建

Windows 上可用 PowerShell 7 和 Visual Studio 2022 的 Roslyn C# 编译器复现发布包。把 `ManagedDirectory` 指向已具备上述依赖的游戏 `Managed` 目录。

```powershell
pwsh -File ./tools/Build-Package.ps1 -ManagedDirectory '<游戏目录>/hollow_knight_Data/Managed' -CompilerPath '<Visual Studio>/MSBuild/Current/Bin/Roslyn/csc.exe'
```

默认输出位于 `artifacts`。脚本只读取游戏程序集，构建和打包都在输出目录完成。

## 上游说明

![GitHub release (latest by date)](https://img.shields.io/github/v/release/syyePhenomenol/HollowKnight.VanillaMapMod)
![Downloads GitHub all releases](https://img.shields.io/github/downloads/syyePhenomenol/HollowKnight.VanillaMapMod/total)

VanillaMapMod is a Hollow Knight mod that adds extra pins for all item locations on the map. Works on any save file.

# Dependencies
- [MapChanger](https://github.com/syyePhenomenol/MapChanger)
- [MagicUI](https://github.com/BadMagic100/HollowKnight.MagicUI)
- [ConnectionMetadataInjector](https://github.com/BadMagic100/ConnectionMetadataInjector)

Please make sure all of the dependent mods are installed and up to date.

Optional and compatible:
- [AdditionalMaps](https://github.com/SFGrenade/AdditionalMaps). Adds pins to white palace and godhome maps

# Screenshots
<details>
<summary>Click to expand</summary>
    <img src="./ReadmeAssets/Screenshots/WorldMap.jpg" alt="World Map">
    <img src="./ReadmeAssets/Screenshots/Pause.JPG" alt="Pause Menu">
</details>

# Quick Start Guide
- Press `CTRL-M` during a game to enable the mod. Alternatively, click the red button when the game is paused.

# Features
## Hotkeys
- ``CTRL-M``: Toggle map enabled/disabled
- ``CTRL-T``: Toggle map mode

## Pause Menu
- "Map Mod Enabled/Disabled" `CTRL-M`: Toggle the mod on/off
- "Mode": Toggle the map mode
    - "Normal": Adds new pins over the existing map
    - "Full Map": Shows all pins and the full map regardless of progression
- "Mod Pins": Toggles the new pins on/off.
- "Vanilla Pins": Toggles the game's existing pins on/off.
- "Pin Shape": Toggles the shape of the new pins.
- "Pin Size": Toggles the size of the new pins.
- "Customize Pins": Toggle each pool on/off.
- "Fast Map Update (Normal Mode only)": When enabled and you have Quill, your map will update as soon as you enter a new room.
- "UI Scale": Change the scaling of UI text and buttons.

# How To Install
Use Scarab: https://github.com/fifty-six/Scarab

Or, you can install manually:
1. Download the latest release of `VanillaMapMod.zip`.
2. Unzip and copy the folder 'VanillaMapMod' to `...\hollow_knight_Data\Managed\Mods`.
