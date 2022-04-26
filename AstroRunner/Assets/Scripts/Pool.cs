using UnityEngine;
using System.Collections.Generic;

namespace Project.Scripts
{

        [System.Serializable]
        public class PoolItem  
        {
            // type of platform
            public GameObject _prefab;
            // ammount of platforms of this type
            public int _ammount;
            // control the size(expanding) of our pool
            public bool _isExpandable;
        }

        public class Pool : MonoBehaviour
        {      
            public static Pool _singleton;
                
            [Header("Pool Settings")]
                
            [Tooltip("Type of platforms that go into pool.")]
            public List<PoolItem> _items;

            [Tooltip("List of platforms that are pooled.")]
            public List<GameObject> _pooledItems;

            private void Awake() 
            {   
                CreatePool();   
            }

            private void CreatePool()
            {
        
                _singleton = this;
                // create a new list of platforms 
                // that will go into our pool
                _pooledItems = new List<GameObject>();

                foreach (PoolItem _item in _items)
                {
                    for (int i = 0; i < _item._ammount; i++)
                    { 
                        GameObject _obj = Instantiate(_item._prefab);
                        _obj.SetActive(false);
                        _pooledItems.Add(_obj);
                    }
                }
            }
    
            public GameObject GetRandom()
            {
                 Utils.Shuffle(_pooledItems);
            
                for (int i = 0; i < _pooledItems.Count; i++)
                {
                    // if our item is not active we can use it
                    if (!_pooledItems[i].activeInHierarchy)
                        return _pooledItems[i];
                }
   
                foreach (PoolItem _item in _items)
                {
                    if (_item._isExpandable)
                    {
                        GameObject _obj = Instantiate(_item._prefab);
                        _obj.SetActive(false);
                        _pooledItems.Add(_obj);
                        return _obj;
                    }
                }
                return null;
            }
        }

       
        public static class Utils
        {
            public static System.Random _r = new System.Random();

            public static void Shuffle<T>(this IList<T> _list)
            {
                /*use Fisher-Yates algorithm
                for shuffling elements in list
                */

                /*in variable n 
                we put number of  
                elements in list*/
                int _n = _list.Count;
                while(_n > 1)
                {
                    //we go backwards through list
                    _n--;
                    /*here we put a random  value 
                    between 0 and n to make sure 
                    that value stays inside list 
                    and we add it to 1 to make sure 
                    it won't start from first element
                    of our list*/
                    int _k = _r.Next(_n + 1);

                    //swap list elements
                    T _value = _list[_k];
                    _list[_k] = _list[_n];
                    _list[_n] = _value;
                }
            }
        }
    }        
    
