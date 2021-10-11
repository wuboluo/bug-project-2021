using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ArrowPositionControl : MonoBehaviour
{
    /// <summary>
    /// 监听箭头位置的事件
    /// </summary>
    public IntEventChannelSO arrowPositionSO;

    /// <summary>
    /// EventTrigger 组件
    /// </summary>
    private EventTrigger eventTrigger;
    /// <summary>
    /// 属性
    /// </summary>
    public EventTrigger EventTrigger
    {
        get
        {
            if (eventTrigger == null)
            {
                eventTrigger = this.GetComponent<EventTrigger>();
            }
            return eventTrigger;
        }
    }

    void Start()
    {
        AddPointerEvent(EventTriggerType.PointerEnter, GiveArrowPosition);
    }


    void Update()
    {

    }

    /// <summary>
    /// 给 EventTrigger 添加监听事件
    /// </summary>
    /// <param name="eventTriggerType">触发类型</param>
    /// <param name="unityAction">方法</param>
    void AddPointerEvent(EventTriggerType eventTriggerType, UnityAction<BaseEventData> unityAction)
    {
        // 如果没有 EventTrigger 组件，则动态添加组件
        if (EventTrigger == null)
        {
            gameObject.AddComponent<EventTrigger>();
        }
        // 定义回掉函数，委托
        UnityAction<BaseEventData> action = new UnityAction<BaseEventData>(unityAction);
        // 定义所要绑定的事件类型
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            // 设置事件类型
            eventID = eventTriggerType
        };
        // 设置回掉函数
        entry.callback.AddListener(action);
        // 添加触发事件到 EventTrigger 组件上
        EventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// 监听的事件（方法）
    /// </summary>
    /// <param name="baseEventData"></param>
    public void GiveArrowPosition(BaseEventData baseEventData)
    {
        arrowPositionSO.RaiseEvent((int)transform.localPosition.y);
    }
}
