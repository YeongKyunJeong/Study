using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color initColor;
    private static Vector3 positinoOffset;

    private GameObject turretOnMe;
    private Renderer rend;
    
    public void Initialize()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = initColor;
        positinoOffset = 0.5f * Vector3.up;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
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
