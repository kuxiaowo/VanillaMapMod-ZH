using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class PinShapeButton : MainButton
{
    protected override void OnClick()
    {
        VanillaMapMod.GS.TogglePinShape();
    }

    protected override TextFormat GetTextFormat()
    {
        return new(
            $"{"Pin Shape".Translate()}:\n" + VanillaMapMod.GS.PinShape.ToString().ToWhitespaced().Translate(),
            Colors.GetColor(ColorSetting.UI_Neutral)
        );
    }
}
