using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    [Header("毒瓶特效")]
    public GameObject magicPoisonBattle;
    

    public float damage;
    public bool player;
    public int duration;

    private void Start()
    {
               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如何設定打到怪也會製造煙霧 還有如何持續傷害。
        if (player && collision.transform.tag =="怪物")
        {
            print("打到怪");
            //Instantiate(magicPoisonBattle, transform.position, Quaternion.identity);
            GameObject temp =  Instantiate(magicPoisonBattle, transform.position, Quaternion.Euler(90, 0, 0));
            temp.GetComponent<CircleCollider2D>().enabled = false;
            
            //StartCoroutine(collision.GetComponent<Monster>().StartPoison(damage,duration));
            collision.GetComponent<Monster>().StartCoroutine(collision.GetComponent<Monster>().StartPoison(damage,duration));        
            Destroy(gameObject);
        }


        else if (player && collision.tag == "地板")
        {
            print("打地板");
            GameObject temp = Instantiate(magicPoisonBattle, transform.position, Quaternion.Euler(90, 0, 0));

            temp.GetComponent<Poison>().damage = damage;
            temp.GetComponent<Poison>().duration = duration;
            temp.GetComponent<Poison>().player = true;
            Destroy(gameObject);
        }
    }
   
}
