using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    private PointerEventData _pointerEventData;
    private List<RaycastResult> _raycastResults;

    private void Start()
    {
        _pointerEventData = new(EventSystem.current);
        _raycastResults = new List<RaycastResult>(1);
    }

    private void Update()
    {
        _pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);
        if (_raycastResults.Count > 0)
        {
            if (_raycastResults[0].gameObject.TryGetComponent(out BubbleController bubbleController))
            {
                SetCursorTexture(bubbleController.Config.CursorTexture, bubbleController.Config.CursorHotspot);
            }
        }
        else
        {
            SetCursorTexture(null, Vector2.zero);
        }
    }

    public void SetCursorTexture(Texture2D texture, Vector2 hotspot)
    {
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }
}
