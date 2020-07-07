using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{

    public float damage;
    public bool player;
    public int duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如何設定打到怪也會製造煙霧 還有如何持續傷害。
        if (player && collision.transform.tag == "怪物")
        {
            print("打到怪");
            //Instantiate(magicPoisonBattle, transform.position, Quaternion.identity);
           
            transform.GetComponent<CircleCollider2D>().enabled = false;
         
            collision.GetComponent<Monster>().StartCoroutine(collision.GetComponent<Monster>().StartPoison(damage, duration));
            Destroy(gameObject);
        }      
    }
}
