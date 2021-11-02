using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPositionView : MonoBehaviour
{
    /// <summary>
    /// 监听箭头位置的事件
    /// </summary>
    public IntEventChannelSO arrowPositionSO;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        arrowPositionSO.OnEventRaised += SetArrowPosition;
    }

    private void OnDisable()
    {
        arrowPositionSO.OnEventRaised -= SetArrowPosition;
    }

    /// <summary>
    /// 移动箭头到制定位置
    /// </summary>
    public void SetArrowPosition(int positionY)
    {
        var pos = GetComponent<RectTransform>().localPosition;
        GetComponent<RectTransform>().localPosition = new Vector3(pos.x, positionY, pos.z);
    }
}
