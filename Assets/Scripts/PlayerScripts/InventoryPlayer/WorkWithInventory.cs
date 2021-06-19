using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWithInventory : MonoBehaviour
{
    public Camera viewCamera;

    private void FixedUpdate()
    {
        RaycastHit raycastHit;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            Debug.Log(raycastHit.transform.name);
        }
    }
}
