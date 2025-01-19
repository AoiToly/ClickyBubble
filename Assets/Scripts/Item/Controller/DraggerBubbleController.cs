using UnityEngine;

public class DraggerBubbleController : BubbleController
{
    protected override void Update()
    {
        if (_clickCountView != null)
        {
            _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        if (Input.GetMouseButtonDown(0) && _pointerInside)
        {
            _dragTimer = 0;
            _dragTimerOn = true;
            _lastMousePos = Input.mousePosition;
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
                _lastMousePos = Input.mousePosition;
                _dragDistance = 0;
                _dragTimerOn = false;
            }
        }
        if (_isDraging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dragDistance += (Input.mousePosition - _lastMousePos).magnitude;
            _lastMousePos = Input.mousePosition;
            if (_dragDistance > 2500)
            {
                _dragDistance = 0;
                OnClick();
            }
        }
    }
}
