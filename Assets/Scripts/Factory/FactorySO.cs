using UnityEngine;

namespace Bug.Project21.Tools
{
    public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
    {
        public abstract T Create();
    }
}