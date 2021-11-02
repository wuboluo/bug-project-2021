using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 团队介绍界面 视图
/// </summary>
public class IntroduceView : MonoBehaviour
{
    /// <summary>
    /// 监听滚动位置的事件
    /// </summary>
    public FloatEventChannelSO introduceContentSO;


    void Start()
    {

    }


    void FixedUpdate()
    {

    }

    private void OnEnable()
    {
        introduceContentSO.OnEventRaised += ControlIntroduceContent;
    }

    private void OnDisable()
    {
        introduceContentSO.OnEventRaised -= ControlIntroduceContent;
    }

    /// <summary>
    /// 页面滚动的位置
    /// </summary>
    /// <param name="pos"></param>
    public void ControlIntroduceContent(float pos)
    {
        var posOld = GetComponent<RectTransform>().localPosition;
        GetComponent<RectTransform>().localPosition = new Vector3(posOld.x, pos, posOld.z);
    }
}
