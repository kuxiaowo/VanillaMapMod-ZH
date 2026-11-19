using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class ModEnabledButton : MainButton
{
    protected override void OnClick()
    {
        ModeManager.ToggleModEnabled();
    }

    protected override TextFormat GetTextFormat()
    {
        return new("Mod\nEnabled".Translate(), Colors.GetColor(ColorSetting.UI_On));
    }
}
