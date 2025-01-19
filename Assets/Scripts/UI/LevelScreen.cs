using R3;
using System;
using TMPro;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    [SerializeField] TimerView _timerView;
    [SerializeField] ScoreView _scoreView;
    [SerializeField] TextMeshProUGUI _targetScoreText;
    IDisposable _disposable;

    public void StartLevel()
    {
        LevelController.Instance.StartLevel();
        _timerView.SetData();
        _scoreView.SetData();

        _disposable?.Dispose();
        var disposableBuilder = Disposable.CreateBuilder();

        DataManager.Instance.CurrentLevel.Subscribe(levelConfig =>
        {
            _targetScoreText.text = $"{levelConfig.TargetScore}";
        });

        _disposable = disposableBuilder.Build();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
