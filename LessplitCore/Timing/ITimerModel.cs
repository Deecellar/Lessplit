using System;

namespace LessplitCore.Timing
{
    public interface ITimer
    {
        SplitterState CurrentState { get; set; }

        event EventHandler OnSplit;
        event EventHandler OnUndoSplit;
        event EventHandler OnSkipSplit;
        event EventHandler OnStart;
        event EventHandler<TimerState> OnReset;
        event EventHandler OnPause;
        event EventHandler OnUndoAllPauses;
        event EventHandler OnResume;
        event EventHandler OnScrollUp;
        event EventHandler OnScrollDown;
        event EventHandler OnSwitchComparisonPrevious;
        event EventHandler OnSwitchComparisonNext;

        void Start();
        void InitializeGameTime();
        void Split();
        void SkipSplit();
        void UndoSplit();
        void Reset();
        void Reset(bool updateSplits);
        void ResetAndSetAttemptAsPB();
        void Pause();
        void UndoAllPauses();
        void ScrollUp();
        void ScrollDown();
        void SwitchComparisonPrevious();
        void SwitchComparisonNext();
    }
}
