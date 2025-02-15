using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color initColor;
    private static Vector3 positinoOffset;

    private GameObject turretOnMe;
    private Renderer rend;

    private BuildManager buildManager;

    public void Initialize()
    {
        buildManager = BuildManager.Instance;

        rend = GetComponent<Renderer>();
        rend.material.color = initColor;
        positinoOffset = 0.5f * Vector3.up;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        rend.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        if (turretOnMe != null)
        {
            Debug.Log("Can't build there"); // To do : Display on screen
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        turretOnMe = Instantiate(turretToBuild, transform.position + positinoOffset, transform.rotation);

    }

    private void OnMouseExit()
    {
        rend.material.color = initColor;
    }
}
