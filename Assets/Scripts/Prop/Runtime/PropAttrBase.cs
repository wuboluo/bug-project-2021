using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Props
{
    public class PropAttrBase
    {
        /// <summary>
        /// This 'PropAttr' value validity, configured in the Prop-Editor-Window.
        /// </summary>
        public bool enable;

        [HideLabel] public Vector2 range = new Vector2(0, 10);

        /// <summary>
        /// Used to record the initialized value, used to Get.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Initialize the 'PropAttr' attr value, only need to call once.
        /// </summary>
        public void InitValue()
        {
            Value = (int) (enable ? Random.Range(range.x, range.y) : int.MinValue);
        }
    }
}