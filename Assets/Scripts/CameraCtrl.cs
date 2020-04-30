using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform Witch;

    [Header("追蹤速度"), Range(1, 100)]
    public float speed = 1;

    private void LateUpdate()
    {
        Track();
    }

    public void Track()
    {
        Vector3 witpos = Witch.position;       
        witpos.y = 7.5f;
        witpos.z = -1f;
        transform.position = Vector3.Lerp(transform.position, witpos, 0.4f * Time.deltaTime * speed);
    }
}

