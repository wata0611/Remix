using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    [SerializeField] GameObject seObj;
    public int HP { set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        HP = maxHP;
    }

    public void Damage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
            Instantiate(seObj);
            Debug.Log("HP:" + HP.ToString());
        }
    }
}
