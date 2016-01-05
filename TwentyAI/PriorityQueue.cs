using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//not finished 
//the data structure should be modified to max heap
//modify to max heap done
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
            //_data.Add(data);
            _dict.Add(data, value);

            _data.Add(data);
            int ci = _data.Count - 1; // child index; start at end
            while (ci > 0)
            {
                int pi = (ci - 1) / 2; // parent index
                if (_dict[_data[ci]] >= _dict[_data[pi]]) break; // child item is larger than (or equal) parent so we're done
                T tmp = _data[ci]; _data[ci] = _data[pi]; _data[pi] = tmp;
                ci = pi;
            }
        }
        public T Pop()
        {
            /*List<int> allValue = new List<int>();
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
            return _data[0];*/
            // assumes pq is not empty; up to calling code
            int li = _data.Count - 1; // last index (before removal)
            T frontItem = _data[0];   // fetch the front
            _data[0] = _data[li];
            _data.RemoveAt(li);

            --li; // last index (after removal)
            int pi = 0; // parent index. start at front of pq
            while (true)
            {
                int ci = pi * 2 + 1; // left child index of parent
                if (ci > li) break;  // no children so done
                int rc = ci + 1;     // right child
                if (rc <= li && _dict[_data[rc]] < _dict[_data[ci]]) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                    ci = rc;
                if (_dict[_data[pi]] <= _dict[_data[ci]]) break; // parent is smaller than (or equal to) smallest child so done
                T tmp = _data[pi]; _data[pi] = _data[ci]; _data[ci] = tmp; // swap parent and child
                pi = ci;
            }
            return frontItem;
        }
        public bool Contains(T d)
        {
            return _data.Contains(d);
        }

    }
}
