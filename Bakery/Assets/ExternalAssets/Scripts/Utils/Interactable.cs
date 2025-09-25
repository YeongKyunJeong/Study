using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public interface IInteractable
    {
        public void OnInteractEnter();
        public void OnInteractExit();
    }
}
