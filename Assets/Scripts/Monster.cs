﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Monster : MonoBehaviour
{
    public Animator ani;
    private float timer;
    private float walkTimer;
    int walkDirection = 0;
    
    

    //[Header("攻擊觸發")]
    //public GameObject atkTri;
    //[Header("視野觸發")]
    //public GameObject seeTri;
    [Header("攻擊特效")]
    public GameObject atkEff;
    [Header("怪物資料")]
    public MonsterData Data;

    //[Header("補血藥水")]
    //public GameObject propHp;
    //[Header("加速藥水")]
    //public GameObject propCd;
    //[Header("子彈")]
    //public GameObject bullet;
    //public SpriteRenderer[] spr;

    private HpValueManager hpValueManager;

    private float hp;
    private float hpMax;
    private float r;
    public Image imgHP;
    public Text textDmg;
    public Transform atkPos;
    public Material monMaterial;

    void Start()
    {
        //如何設定攻擊觸發與視野觸發
        hp = Data.HP;
        hpMax = Data.HpMax;
        hpValueManager = GetComponentInChildren<HpValueManager>();
        //atkTri = GameObject.Find("攻擊觸發");
        //seeTri = GameObject.Find("視野觸發");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AttackCheck());

        timer += Time.deltaTime;
        if (timer > 3)
        {
            print(timer);
            timer = 0;

            r = Random.Range(0f, 1f);                    
            
        }

        if (r < 0.5) walk();
        else idle();

    }

    private void walk()
    {
        ani.SetBool("Walk", true);
        walkTimer += Time.deltaTime;
       
        if (walkTimer>3)
        {
            walkTimer = 0;
            walkDirection = Random.Range(1, 3);
                      
        }

        if (walkDirection == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);  //0或180??
            transform.Translate(-3 * Time.deltaTime, 0, 0);
            hpValueManager.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (walkDirection == 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);  //0或180??
            transform.Translate(-3 * Time.deltaTime, 0, 0);
            hpValueManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        }


    }
    private void attack()
    {
        ani.SetBool("Walk", false);
        Instantiate(atkEff, new Vector3(atkPos.position.x,atkPos.position.y), Quaternion.identity);
        ani.SetTrigger("Attack");
    }
    private void idle()
    {
        ani.SetBool("Walk", false);
    }
  
    private IEnumerator Dead()
    {
        ani.SetTrigger("Dead");
        GetComponent<Monster>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

   

    public void hurt(float damage)
    {
        hp -= damage;
        monMaterial.color = Color.red;      
        Invoke("reColor", 0.2f);
        
        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp <= 0) StartCoroutine(Dead());
              
    }

    public void poisoning(float damage)
    {
        for (int i = 0; i < 5; i++)
        {
            hp -= damage;
            monMaterial.color = Color.green;
            Invoke("reColor", 0.2f);
        }
       
        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp <= 0) StartCoroutine(Dead());

    }
    private void reColor()
    {
        monMaterial.color = Color.white;
       
    }


    




    private IEnumerator AttackCheck()
    {

        // RaycastHit hit;  //區域變數 碰撞資訊:用來存放射線打到的物件;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + new Vector3(0, 5, 0), -transform.right, 5);
        
        if(hit2D.collider.tag == "玩家")
        {
            print("遇到玩家");
            attack();
        }
        yield return new WaitForSeconds(Data.cd);
    }

    //繪製圖示，僅在場景顯示給開發者觀看
    private void OnDrawGizmos()
    {
        //圖示顏色
        Gizmos.color = Color.red;

        //前方Z transform.forward
        //右方X transform.right
        //上方Y transform.up
        //繪製射線(起點,方向*長度)
        Gizmos.DrawRay(transform.position + new Vector3(0,5,0) , transform.right * -5);


    }

}
