using UnityEngine;

public class ScrollBubbleController : BubbleController
{

    int _count = 0;
    protected override void Update()
    {
        if (_clickCountView != null)
        {
            _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        var axis = Input.GetAxis("Mouse ScrollWheel");
        if ((axis < 0f || axis > 0f) && _pointerInside)
        {
            _count++;
            if (_count > 5)
            {
                _count = 0;
                OnClick();
            }
        }

        if (Input.GetMouseButtonDown(0) && _pointerInside)
        {
            _dragTimer = 0;
            _dragTimerOn = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _dragTimerOn = false;
            _dragTimer = 0;
            _isDraging = false;
        }
        if (_dragTimerOn)
        {
            _dragTimer += Time.deltaTime;
            if (_dragTimer > 0.01f)
            {
                _isDraging = true;
            }
        }
        if (_isDraging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
