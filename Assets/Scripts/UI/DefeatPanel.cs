using UnityEngine;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void SetData()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            LevelController.Instance.TryAgain();
            gameObject.SetActive(false);
        });
    }
}
