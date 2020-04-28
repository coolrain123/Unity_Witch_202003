using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 5;
    [Header("虛擬搖桿")]
    public Joystick joy;

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        float v = Input.GetAxis("Vertical");   //Vertical:WS上下
        float h = Input.GetAxis("Horizontal"); //Horizontal:AD左右
        transform.Translate(speed * Time.deltaTime * h, 0, speed * Time.deltaTime * v);

        float joyv = joy.Vertical;
        float joyh = joy.Horizontal;
        transform.Translate(speed * Time.deltaTime * joyh, 0, speed * Time.deltaTime * joyv);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, 30, 70);
        transform.position = pos;

    }
    private void Update()
    {
        Move();
    }


}