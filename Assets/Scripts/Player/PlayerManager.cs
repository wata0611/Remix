using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    public int HP { set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        HP = maxHP;
    }

    public void Damage(int damage)
    {
        HP -= damage;
        Debug.Log("HP:" + HP.ToString());
    }
}
