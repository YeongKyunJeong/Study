using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DataManager : MonoBehaviour
    {
        public TableDataLoader TableDataLoader { get; private set; }
       
        public void Initialize()
        {
            TableDataLoader = new TableDataLoader();

            TableDataLoader.Initialize();
        }
    }


}
