using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace DancingLineFanmade.Level
{
    public class ObjectPool<T> where T : Object
    {
        private Queue<T> pool = new Queue<T>();
        private int size = 0;

        public int Size
        {
            get => size;
            set => size = value;
        }

        public void DestoryAll()
        {
            foreach (T t in pool) Object.Destroy(t.GameObject());
            pool.Clear();
        }

        public void Add(T t)
        {
            pool.Enqueue(t);
        }

        public T First()
        {
            return pool.Dequeue();
        }

        public bool Full
        {
            get => pool.Count >= size;            
        }
    }
}