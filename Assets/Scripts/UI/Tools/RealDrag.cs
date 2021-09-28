using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RealDrag : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    Vector3 drag_offset = Vector3.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                break;
            case PointerEventData.InputButton.Right:
                float x = Mathf.Abs(eventData.position.x - transform.position.x);
                float y = Mathf.Abs(eventData.position.y - transform.position.y);
                drag_offset.x = eventData.position.x > transform.position.x ? -x : x;
                drag_offset.y = eventData.position.y > transform.position.y ? -y : y;
                break;
            case PointerEventData.InputButton.Middle:
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                break;
            case PointerEventData.InputButton.Right:
                Vector3 Drag = new Vector3(eventData.position.x + drag_offset.x, eventData.position.y + drag_offset.y, 0);
                transform.position = Drag;
                break;
            case PointerEventData.InputButton.Middle:
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                break;
            case PointerEventData.InputButton.Right:
                drag_offset = Vector3.zero;
                break;
            case PointerEventData.InputButton.Middle:
                break;
        }
    }
}
