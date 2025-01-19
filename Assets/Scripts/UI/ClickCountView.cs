using R3;
using System;
using TMPro;
using UnityEngine;

public class ClickCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    private IDisposable _disposable;


    public void SetData(BubbleController item)
    {
        _disposable?.Dispose();
        var disposableBuilder = Disposable.CreateBuilder();

        item.ClickCount.Subscribe(count =>
        {
            _countText.text = count.ToString();
        }).AddTo(ref disposableBuilder);

        _disposable = disposableBuilder.Build();

    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
