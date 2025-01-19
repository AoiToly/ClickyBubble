using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private LevelScreen _levelScreen;

    private void Start()
    {
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.AddListener(() =>
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            GetComponent<CanvasGroup>().interactable = false;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                _levelScreen.StartLevel();
            });
            _levelScreen.gameObject.SetActive(true);
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
