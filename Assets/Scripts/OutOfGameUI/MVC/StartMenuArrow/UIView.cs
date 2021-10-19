
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面 视图
/// </summary>
public class UIView : MonoBehaviour
{
    public GameObject[] UIPages;

    public Button[] UIButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UIButtons.Length; i++)
        {
            var index = i;
            UIButtons[i].onClick.AddListener(() => PagesOnOff(index));
        }
        //UIButtons[0].onClick.AddListener(() => PagesOnOff(0));
        //UIButtons[1].onClick.AddListener(() => PagesOnOff(1));
        //UIButtons[2].onClick.AddListener(() => PagesOnOff(2));
        //UIButtons[3].onClick.AddListener(() => PagesOnOff(3));
        //UIButtons[4].onClick.AddListener(() => PagesOnOff(4));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PagesOnOff(int j)
    {
        Debug.Log(j);

        for (int i = 0; i < UIPages.Length; i++)
        {
            UIPages[i].SetActive(i == j);
        }

        // UIPages.ToList().ForEach(_ => _.SetActive(UIPages.ToList().IndexOf(_) == j));
    }
}
