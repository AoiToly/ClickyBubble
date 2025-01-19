using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeItemView> _items = new();

    public void SetData(Tool toolA, Tool toolB, Tool toolC)
    {
        _items[0].SetData(toolA, () => { gameObject.SetActive(false); });
        _items[1].SetData(toolB, () => { gameObject.SetActive(false); });
        _items[2].SetData(toolC, () => { gameObject.SetActive(false); });
    }
}
