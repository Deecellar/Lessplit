using LessplitCore.Configuration;
using LessplitCore.Run;
using LessplitCore.Timing;
using System;
using System.Linq;

namespace LessplitCore
{
    public class SplitterState
    {
        public IRun Run { get; set; }
        public ILayout Layout { get; set; }
        public LayoutSettings LayoutSettings { get; set; }
        public ISettings Settings { get; set; }

        public AtomicDateTime AttemptStarted { get; set; }
        public AtomicDateTime AttemptEnded { get; set; }

        public TimeStamp AdjustedStartTime { get; set; }
        public TimeStamp StartTimeWithOffset { get; set; }
        public TimeStamp StartTime { get; set; }
        public TimeSpan TimePausedAt { get; set; }
        public TimeSpan? GameTimePauseTime { get; set; }
        public TimerState CurrentPhase { get; set; }
        public string CurrentComparison { get; set; }
        public TimingMethod CurrentTimingMethod { get; set; }
        public string CurrentHotkeyProfile { get; set; }

        internal TimeSpan? loadingTimes;
        public TimeSpan LoadingTimes { get { return loadingTimes ?? TimeSpan.Zero; } set { loadingTimes = value; } }
        public bool IsGameTimeInitialized
        {
            get
            {
                return loadingTimes.HasValue;
            }
            set
            {
                if (value)
                {
                    loadingTimes = LoadingTimes;
                }
                else
                    loadingTimes = null;
            }
        }
        private bool isGameTimePaused;
        public bool IsGameTimePaused
        {
            get { return isGameTimePaused; }
            set
            {
                if (!value && isGameTimePaused)
                    LoadingTimes = CurrentTime.RealTime.Value - (CurrentTime.GameTime ?? CurrentTime.RealTime.Value);
                else if (value && !isGameTimePaused)
                    GameTimePauseTime = (CurrentTime.GameTime ?? CurrentTime.RealTime);

                isGameTimePaused = value;
            }
        }

        public event EventHandler OnSplit;
        public event EventHandler OnUndoSplit;
        public event EventHandler OnSkipSplit;
        public event EventHandler OnStart;
        public event EventHandler<TimerState> OnReset;
        public event EventHandler OnPause;
        public event EventHandler OnUndoAllPauses;
        public event EventHandler OnResume;
        public event EventHandler OnScrollUp;
        public event EventHandler OnScrollDown;
        public event EventHandler OnSwitchComparisonPrevious;
        public event EventHandler OnSwitchComparisonNext;

        public event EventHandler RunManuallyModified;
        public event EventHandler ComparisonRenamed;

        public Time CurrentTime
        {
            get
            {
                var curTime = new Time();

                if (CurrentPhase == TimerState.NotRunning)
                    curTime.RealTime = TimeSpan.Zero;
                else if (CurrentPhase == TimerState.Running)
                    curTime.RealTime = TimeStamp.Now - AdjustedStartTime;
                else if (CurrentPhase == TimerState.Paused)
                    curTime.RealTime = TimePausedAt;
                else
                    curTime.RealTime = Run.Last().SplitTime.RealTime;

                if (CurrentPhase == TimerState.Ended)
                    curTime.GameTime = Run.Last().SplitTime.GameTime;
                else
                    curTime.GameTime = IsGameTimePaused
                        ? GameTimePauseTime
                        : curTime.RealTime - (IsGameTimeInitialized ? (TimeSpan?)LoadingTimes : null);

                return curTime;
            }
        }

        public TimeSpan? PauseTime
        {
            get
            {
                if (CurrentPhase == TimerState.Paused)
                    return TimeStamp.Now - StartTimeWithOffset - TimePausedAt;
                if (CurrentPhase != TimerState.NotRunning && StartTimeWithOffset != AdjustedStartTime)
                    return AdjustedStartTime - StartTimeWithOffset;
                return null;
            }
        }

        public TimeSpan CurrentAttemptDuration
        {
            get
            {
                if (CurrentPhase == TimerState.Paused || CurrentPhase == TimerState.Running)
                    return TimeStamp.Now - StartTime;
                if (CurrentPhase == TimerState.Ended)
                    return AttemptEnded - AttemptStarted;
                return TimeSpan.Zero;
            }
        }

        public int CurrentSplitIndex { get; set; }
        public ISegment CurrentSplit => (CurrentSplitIndex >= 0 && CurrentSplitIndex < Run.Count) ? Run[CurrentSplitIndex] : null;

        private SplitterState() { }

        public SplitterState(IRun run, ILayout layout, LayoutSettings layoutSettings, ISettings settings)
        {
            Run = run;
            Layout = layout;
            Settings = settings;
            LayoutSettings = layoutSettings;
            AdjustedStartTime = StartTimeWithOffset = StartTime = TimeStamp.Now;
            CurrentPhase = TimerState.NotRunning;
            CurrentSplitIndex = -1;
        }

        public object Clone()
        {
            return new SplitterState()
            {
                Run = Run.Clone() as IRun,
                Layout = Layout.Clone() as ILayout,
                Settings = Settings.Clone() as ISettings,
                LayoutSettings = LayoutSettings.Clone() as LayoutSettings,
                AdjustedStartTime = AdjustedStartTime,
                StartTimeWithOffset = StartTimeWithOffset,
                StartTime = StartTime,
                TimePausedAt = TimePausedAt,
                GameTimePauseTime = GameTimePauseTime,
                isGameTimePaused = isGameTimePaused,
                LoadingTimes = LoadingTimes,
                CurrentPhase = CurrentPhase,
                CurrentSplitIndex = CurrentSplitIndex,
                CurrentComparison = CurrentComparison,
                CurrentHotkeyProfile = CurrentHotkeyProfile,
                CurrentTimingMethod = CurrentTimingMethod,
                AttemptStarted = AttemptStarted,
                AttemptEnded = AttemptEnded
            };
        }

        public void RegisterTimerModel(ITimer model)
        {
            model.OnSplit += (s, e) => OnSplit?.Invoke(this, e);
            model.OnSkipSplit += (s, e) => OnSkipSplit?.Invoke(this, e);
            model.OnUndoSplit += (s, e) => OnUndoSplit?.Invoke(this, e);
            model.OnStart += (s, e) => OnStart?.Invoke(this, e);
            model.OnReset += (s, e) => OnReset?.Invoke(this, e);
            model.OnPause += (s, e) => OnPause?.Invoke(this, e);
            model.OnUndoAllPauses += (s, e) => OnUndoAllPauses?.Invoke(this, e);
            model.OnResume += (s, e) => OnResume?.Invoke(this, e);
            model.OnScrollUp += (s, e) => OnScrollUp?.Invoke(this, e);
            model.OnScrollDown += (s, e) => OnScrollDown?.Invoke(this, e);
            model.OnSwitchComparisonPrevious += (s, e) => OnSwitchComparisonPrevious?.Invoke(this, e);
            model.OnSwitchComparisonNext += (s, e) => OnSwitchComparisonNext?.Invoke(this, e);
        }

        public void SetGameTime(TimeSpan? gameTime)
        {
            if (CurrentTime.RealTime.HasValue && gameTime.HasValue)
            {
                LoadingTimes = CurrentTime.RealTime.Value - gameTime.Value;
                if (IsGameTimePaused)
                    GameTimePauseTime = gameTime.Value;
            }
        }

        public void CallRunManuallyModified() => RunManuallyModified?.Invoke(this, null);

        public void CallComparisonRenamed(EventArgs e) => ComparisonRenamed?.Invoke(this, e);
    }
}
