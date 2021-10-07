using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.Tools
{
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Stack<T> objs = new Stack<T>();

        public abstract IFactory<T> Factory { get; set; }

        protected bool HasBeenPrewarmed { get; set; }

        public virtual void OnDisable()
        {
            objs.Clear();
            HasBeenPrewarmed = false;
        }

        /// <summary>
        ///     Start to generate a certain amount of obj in the pool in advance for use.
        /// </summary>
        public virtual void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                Debug.LogWarning($"{name} pool has been prewarm");
                return;
            }

            for (var i = 0; i < num; i++) objs.Push(Create());
            HasBeenPrewarmed = true;
        }

        /// <summary>
        ///     Pop an obj from the pool, if there is no remaining in the pool, create a new one. ---Stack.Pop()
        /// </summary>
        public virtual T Request()
        {
            return objs.Count > 0 ? objs.Pop() : Create();
        }

        /// <summary>
        ///     Push the useless obj into the pool. ---Stack.Push()
        /// </summary>
        public virtual void Return(T member)
        {
            objs.Push(member);
        }

        /// <summary>
        ///     Created by the corresponding type factory.
        /// </summary>
        protected virtual T Create()
        {
            return Factory.Create();
        }

        /// <summary>
        ///     Multiple objs in a single request.
        /// </summary>
        public virtual IEnumerable<T> Request(int num = 1)
        {
            var members = new List<T>(num);
            for (var i = 0; i < num; i++) members.Add(Request());
            return members;
        }

        /// <summary>
        ///     Return multiple objs at a time.
        /// </summary>
        public virtual void Return(IEnumerable<T> members)
        {
            foreach (var member in members) Return(member);
        }
    }
}