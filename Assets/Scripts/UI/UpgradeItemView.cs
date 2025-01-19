using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Button _button;

    public void SetData(Tool item, Action onClick)
    {
        _icon.sprite = item.Icon;
        _name.text = item.Name;
        _description.text = item.Description;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            onClick?.Invoke();
            DataManager.Instance.Tools.Add(item);
            LevelController.Instance.StartNextLevel();
        });
    }
}
