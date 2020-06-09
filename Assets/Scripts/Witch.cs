﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Witch : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 10;
    [Header("跳躍力度"), Range(1, 1000)]
    public float jumpHeight = 400;
    [Header("閃現距離"), Range(1, 1000)]
    public float flashLength = 3;
    [Header("玩家資料")]
    public PlayerData Data;

    /// <summary>
    /// 特效
    /// </summary>
    [Header("魔法彈")]
    public GameObject magicBall;   
    [Header("閃現")]
    public GameObject magicFlash;
    [Header("毒瓶")]
    public GameObject poisonBattle;
    [Header("毒瓶特效")]
    public GameObject magicPoisonBattle;
    [Header("跳躍特效")]
    public GameObject magicJump;
    /// <summary>
    /// 音效
    /// </summary>
    [Header("魔法彈音效")]
    public AudioClip audMagicBall;

    /// <summary>
    /// 按鈕
    /// </summary>
    [Header("攻擊按鈕")]
    public Button btnAtk;
    [Header("跳躍按鈕")]
    public Button btnJump;
    [Header("閃現按鈕")]
    public Button btnFlash;
    [Header("毒瓶按鈕")]
    public Button btnBattle;   
    [Header("技能3按鈕")]
    public Button btnSkill3;
    [Header("技能4按鈕")]
    public Button btnSkill4;

    /// <summary>
    /// 位置資訊
    /// </summary>
    public Transform magicPos;
    public Transform flashPos;
    public Transform throwPos;

    AudioSource aud;
    public Joystick joy;
    private Animator ani;
    private Rigidbody2D rig;
    private HpValueManager hpValueManager;

    private bool isGrounded;
    private bool isAttack;
    private bool isFlash;
    private bool isThrow;
    private bool isCasting;

    private float hp;
    private float hpMax;
    public Material witchMaterial;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        btnFlash.onClick.AddListener(UseFlash);
        btnBattle.onClick.AddListener(UseThrowBattle);
        btnAtk.onClick.AddListener(UseAttack);
        btnJump.onClick.AddListener(UseJump);
        isAttack = false;
        hpValueManager = GetComponentInChildren<HpValueManager>();
        hp = Data.HP;
        hpMax = Data.HpMax;

    }

    
    private void UseFlash()
    {
        StartCoroutine(Flash());
    }
    private void UseAttack()
    {       
        StartCoroutine(Attack());
    }
    private void UseJump()
    {
        Jump();
    }
    private void UseThrowBattle()
    {
        StartCoroutine(ThrowBattle());
    }
    private void Update()
    {
       
        Move();
        //if (Input.GetKeyDown(KeyCode.T) && isThrow == false)
        //{
        //    StartCoroutine(ThrowBattle());
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        //{
        //    Jump();
        //}
        //if (Input.GetMouseButtonDown(0) && isAttack == false)
        //{
        //    StartCoroutine(Attack());
        //}
        //if (Input.GetMouseButtonDown(1) && isFlash == false)
        //{
        //    StartCoroutine(Flash());

        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    ani.SetBool("Cast", true);

        //}

    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {            
            ani.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ani.SetBool("Walk", false);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {            
            ani.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ani.SetBool("Walk", false);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //float v = Input.GetAxis("Vertical");   //Vertical:WS上下
        float h = Input.GetAxis("Horizontal"); //Horizontal:AD左右
        transform.Translate(speed * Time.deltaTime *Mathf.Abs(h), 0, 0);

       // float joyv = joy.Vertical;
       // float joyh = joy.Horizontal;
       // transform.Translate(speed * Time.deltaTime * joyh, 0, speed * Time.deltaTime * joyv);

        Vector2 pos = transform.position;    

    }

    
    

    public IEnumerator Attack()
    {
        if(isAttack == false)
        {
            ani.SetBool("Walk", false);
            speed = 0;
            isAttack = true;
            ani.SetTrigger("Attack");
            aud.PlayOneShot(audMagicBall, 0.7f);
            yield return new WaitForSeconds(0.5f);
            GameObject temp = Instantiate(magicBall, new Vector3(magicPos.position.x, magicPos.position.y, 0), transform.rotation);

            temp.GetComponent<Rigidbody2D>().AddForce(temp.transform.right * 1000);
            temp.AddComponent<Bullet>().damage = Data.damage;            
            temp.GetComponent<Bullet>().player = true;
             yield return new WaitForSeconds(0.5f);

            isAttack = false;
            speed = 10;
        }              
        
    }           

    public IEnumerator Flash()
    {

        btnFlash.interactable = false;
        btnFlash.image.fillAmount = 0;
        speed = 0;
        Instantiate(magicFlash, new Vector3(flashPos.position.x, flashPos.position.y, 0), Quaternion.Euler(-41,0,0));
        isFlash = true;
        ani.SetTrigger("Flash");
      

        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < 50; i++)
        {
            btnFlash.image.fillAmount = Mathf.Lerp(0, 1, 0.2f);
        }
        speed = 10;
        transform.Translate(flashLength, 0, 0);
      
        
        yield return new WaitForSeconds(Data.flashCD);
        btnFlash.interactable = true;
        isFlash = false;
        
    }

    public IEnumerator ThrowBattle()
    {
        btnBattle.interactable = false;
        btnBattle.image.fillAmount = 0;

        speed = 0;
        isThrow = true;        
       
        ani.SetTrigger("Throw");
        yield return new WaitForSeconds(0.7f);
        GameObject temp = Instantiate(poisonBattle, throwPos.position+transform.right*2+transform.up*1, transform.rotation);
        temp.GetComponent<Rigidbody2D>().AddForce(temp.transform.right * 700 + temp.transform.up * 200);
        temp.GetComponent<Rigidbody2D>().AddTorque(500);
        speed = 10;
        yield return new WaitForSeconds(Data.battleCD);              
       
        btnBattle.interactable = true;
        btnBattle.image.fillAmount = 1;
        isThrow = false;

    }
    public void Jump()
    { 
        if(isGrounded == true)
        {
            isGrounded = false;
            Instantiate(magicJump, new Vector3(flashPos.position.x, flashPos.position.y, 0), Quaternion.identity);
            rig.AddForce(new Vector2(0, jumpHeight));
            ani.SetTrigger("Jump");
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "地板")
        {
            isGrounded = true;
            print("在地上");
        }
    }

    public void hurt(float damage)
    {
        hp -= damage;
        witchMaterial.color =new Color(1,0.49f,0.49f,1);
        Invoke("reColor", 0.2f);

        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp <= 0) StartCoroutine(Dead());
    }
    private void reColor()
    {
        witchMaterial.color = Color.white;
    }

    public IEnumerator Dead()
    {
        ani.SetTrigger("Dead");
        GetComponent<Witch>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
     
    }
}
