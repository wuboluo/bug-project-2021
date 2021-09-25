using UnityEngine;

namespace Bug.Project21.Tools
{
    public abstract class ComponentPoolSO<T> : PoolSO<T> where T : Component
    {
        private Transform _parent;
        private Transform _poolRoot;

        private Transform PoolRoot
        {
            get
            {
                if (_poolRoot != null) return _poolRoot;
                _poolRoot = new GameObject(name).transform;
                _poolRoot.SetParent(_parent);

                return _poolRoot;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (_poolRoot != null)
            {
                Destroy(_poolRoot.gameObject);
            }
        }

        public void SetParent(Transform t)
        {
            _parent = t;
            PoolRoot.SetParent(_parent);
        }

        public override T Request()
        {
            var member = base.Request();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(T member)
        {
            member.transform.SetParent(PoolRoot.transform);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override T Create()
        {
            var newMember = base.Create();
            newMember.transform.SetParent(PoolRoot.transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }
    }
}