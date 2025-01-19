using TMPro;
using UnityEngine;

public class UIFloating : MonoBehaviour
{
    Vector3 _direction = Vector2.one;
    [SerializeField] private float _speed = 100;
    [SerializeField] private TextMeshProUGUI _text;

    void Update()
    {
        if ((_direction.x > 0 && transform.position.x + _text.rectTransform.sizeDelta.x / 2 > Screen.width)
            || (_direction.x < 0 && transform.position.x - _text.rectTransform.sizeDelta.x / 2 < 0))
        {
            _direction.x *= -1;
            var color = Random.ColorHSV(0.2f, 1, 1, 1, 1, 1);
            _text.color = color;
            _text.fontMaterial.SetColor("_GlowColor", color);
        }
        if ((_direction.y > 0 && transform.position.y + _text.rectTransform.sizeDelta.y / 2 > Screen.height)
            || (_direction.y < 0 && transform.position.y - _text.rectTransform.sizeDelta.y / 2 < 0))
        {
            _direction.y *= -1;
            var color = Random.ColorHSV(0.2f, 1);
            _text.color = color;
            _text.fontMaterial.SetColor("_GlowColor", color);
        }
        transform.position += _direction * _speed * Time.deltaTime;
    }


}
