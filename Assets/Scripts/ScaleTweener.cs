using UnityEngine;

public class ScaleTweener : MonoBehaviour
{
    [SerializeField] private float _range = 0.4f;
    [SerializeField] private float _speed = 1;

    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime * _speed;
        transform.localScale = Vector3.one * (1 + Mathf.Sin(_timer) * _range);
    }
}
