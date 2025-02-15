using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager instance;
    public static BuildManager Instance { get { return instance; } private set { instance = value; } }

    public GameObject standardTurretPrefeb;
    private GameObject turretToBuild;

    public void Initialize()
    {
        if (instance == null)
        {
            Instance = this;
        }
        turretToBuild = standardTurretPrefeb;
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
