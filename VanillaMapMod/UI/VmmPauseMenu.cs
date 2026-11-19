using System.Collections.Generic;
using MagicUI.Elements;
using MapChanger;
using MapChanger.UI;

namespace VanillaMapMod.UI;

internal class VmmPauseMenu : PauseMenuLayout
{
    internal VmmPauseMenu()
        : base(nameof(VanillaMapMod), nameof(VmmPauseMenu))
    {
        if (Localization.MenuFont is not { } font)
        {
            return;
        }

        foreach (var element in Elements)
        {
            if (element is Button button)
            {
                button.Font = font;
            }
            else if (element is TextObject text)
            {
                text.Font = font;
            }
        }
    }

    protected override bool ActiveCondition()
    {
        return base.ActiveCondition()
            && MapChangerMod.IsEnabled()
            && ModeManager.CurrentMode().Mod is nameof(VanillaMapMod);
    }

    protected override PauseMenuTitle GetTitle()
    {
        return new VmmPauseMenuTitle();
    }

    protected override IEnumerable<MainButton> GetMainButtons()
    {
        return
        [
            new ModEnabledButton(),
            new ModPinsButton(),
            new VanillaPinsButton(),
            new PoolsGridControlButton(),
            new ModeButton(),
            new PinShapeButton(),
            new PinSizeButton(),
            new FastMapUpdateButton(),
            new UIScaleButton(),
        ];
    }

    protected override IEnumerable<ExtraButtonGrid> GetExtraButtonGrids()
    {
        return [new PoolsGrid()];
    }

    private sealed class VmmPauseMenuTitle : PauseMenuTitle
    {
        protected override TextFormat GetDefaultTextFormat()
        {
            return new(Layout.Mod.Translate(), Colors.GetColor(ColorSetting.UI_Neutral));
        }
    }
}
