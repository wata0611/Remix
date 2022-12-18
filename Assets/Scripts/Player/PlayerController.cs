using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveLimit = 5.72f;
    [SerializeField] PlayerManager playerManager;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.D)) {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            
            MoveLeft();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0.0f, 1.0f * jumpPower));
    }

    void MoveRight()
    {
        if (transform.position.x < moveLimit)
        {
            transform.Translate(1.0f * moveSpeed, 0f, 0f);
        }
    }

    void MoveLeft()
    {
        if (transform.position.x > -moveLimit)
        {
            transform.Translate(-1.0f * moveSpeed, 0f, 0f);
        }
    }

    public void Damage(int damage)
    {
        playerManager.HP -= damage;
        Debug.Log("HP:" + playerManager.HP.ToString());
    }

}
