using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager instance;
    public static BuildManager Instance { get { return instance; } private set { instance = value; } }

    public GameObject standardTurretPrefeb;
    public GameObject secondTurretPrefab;
    private GameObject turretToBuild;

    public void Initialize()
    {
        if (instance == null)
        {
            Instance = this;
        }
        turretToBuild = standardTurretPrefeb;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
