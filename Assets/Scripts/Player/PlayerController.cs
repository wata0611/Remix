using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHP = 50;
    [SerializeField] float jumpPower = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveLimit = 5.72f;

    Rigidbody2D rb;
    public int HP { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HP = maxHP;
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
}
