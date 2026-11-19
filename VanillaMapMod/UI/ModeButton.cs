using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class ModeButton : MainButton
{
    protected override void OnClick()
    {
        ModeManager.ToggleMode();
    }

    protected override TextFormat GetTextFormat()
    {
        var text = $"{"Mode".Translate()}\n";
        return ModeManager.CurrentMode() switch
        {
            NormalMode normalMode => new(text + normalMode.ModeName.Translate(), Colors.GetColor(ColorSetting.UI_Neutral)),
            FullMapMode fullMapMode => new(text + fullMapMode.ModeName.Translate(), Colors.GetColor(ColorSetting.UI_On)),
            _ => default,
        };
    }
}
