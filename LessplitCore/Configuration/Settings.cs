using LessplitCore.Input;
using LessplitCore.Run;
using LessplitCore.Timing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LessplitCore.Configuration
{
    public class Settings : ISettings
    {
        public IDictionary<string, HotkeyProfile> HotkeyProfiles { get; set; }
        public KeyOrButton ScrollUp { get; set; }
        public KeyOrButton ScrollDown { get; set; }
        public IList<RecentSplitsFile> RecentSplits { get; set; }
        public IList<string> RecentLayouts { get; set; }
        public string LastComparison { get; set; }
        public bool WarnOnReset { get; set; }
        public bool AgreedToSRLRules { get; set; }
        public bool SimpleSumOfBest { get; set; }
        public IRaceViewer RaceViewer { get; set; }
        public IList<string> ActiveAutoSplitters { get; set; }
        public IDictionary<string, bool> ComparisonGeneratorStates { get; set; }

        public Settings()
        {
            RecentSplits = new List<RecentSplitsFile>();
            RecentLayouts = new List<string>();
            ActiveAutoSplitters = new List<string>();
        }

        public object Clone()
        {
            return new Settings()
            {
                HotkeyProfiles = HotkeyProfiles.ToDictionary(x => x.Key, x => (HotkeyProfile)(x.Value.Clone())),
                ScrollUp = ScrollUp,
                ScrollDown = ScrollDown,
                WarnOnReset = WarnOnReset,
                RecentSplits = new List<RecentSplitsFile>(RecentSplits),
                RecentLayouts = new List<string>(RecentLayouts),
                LastComparison = LastComparison,
                RaceViewer = RaceViewer,
                AgreedToSRLRules = AgreedToSRLRules,
                SimpleSumOfBest = SimpleSumOfBest,
                ActiveAutoSplitters = new List<string>(ActiveAutoSplitters),
                ComparisonGeneratorStates = new Dictionary<string, bool>(ComparisonGeneratorStates)
            };
        }

        public void AddToRecentSplits(string path, IRun run, TimingMethod lastTimingMethod, string lastHotkeyProfile)
        {
            var foundRecentSplitsFile = RecentSplits.FirstOrDefault(x => x.Path == path);
            if (foundRecentSplitsFile.Path != null)
                RecentSplits.Remove(foundRecentSplitsFile);

            var recentSplitsFile = new RecentSplitsFile(path, run, lastTimingMethod, lastHotkeyProfile);

            RecentSplits.Add(recentSplitsFile);

            while (RecentSplits.Count > 50)
                RecentSplits.RemoveAt(0);
        }
        public void RegisterHotkeys(CompositeHook hook, string hotkeyProfileName)
        {
            try
            {
                UnregisterAllHotkeys(hook);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            try
            {
                //   RegisterScrolling(hook);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            if (HotkeyProfiles.ContainsKey(hotkeyProfileName))
            {
                var hotkeyProfile = HotkeyProfiles[hotkeyProfileName];
                var deactivateForOtherPrograms = hotkeyProfile.GlobalHotkeysEnabled && hotkeyProfile.DeactivateHotkeysForOtherPrograms;
                if (hotkeyProfile.SplitKey != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.SplitKey, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.ResetKey != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.ResetKey, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.SkipKey != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.SkipKey, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.UndoKey != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.UndoKey, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.PauseKey != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.PauseKey, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.ToggleGlobalHotkeys != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.ToggleGlobalHotkeys, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.SwitchComparisonPrevious != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.SwitchComparisonPrevious, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
                if (hotkeyProfile.SwitchComparisonNext != null)
                {
                    try
                    {
                        RegisterHotkey(hook, hotkeyProfile.SwitchComparisonNext, deactivateForOtherPrograms);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }



        public void AddToRecentLayouts(string path)
        {
            if (RecentLayouts.Contains(path))
                RecentLayouts.Remove(path);
            RecentLayouts.Add(path);
            while (RecentLayouts.Count > 10)
                RecentLayouts.RemoveAt(0);
        }

        private void RegisterHotkey(CompositeHook hook, KeyOrButton key, bool deactivateForOtherPrograms)
        {
            //hook.RegisterHotKey(key);
            if (deactivateForOtherPrograms && key.IsKey)
            {
                var args = new Eto.Forms.KeyEventArgs(key.Key, Eto.Forms.KeyEventType.KeyUp);
                var modifiers = (args.Alt ? Eto.Forms.Keys.Alt : Eto.Forms.Keys.None)
                    | (args.Shift ? Eto.Forms.Keys.Shift : Eto.Forms.Keys.None)
                    | (args.Control ? Eto.Forms.Keys.Control : Eto.Forms.Keys.None);
                //  HotkeyHook.Instance.RegisterHotKey(modifiers, args.KeyCode);
            }
        }

        public void UnregisterAllHotkeys(CompositeHook hook)
        {
            //  hook.UnregisterAllHotkeys();
            // HotkeyHook.Instance.UnregisterAllHotkeys();
        }
    }
}
