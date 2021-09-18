using Sirenix.OdinInspector;
using UnityEngine;

public class AttrBase
{
    public bool enable;
    [HideLabel] public Vector2 range = new Vector2(0, 10);

    // 若 enable == false, 返回 minValue
    public float GetValue()
    {
        return enable ? Random.Range(range.x, range.y) : float.MinValue;
    }
}