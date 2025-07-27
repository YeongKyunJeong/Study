using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{

    public abstract class SceneInitializer : MonoBehaviour
    {
        protected bool isInitialize = false;

        public abstract void Initialize();

        protected void Awake()
        {
            Initialize();
        }
    }
}
