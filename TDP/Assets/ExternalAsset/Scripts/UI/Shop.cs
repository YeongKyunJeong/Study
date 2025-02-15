using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TDP
{

    public class Shop : MonoBehaviour
    {
        private GameManager gameManager;
        private BuildManager buildManager;

        public void Initialize()
        {
            gameManager = GameManager.Instance;
            buildManager = BuildManager.Instance;
        }

        public void SelectStandardTurret()
        {
            buildManager.SetTurretToBuild(buildManager.standardTurretPrefeb);
        }
        public void SelectSecondTurret()
        {
            buildManager.SetTurretToBuild(buildManager.secondTurretPrefab);
        }
    }

}