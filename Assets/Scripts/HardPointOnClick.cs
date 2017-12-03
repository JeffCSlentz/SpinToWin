using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPointOnClick : MonoBehaviour, IHasSwappableChild
{
    //public turretController turret;


    public void Swap(GameObject gameObject)
    {
        
    }


    private void Awake()
    {
        
    }
    
    private void OnMouseDown()
    {
        //gameObject.SetActive(!gameObject.activeSelf);
        Ray2D ray = new Ray2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log(ray);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            //SendMessage()
        }
    }

    void Update()
    {
        
    }
}
