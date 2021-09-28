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
        /// 预热：开始在池中预先生成一定数量的 obj 给予使用
        /// </summary>
        /// <param name="num"></param>
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
        /// 请求：从池中弹出一个 obj，若池中没有剩余则创建一个新的（stack.pop 弹栈）
        /// </summary>
        /// <returns></returns>
        public virtual T Request()
        {
            return objs.Count > 0 ? objs.Pop() : Create();
        }

        /// <summary>
        /// 回收：将暂时无用的 obj 压入池中（stack.push 压栈）
        /// </summary>
        /// <param name="member"></param>
        public virtual void Return(T member)
        {
            objs.Push(member);
        }

        /// <summary>
        /// 创建：由对应类型工厂创建
        /// </summary>
        /// <returns></returns>
        protected virtual T Create()
        {
            return Factory.Create();
        }

        /// <summary>
        /// 单次请求出多个 obj
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Request(int num = 1)
        {
            var members = new List<T>(num);
            for (var i = 0; i < num; i++) members.Add(Request());
            return members;
        }

        /// <summary>
        /// 单次回收多个 obj
        /// </summary>
        /// <param name="members"></param>
        public virtual void Return(IEnumerable<T> members)
        {
            foreach (var member in members) Return(member);
        }
    }
}