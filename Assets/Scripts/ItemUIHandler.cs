using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerUpHandler
{
    private GameController gameController;
    private bool isBeingDragged = false;
    private GameObject parentSlot;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameController>();
        parentSlot = gameObject.transform.parent.gameObject; //dont need
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameController.DisplayTooltip(gameObject.GetComponent<Item>(), true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameController.DisplayTooltip(gameObject.GetComponent<Item>(), false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //If this is this is the first OnDrag event, send item to front of UI.
        if (!isBeingDragged)
        {
            isBeingDragged = true;
            Canvas theCanvas = gameObject.GetComponentInParent<Canvas>();
            gameObject.transform.SetParent(theCanvas.gameObject.transform);
            gameObject.transform.SetAsLastSibling();
        }

        //Attach gameobject to cursor.
        gameObject.GetComponent<Transform>().position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            isBeingDragged = false;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var raycastResult in raycastResults)
            {
                //theres gotta be a better way than a tag.
                if (raycastResult.gameObject.tag == "InventorySlot" || raycastResult.gameObject.tag == "WeaponSlot") //<--- this last conditional needs to know the item's type.
                {
                    SwapItems(raycastResult.gameObject);
                }
                else if (raycastResult.gameObject.tag == "TrashSlot")
                {
                    Debug.Log("trash not implemented yet");
                }
                else
                {

                    ResetItem(GetComponent<Item>(), parentSlot);
                    /*
                    //Reset this item back into it's parentslot.
                    gameObject.transform.SetParent(parentSlot.transform);
                    transform.localRotation = Quaternion.identity;
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    */
                }
            }
        }
    }

    private void SwapItems(GameObject targetSlot)
    {
        //targetSlot is the to slot
        //parentslot is the from slot
        Item otherItem = targetSlot.GetComponentInChildren<Item>();
        if (otherItem != null)
        {
            ResetItem(otherItem, parentSlot);
            /*

            //Move the other item underneath the parentSlot
            otherItem.transform.SetParent(parentSlot.transform);

            //Reset the local position
            otherItem.transform.localRotation = Quaternion.identity;
            otherItem.transform.localPosition = Vector3.zero;
            otherItem.transform.localScale = Vector3.one;
            */
        }
        ResetItem(GetComponent<Item>(), targetSlot);

        /*
        //Move the current item underneath the targetslot.
        gameObject.transform.SetParent(targetSlot.transform);

        //Reset the local position
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        */

        parentSlot = targetSlot;
    }


    private void ResetItem(Item item, GameObject parentSlot)
    {
        item.transform.SetParent(parentSlot.transform);

        item.transform.localRotation = Quaternion.identity;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }
}
