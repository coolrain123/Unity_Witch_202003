﻿
using UnityEngine;
[CreateAssetMenu(fileName = "玩家資料", menuName = "Witch/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("血量"), Range(200, 10000)]
    public float HP;
    public float HpMax;
    
   
    [Header("攻擊力"), Range(50, 1000)]
    public float damage;


}