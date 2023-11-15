using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class InactivityManager : MonoBehaviour
{
    [SerializeField] private float _inactivityThreshold = 90f;
    [SerializeField] private List<Button> _trackedButtons;
    private Timer _timer;
    public static event Action ApplicationReseted;

    public void SetAllTrackableButtons() //for using in editor mode (!)
    {
        if (_trackedButtons != null) _trackedButtons.Clear();
        _trackedButtons = Resources.FindObjectsOfTypeAll<Button>().ToList();
    }

    private void Awake()
    {
        _timer = new Timer(this);
        _timer.OnTimeIsOver += ResetApplication;
        _trackedButtons.ForEach(e => e.onClick.AddListener(ResetTimer));
    }

    private void ResetTimer()
    {
        _timer.StopCountingTime();
        _timer.Set(_inactivityThreshold);
        _timer.StartCountingTime();
    }

    private void ResetApplication()
    {
        ApplicationReseted?.Invoke();
        ResetTimer();
    }
}

