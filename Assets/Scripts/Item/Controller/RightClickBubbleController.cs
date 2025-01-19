using UnityEngine;

public class RightClickBubbleController : BubbleController
{
    protected override void Update()
    {
        if (_clickCountView != null)
        {
            _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        if (Input.GetMouseButtonDown(1) && _pointerInside)
        {
            if (!DataManager.Instance.Dragger.Value)
            {
                OnClick();
            }
            _dragTimer = 0;
            _dragTimerOn = true;
            _lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
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
            if (DataManager.Instance.Dragger.Value)
            {
                _dragDistance += Time.deltaTime;
                _lastMousePos = Input.mousePosition;
                if (_dragDistance > 0.5f)
                {
                    _dragDistance = 0;
                    OnClick();
                }
            }
        }
    }
}
