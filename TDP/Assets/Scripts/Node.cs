using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TDP
{

    public class Node : MonoBehaviour
    {
        public Color hoverColor;
        public Color initColor;
        public Color notEnoughMoneyColor;
        private static Vector3 positionOffset;

        [Header("Optional")]
        [SerializeField] private GameObject turretGOOnMe;
        public GameObject SetTurretOnNode { set => turretGOOnMe = value; }
        private Renderer rend;

        private BuildManager buildManager;

        public void Initialize()
        {
            buildManager = BuildManager.Instance;

            rend = GetComponent<Renderer>();
            rend.material.color = initColor;
            positionOffset = 0.5f * Vector3.up;
        }

        public Vector3 GetBuildPosition()
        {
            return transform.position + positionOffset;
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!buildManager.CanBuild)
            {
                return;
            }

            if (buildManager.GetTurretToBuild != null)
            {
                if (buildManager.HasEnoughMoney)
                {
                    rend.material.color = hoverColor;
                }
                else
                {
                    rend.material.color = notEnoughMoneyColor;
                }
            }

        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (turretGOOnMe != null)
            {
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
            {
                return;
            }

            buildManager.BuildTurretOn(this);

        }

        private void OnMouseExit()
        {
            rend.material.color = initColor;
        }
    }
}
