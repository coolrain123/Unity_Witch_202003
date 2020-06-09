using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    [Header("毒瓶特效")]
    public GameObject magicPoisonBattle;
    [Header("玩家資料")]
    public PlayerData Data;

    private void Start()
    {

        //Data = GameObject.Find("Witch").GetComponent<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如何設定打到怪也會製造煙霧 還有如何持續傷害。
        if (collision.transform.tag == "地板" || collision.transform.tag == "怪物")
        {
            //Instantiate(magicPoisonBattle, transform.position, Quaternion.identity);
            GameObject temp =  Instantiate(magicPoisonBattle, transform.position, Quaternion.Euler(90, 0, 0));
            temp.AddComponent<Bullet>().damage = 20;
            temp.GetComponent<Bullet>().player = true;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}
