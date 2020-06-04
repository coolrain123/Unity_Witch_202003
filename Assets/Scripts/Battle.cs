using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    [Header("毒瓶特效")]
    public GameObject magicPoisonBattle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "地板")
        {
            //Instantiate(magicPoisonBattle, transform.position, Quaternion.identity);
            Instantiate(magicPoisonBattle, transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}
