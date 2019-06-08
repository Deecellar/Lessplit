using Eto.Forms;
using LessplitCore.Comparators;
using System.Collections.Generic;

namespace LessplitCore.Configuration.SettingsFactory
{
    public class StandardSettingsFactory : ISettingsFactory
    {
        public ISettings Create()
        {
            return new Settings()
            {
                HotkeyProfiles = new Dictionary<string, HotkeyProfile>()
                {
                    {HotkeyProfile.DefaultHotkeyProfileName, new HotkeyProfile()
                        {
                            SplitKey = new KeyOrButton(Keys.Keypad0),
                            ResetKey = new KeyOrButton(Keys.Keypad3),
                            UndoKey = new KeyOrButton(Keys.Keypad8),
                            SkipKey = new KeyOrButton(Keys.Keypad2),
                            SwitchComparisonPrevious = new KeyOrButton(Keys.Keypad4),
                            SwitchComparisonNext = new KeyOrButton(Keys.Keypad6),
                            PauseKey = null,
                            ToggleGlobalHotkeys = null,
                            GlobalHotkeysEnabled = false,
                            DeactivateHotkeysForOtherPrograms = false,
                            DoubleTapPrevention = true,
                            HotkeyDelay = 0f
                        }
                    }
                },
                WarnOnReset = true,
                LastComparison = Run.Run.PersonalBestComparisonName,
                RaceViewer = new SRLRaceViewer(),
                AgreedToSRLRules = false,
                SimpleSumOfBest = false,
                ComparisonGeneratorStates = new Dictionary<string, bool>()
                {
                    { BestSegmentsComparisonGenerator.ComparisonName, true },
                    { BestSplitTimesComparisonGenerator.ComparisonName, false },
                    { AverageSegmentsComparisonGenerator.ComparisonName, true },
                    { WorstSegmentsComparisonGenerator.ComparisonName, false},
                    { PercentileComparisonGenerator.ComparisonName, false },
                    { LatestRunComparisonGenerator.ComparisonName, false },
                    { NoneComparisonGenerator.ComparisonName, false }
                }
            };
        }
    }
}
