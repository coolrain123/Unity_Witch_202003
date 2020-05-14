using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public Transform Witch;
  
    public float yPos;

    [Header("追蹤速度"), Range(1, 100)]
    public float speed = 1;

    private void LateUpdate()
    {
        trace();
    }

    public void trace()
    {
        Vector3 witpos = Witch.position;
        witpos.y = yPos;
        witpos.z = 0f;
        transform.position = Vector3.Lerp(transform.position, witpos, 0.4f * Time.deltaTime * speed);
    }
}
