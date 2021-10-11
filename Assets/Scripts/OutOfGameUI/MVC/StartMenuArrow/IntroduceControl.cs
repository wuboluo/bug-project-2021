using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 团队介绍界面 控制器
/// </summary>
public class IntroduceControl : MonoBehaviour
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

    RectTransform ScrollView;
    RectTransform Content;

    void Start()
    {
        ScrollView = transform.GetComponentInChildren<ScrollRect>().GetComponent<RectTransform>();
        Content = ScrollView.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (isRoll && rollContent <= Content.sizeDelta.y - ScrollView.sizeDelta.y)
        {
            rollContent += Time.deltaTime * rollTimes;
            ControlIntroduceContent(rollContent);
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
}
