using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug.Project21.Props;

public class BackpackGridControl : GridBaseControl<BackpackGridControl>
{
    public override void OnCreateTo(PropSO prop)
    {
        GameObject tester = null;
        GridBaseLists.Add(tester.GetComponent<GridBase>());
    }

    /// <summary>
    /// 检查该标号物体是否存在
    /// </summary>
    /// <param name="value"></param>
    public override bool OnExamine(int value)
    {

        return false;
    }
}
