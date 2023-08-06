using System.Collections.Generic;
namespace GameDev.DesignPatterns
{
    [System.Serializable]
    public abstract class ObjectPooling<T>
    {
        private List<T> _objects;

        protected void Init()
        {
            _objects = new List<T>();
        }
        public T Request()
        {
            if (_objects.Count > 0)
            {
                T obj = _objects[0];
                _objects.Remove(obj);
                return obj;
            }
            else
            {
                return InstanceObject();
            }

        }
        public void ReleaseObject(T obj)
        {
            _objects.Add(obj);
        }

        protected abstract T InstanceObject();

    }
}
