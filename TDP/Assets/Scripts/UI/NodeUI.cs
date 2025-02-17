using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class NodeUI : MonoBehaviour
    {
        private Node target;
        [SerializeField] private GameObject uIGO;

        public void Initialize()
        {
            HideUI();
        }

        public void SetTarget(Node _target)
        {
            target = _target;
            transform.position = target.GetBuildPosition();
            uIGO.SetActive(true);
        }

        public void HideUI()
        {
            uIGO.SetActive(false);
        }
    }
}
