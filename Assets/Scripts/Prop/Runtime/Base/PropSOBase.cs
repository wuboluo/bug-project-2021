using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    public abstract class PropSOBase : ScriptableObject
    {
        public virtual string Name { get; set; }
        public virtual Texture Icon { get; set; }
    }
}