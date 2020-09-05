using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("撿取特效")]
    public GameObject magicPick;
   

    void Start()
    {
    }

   
    public void OnMouseDown()
    {
        Debug.Log("點擊到");
       
        Instantiate(magicPick, transform.position + transform.up * 2, Quaternion.identity);
        Destroy(gameObject);
    }
}
