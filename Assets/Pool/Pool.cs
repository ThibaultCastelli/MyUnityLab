using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolTC
{
    public class Pool : MonoBehaviour
    {
        #region Variables
        [Header("COMPONENTS")]
        [Tooltip("The object that will be in the pool.")]
        [SerializeField] GameObject prefab;
        [Tooltip("Object use to return null and prevent null exception.")]
        [SerializeField] GameObject nullObject;
        [Tooltip("The object where the pool will be created.")]
        [SerializeField] Transform poolContainer;
        [Space]

        [Header("POOL INFOS")]
        [SerializeField] [Range(1, 1000)] int defaultPoolSize = 1;
        [SerializeField] [Range(1, 1000)] int maxPoolSize = 10;
        [Tooltip("If set to false : The pool can be resize dynamically if you request a game object while they are all in use in the " +
            "pool.\nIf set to true : The pool will not be resize dynamically, so if all game objects are in use in the pool and you " +
            "request a new one, nothing will happen.")]
        [SerializeField] bool fixedSize;

        List<GameObject> pool;
        #endregion

        private void Awake()
        {
            // Instantiate and fill the pool
            pool = new List<GameObject>(defaultPoolSize);

            for (int i = 0; i < defaultPoolSize; i++)
            {
                GameObject currentPrefab = Instantiate(prefab, poolContainer);
                currentPrefab.SetActive(false);
                pool.Add(currentPrefab);
            }
        }

        public GameObject Request()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }

            if (fixedSize)
            {
                Debug.LogError("ERROR : The pool is full and has a fixed size.");
                return nullObject;
            }
            else if (pool.Count == maxPoolSize)
            {
                Debug.LogError("ERROR : You have reach the maximum size of the pool.");
                return nullObject;
            }
            else
            {
                GameObject newPrefab = Instantiate(prefab, poolContainer);
                pool.Add(newPrefab);
                return newPrefab;
            }
        }
    }
}
