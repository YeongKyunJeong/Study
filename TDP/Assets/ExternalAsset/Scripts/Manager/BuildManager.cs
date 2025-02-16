using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class BuildManager : MonoBehaviour
    {
        private static BuildManager instance;
        public static BuildManager Instance { get { return instance; } private set { instance = value; } }

        private GameManager gameManager;
        //private PlayerStats playerStats;

        public GameObject standardTurretPrefeb;
        public GameObject missileTurretPrefab;
        [SerializeField] private TurretBluePrint turretToBuild;
        public bool CanBuild { get { return turretToBuild != null; } }
        public bool HasEnoughMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

        public void Initialize()
        {
            if(gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            //playerStats = gameManager.GetPlayerStats;

            if (instance == null)
            {
                Instance = this;
            }
        }

        public void BuildTurretOn(Node node)
        {
            if(PlayerStats.Money < turretToBuild.cost)
            {
                Debug.Log("Not Enough Money");
                return;
            }

            //playerStat.Money = 
            PlayerStats.Money -= turretToBuild.cost;
            Debug.Log($"{PlayerStats.Money} left");

            node.SetTurretOnNode = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);

        }

        public void SelectTurretToBuild(TurretBluePrint turretBP)
        {
            turretToBuild = turretBP;
        }

    }
}
