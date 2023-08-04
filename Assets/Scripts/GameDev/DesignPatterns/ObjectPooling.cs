using System.Collections.Generic;
namespace GameDev.DesignPatterns
{

    public abstract class ObjectPooling<T>
    {
        private List<T> _objects;
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
