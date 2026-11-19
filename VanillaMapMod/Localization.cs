using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MapChanger;
using UnityEngine;

namespace VanillaMapMod;

internal static class Localization
{
    private static readonly string[] ChineseFontNames =
    [
        "Microsoft YaHei", "Microsoft YaHei UI", "SimHei", "SimSun",
        "PingFang SC", "Noto Sans CJK SC", "Source Han Sans SC", "WenQuanYi Micro Hei",
    ];

    private static Dictionary<string, string> _localization;
    internal static Font MenuFont { get; private set; }

    internal static void Load()
    {
        try
        {
            _localization = JsonUtil.DeserializeFromExternalFile<Dictionary<string, string>>(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "language.json")
            );
            if (_localization is null)
            {
                return;
            }

            var characters = _localization.Values
                .Where(value => value is not null)
                .SelectMany(value => value)
                .Where(character => !char.IsWhiteSpace(character))
                .Distinct()
                .ToArray();

            if (characters.Any(character => character is >= '\u4e00' and <= '\u9fff'))
            {
                MenuFont = FindChineseFont(characters);
                if (MenuFont is null)
                {
                    VanillaMapMod.Instance.LogWarn("No font with the required Chinese glyphs found. Using English labels.");
                    _localization = null;
                }
            }
        }
        catch (Exception e)
        {
            _localization = null;
            VanillaMapMod.Instance.LogDebug($"Could not load language.json: {e.Message}");
        }
    }

    internal static string Translate(this string text)
    {
        return _localization is not null && _localization.TryGetValue(text, out var result) && result is not null
            ? result
            : text.L();
    }

    private static Font FindChineseFont(char[] characters)
    {
        var installed = new HashSet<string>(Font.GetOSInstalledFontNames(), StringComparer.OrdinalIgnoreCase);
        foreach (var name in ChineseFontNames.Where(installed.Contains))
        {
            var candidate = Font.CreateDynamicFontFromOSFont(name, 16);
            if (candidate == null)
            {
                continue;
            }

            if (characters.All(candidate.HasCharacter))
            {
                return candidate;
            }

            UnityEngine.Object.Destroy(candidate);
        }

        return Resources.FindObjectsOfTypeAll<Font>().FirstOrDefault(font => characters.All(font.HasCharacter));
    }
}
