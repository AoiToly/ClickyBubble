using R3;
using System;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    IDisposable _disposable;

    public void SetData()
    {
        _disposable?.Dispose();
        var disposableBuilder = Disposable.CreateBuilder();

        DataManager.Instance.LevelTimer.Subscribe(time =>
        {
            _timeText.text = $"{time.ToString("0.00")}s";
        });

        _disposable = disposableBuilder.Build();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
