using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInfoPopupWindowBase<T> : MonoBehaviour where T:MonoBehaviour
{
    #region µ¥ÀýÄ£¿é
    protected static T _instance = null;
    protected static readonly object _locker = new object();
    public static T GetInstance
    {
        get
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                    }
                }
            }
            return _instance;
        }
    } 
    #endregion



    public abstract void OnOpenPopupInfoWindow<Item>(Item item,GridBase GridPoint);
    public abstract void OnOpenPopupInfoWindow(GridPoint gridPoint,ItemBase item,GridBase _thisGrid);

    public abstract void OnClosePopupInfoWindow();
}
