using R3;
using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    IDisposable _disposable;

    public void SetData()
    {
        _disposable?.Dispose();
        var disposableBuilder = Disposable.CreateBuilder();

        DataManager.Instance.LevelScore.Subscribe(score =>
        {
            _scoreText.text = $"{score}";
        });

        _disposable = disposableBuilder.Build();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
