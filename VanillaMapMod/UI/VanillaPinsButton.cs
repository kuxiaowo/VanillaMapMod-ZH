using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class VanillaPinsButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.LS.ToggleVanillaPins();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"{"Vanilla Pins".Translate()}:\n";

        return VanillaMapMod.LS.VanillaPinsOn
            ? new(text + "On".Translate(), Colors.GetColor(ColorSetting.UI_On))
            : new(text + "Off".Translate(), Colors.GetColor(ColorSetting.UI_Neutral));
    }
}
