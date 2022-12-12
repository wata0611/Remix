using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttackAbstract : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float actionTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Damage(ref PlayerController player)
    {
        player.Damage(damage);
        Destroy(gameObject);

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Damage(ref player);
        }   
    }

    protected abstract void Action();
}
