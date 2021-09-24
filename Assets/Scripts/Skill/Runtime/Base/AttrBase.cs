using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Props
{
    public class AttrBase
    {
        /// <summary>
        ///     Value 有效性，在 道具编辑器 中配置
        /// </summary>
        public bool enable;

        [HideLabel] public Vector2 range = new Vector2(0, 10);

        private float? value = default;

        public float? Value
        {
            get
            {
                if (!enable) return null;
                return value.Equals(default) ? Random.Range(range.x, range.y) : value;
            }
        }

        // /// <summary>
        // /// 初始化数值，只能调用一次
        // /// </summary>
        // public void InitValue()
        // {
        //     Value = enable ? Random.Range(range.x, range.y) : float.MinValue;
        // }
    }
}