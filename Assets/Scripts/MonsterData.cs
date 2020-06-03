
using UnityEngine;
[CreateAssetMenu(fileName = "怪物資料", menuName = "Witch/怪物資料")]
public class MonsterData : ScriptableObject
{
    [Header("血量"), Range(200, 10000)]
    public float HP;
    public float HpMax;   
    [Header("攻擊冷卻"), Range(0, 1.5f)]
    public float cd = 0.7f;
   
    [Header("攻擊力"), Range(1f, 1000f)]
    public float damage = 2f;


}