using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private RectTransform _clickCountContainer;
    [SerializeField] private ClickCountView _clickCountTextTemplate;
    [SerializeField] private EnergyView _energyViewTemplate;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private DefeatPanel _defeatPanel;

    public ClickCountView SpawnClickCountText()
    {
        return Instantiate(_clickCountTextTemplate, _clickCountContainer);
    }

    public EnergyView SpawnEnergyView()
    {
        return Instantiate(_energyViewTemplate, _clickCountContainer);
    }

    public void ShowWinPanel(Tool toolA, Tool toolB, Tool toolC)
    {
        _winPanel.gameObject.SetActive(true);
        _winPanel.SetData(toolA, toolB, toolC);
    }

    public void ShowDefeatPanel()
    {
        _defeatPanel.gameObject.SetActive(true);
        _defeatPanel.SetData();
    }

    public void DestroyAllViews()
    {
        for (int i = 0; i < _clickCountContainer.childCount; i++)
        {
            Destroy(_clickCountContainer.GetChild(i).gameObject);
        }
    }
}
