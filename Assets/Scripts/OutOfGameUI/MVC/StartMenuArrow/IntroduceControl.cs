using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 团队介绍界面 控制器
/// </summary>
public class IntroduceControl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// 团队介绍界面滚动的位置
    /// </summary>
    public FloatEventChannelSO introduceContentSO;

    /// <summary>
    /// 介绍界面内容滚动时间
    /// </summary>
    float rollContent = 0;
    /// <summary>
    /// 是否开始滚动
    /// </summary>
    bool isRoll = false;

    /// <summary>
    /// 滚动速度（倍数）
    /// </summary>
    public int rollTimes = 20;

    /// <summary>
    /// 方便计算滚动的距离
    /// </summary>
    RectTransform ScrollView;
    RectTransform Content;

    void Start()
    {
        ScrollView = transform.GetComponentInChildren<ScrollRect>().GetComponent<RectTransform>();
        Content = ScrollView.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (isRoll)
        {
            if (rollContent <= Content.sizeDelta.y - ScrollView.sizeDelta.y)
            {
                rollContent += Time.deltaTime * rollTimes;
                ControlIntroduceContent(rollContent);
            }
        }
        Debug.Log(isRoll);

    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetMouseButtonDown(0))
        {
            isRoll = false;
        }
        else
        {
            rollContent = Content.localPosition.y;
            isRoll = true;
        }
    }

    private void OnEnable()
    {
        rollContent = 0;
        isRoll = true;
    }

    private void OnDisable()
    {
        isRoll = false;
    }



    /// <summary>
    /// 传值给滚动位置
    /// </summary>
    /// <param name="rollTime"></param>
    public void ControlIntroduceContent(float rollTime)
    {
        introduceContentSO?.RaiseEvent(rollTime);
    }

    /// <summary>
    /// 鼠标拖拽时时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isRoll = false;
    }

    /// <summary>
    /// 松开鼠标时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        rollContent = Content.localPosition.y;
        isRoll = true;
    }
}
