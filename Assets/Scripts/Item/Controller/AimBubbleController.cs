using UnityEngine;

public class AimBubbleController : BubbleController
{
    float _count;
    protected override void Update()
    {
        if (_clickCountView != null)
        {
            _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        if (_pointerInside)
        {
            _count += Time.deltaTime;
            if (_count > 1)
            {
                _count = 0;
                OnClick();
            }
        }
    }
}
