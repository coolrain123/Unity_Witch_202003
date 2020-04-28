using UnityEngine;

public class Witch : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 5;
    [Header("跳躍高度"), Range(1, 1000)]
    public float jumpHeight = 5;

    public Animator ani;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Move();
       
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ani.SetBool("Walk", true);

        }

        float v = Input.GetAxis("Vertical");   //Vertical:WS上下
        float h = Input.GetAxis("Horizontal"); //Horizontal:AD左右
        transform.Translate(speed * Time.deltaTime * h, 0, 0);

        //float joyv = joy.Vertical;
        //float joyh = joy.Horizontal;
        //transform.Translate(speed * Time.deltaTime * joyh, 0, speed * Time.deltaTime * joyv);

        Vector2 pos = transform.position;    

    }

    public void Jump()
    {
        transform.Translate(0, jumpHeight, 0);
        ani.SetTrigger("Jump");
      
    }

}
