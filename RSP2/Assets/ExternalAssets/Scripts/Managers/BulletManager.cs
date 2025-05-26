using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class BulletManager : MonoSingleton<BulletManager>
    {
        private ObjectPool objectPool;



        private void Awake()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
