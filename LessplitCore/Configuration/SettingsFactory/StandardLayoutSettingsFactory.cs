﻿using Eto.Drawing;

namespace LessplitCore.Configuration.SettingsFactory
{
    public class StandardLayoutSettingsFactory : ILayoutSettingsFactory
    {
        public LayoutSettings Create()
        {
            return new LayoutSettings()
            {
                TextColor = Color.FromArgb(255, 255, 255),
                BackgroundColor = Color.FromArgb(0, 0, 0, 0),
                BackgroundColor2 = Color.FromArgb(0, 0, 0, 0),
                ThinSeparatorsColor = Color.FromArgb(9, 255, 255, 255),
                SeparatorsColor = Color.FromArgb(38, 255, 255, 255),
                PersonalBestColor = Color.FromArgb(22, 166, 255),
                AheadGainingTimeColor = Color.FromArgb(41, 204, 84),
                AheadLosingTimeColor = Color.FromArgb(112, 204, 137),
                BehindGainingTimeColor = Color.FromArgb(204, 120, 112),
                BehindLosingTimeColor = Color.FromArgb(204, 55, 41),
                BestSegmentColor = Color.FromArgb(216, 175, 31),
                NotRunningColor = Color.FromArgb(122, 122, 122),
                PausedColor = Color.FromArgb(122, 122, 122),
                TextOutlineColor = Color.FromArgb(0, 0, 0, 0),
                ShadowsColor = Color.FromArgb(128, 0, 0, 0),
                TimerFont = new Font("Century Gothic", 43.75f, FontStyle.Bold),
                TimesFont = new Font("Segoe UI", 13, FontStyle.Bold),
                TextFont = new Font("Segoe UI", 13, FontStyle.None),
                ShowBestSegments = true,
                UseRainbowColor = false,
                AlwaysOnTop = true,
                AntiAliasing = true,
                DropShadows = true,
                BackgroundType = BackgroundType.SolidColor,
                BackgroundImage = null,
                ImageOpacity = 1f,
                ImageBlur = 0f,
                Opacity = 1,
                MousePassThroughWhileRunning = false
            };
        }
    }
}
