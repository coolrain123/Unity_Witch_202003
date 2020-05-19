using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform Witch;

    [Header("追蹤速度"), Range(1, 100)]
    public float speed = 1;

    private void LateUpdate()
    {
        CameraTrack();
    }

    public void CameraTrack()
    {
        Vector3 witpos = Witch.position;       
        witpos.y = 7.5f;
        witpos.z = -1f;
        transform.position = Vector3.Lerp(transform.position, witpos, 0.4f * Time.deltaTime * speed);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, 10.7f, 200f);
   
        transform.position = pos;
    }
}

