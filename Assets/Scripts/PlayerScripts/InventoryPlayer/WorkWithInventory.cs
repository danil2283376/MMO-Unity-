using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkWithInventory : MonoBehaviour
{
    public Camera viewCamera;
    // FOR TEST BEFORE DELETE!!!!!
    public Text text;
    public LayerMask layerMask;
    //private void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //    {
    //        //Debug.Log("Clicked on the UI");
    //        PointerEventData pointerData = new PointerEventData(EventSystem.current);
    //        pointerData.position = Input.mousePosition;

    //        List<RaycastResult> result = new List<RaycastResult>();
    //        EventSystem.current.RaycastAll(pointerData, result);

    //        if (result.Count > 0)
    //        {
    //            for (int i = 0; i < result.Count; i++)
    //            {
    //                if (result[i].gameObject.layer == LayerMask.NameToLayer("UIInventorySlot"))
    //                {
    //                    //result[i].gameObject.
    //                    Debug.Log(result[i].gameObject.name);
    //                }
    //            }
    //        }
    //    }
    //}



    ////private void FixedUpdate()
    ////{
    ////ÑÄÅËÀÒÜ RAYCAST ÄËß ÈÍÂÅÍÒÀÐß, ËÓ× ÍÅ ÐÅÀÃÈÐÓÅÒ ÍÀ ÈÍÂÅÍÒÀÐÜ!!!;
    //RaycastHit raycastHit;
    //Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
    //if (Physics.Raycast(ray, out raycastHit))
    //{
    //    text.text = raycastHit.transform.name;
    //    GameObject inventorySlot = raycastHit.transform.gameObject;
    //    if (inventorySlot.GetComponent<Image>() == true)
    //        Debug.Log(inventorySlot.name);
    //    //Debug.Log(raycastHit.transform.name);
    //}
    //}
}
