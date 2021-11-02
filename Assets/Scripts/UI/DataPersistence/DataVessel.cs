using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 读写档脚本案例
/// </summary>
public class DataVessel : MonoBehaviour
{
    public Text HPValueView = null;
    public Text AtkValueView = null;
    public Text DefValueView = null;

    public List<Texture2D> IconList = new List<Texture2D>();
    public RawImage Icon = null;
    public Button ChangeIconButton = null;


    public Button AddHpButton = null;
    public Button SubHpButton = null;

    public Button AddAtkButton = null;
    public Button SubAtkButton = null;

    public Button AddDefButton = null;
    public Button SubDefButton = null;

    public Button SaveButton = null;
    public Button LoadButton = null;

    public MyData myData = new MyData();


    void Start()
    {
        #region 存读档
        SaveButton.onClick.AddListener(() =>
{
            #region 游戏存档
            DataPersistence.GameDataPersistence(myData);
            #endregion
        });
        LoadButton.onClick.AddListener(() =>
        {
            #region 读取存档数据案例
            try
            {
                myData = DataPersistence.GetPersistenceGameData<MyData>();

                HPValueView.text = myData.MyHp.ToString();
                AtkValueView.text = myData.MyAtk.ToString();
                DefValueView.text = myData.MyDef.ToString();
                Texture2D tex2d = new Texture2D(255, 255);
                tex2d.LoadImage(myData.Tex2D);
                Icon.texture = tex2d;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message.ToString());
                //throw;
                return;
            }
            #endregion
        }); 
        #endregion

        ChangeIconButton.onClick.AddListener(() =>
        {
            Icon.texture = IconList[UnityEngine.Random.Range(0, IconList.Count)];
            myData.Tex2D = (Icon.texture as Texture2D).EncodeToPNG();
        });

        AddHpButton.onClick.AddListener(() =>
        {
            myData.MyHp++;
            HPValueView.text = myData.MyHp.ToString();
        });
        SubHpButton.onClick.AddListener(() =>
        {
            myData.MyHp--;
            HPValueView.text = myData.MyHp.ToString();
        });

        AddAtkButton.onClick.AddListener(() =>
        {
            myData.MyAtk++;
            AtkValueView.text = myData.MyAtk.ToString();
        });
        SubAtkButton.onClick.AddListener(() =>
        {
            myData.MyAtk--;
            AtkValueView.text = myData.MyAtk.ToString();
        });

        AddDefButton.onClick.AddListener(() =>
        {
            myData.MyDef++;
            DefValueView.text = myData.MyDef.ToString();
        });
        SubDefButton.onClick.AddListener(() =>
        {
            myData.MyDef--;
            DefValueView.text = myData.MyDef.ToString();
        });

    }

}
