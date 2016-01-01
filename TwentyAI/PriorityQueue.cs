using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//not finished 
//the data structure should be modified to max heap
namespace TwentyAI
{
    public class PriorityQueue<T>
    {
        private List<T> _data;
        private Dictionary<T, int> _dict;
        public PriorityQueue()
        {
            _data = new List<T>();
            _dict = new Dictionary<T, int>();
        }
        public int Count()
        {
            return _data.Count;
        }
        public void Push(T data,int value)
        {
            _data.Add(data);
            _dict.Add(data, value);
        }
        public T Pop()
        {
            List<int> allValue = new List<int>();
            for (int i = 0; i < _data.Count; i++)
            {
                allValue.Add( _dict[_data[i]] );
            }
            allValue.Sort();
            for (int i = 0; i < _data.Count; i++)
            {
                if (_dict[_data[i]] == allValue[_data.Count - 1])
                {
                    _dict.Remove(_data[i]);
                    T temp = _data[i];
                    return temp;
                }
            }
            return _data[0];
        }
        public bool Contains(T d)
        {
            return _data.Contains(d);
        }

    }
}
