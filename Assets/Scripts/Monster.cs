using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator ani;
    private float timer;

    [Header("攻擊觸發")]
    public Transform atkTri;
    [Header("怪物資料")]
    public MonsterData Data;
    //[Header("補血藥水")]
    //public GameObject propHp;
    //[Header("加速藥水")]
    //public GameObject propCd;
    //[Header("子彈")]
    //public GameObject bullet;
    //public SpriteRenderer[] spr;

    private float hp;
   

    void Start()
    {
        hp = Data.HP;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 3)
        {
            print(timer);
            timer = 0;
            
            float r = Random.Range(0f, 1f);
            print(r);
            if (r < 0.5) walk();
            else idle();
        }



    }

    private void walk()
    {
        ani.SetBool("Walk", true);
        transform.Translate(-3 * Time.deltaTime, 0, 0);

    }
    private void attack()
    {
        ani.SetBool("Walk", false);

        ani.SetTrigger("Attack");

    }
    private void idle()
    {
        ani.SetBool("Walk", false);
    }
    private void dead()
    {
        ani.SetTrigger("Dead");
    }
    private void hurt()
    {
        GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        Invoke("reColor", 0.2f);
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "玩家")
        {
            attack();
        }
        if (other.gameObject.tag == "魔法")
        {
            hurt();           
        }
    }
   

}
