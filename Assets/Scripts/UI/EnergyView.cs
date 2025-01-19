using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private GameObject _energyItemViewTemplate;
    private ToolController _tool;
    private List<GameObject> _energyItems = new();
    IDisposable _disposable;

    public void SetData(ToolController tool, Tool config)
    {
        _disposable?.Dispose();
        var disposableBuilder = Disposable.CreateBuilder();

        _tool = tool;
        for (int i = 0; i < config.Energy; i++)
        {
            var obj = Instantiate(_energyItemViewTemplate, transform);
            _energyItems.Add(obj);
        }

        _tool.Energy.Subscribe(energy =>
        {
            for (int i = 0; i < _energyItems.Count; i++)
            {
                _energyItems[i].transform.GetChild(0).GetComponent<Image>().color = i < energy ? Color.green : Color.red;
            }
        }).AddTo(ref disposableBuilder);

        _disposable = disposableBuilder.Build();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
