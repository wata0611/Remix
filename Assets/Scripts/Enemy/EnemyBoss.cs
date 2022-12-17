using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    const string NOTES_TAG = "Notes";
    [SerializeField] int maxHP = 1000;

    public int HP { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == NOTES_TAG)
        {
            Damage(collision.gameObject.GetComponent<NotesController>().Damage);
            Destroy(collision.gameObject);
        }
    }

    void Damage(int damage)
    {
        HP -= damage;
        Debug.Log("EnemyHP:"+HP.ToString());
    }
}
