using DG.Tweening;
using UnityEngine;

public class ItemAppear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
