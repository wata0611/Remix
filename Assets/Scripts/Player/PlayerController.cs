using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0.0f, jumpPower));
    }

    void MoveRight()
    {
        this.transform.Translate(1.0f*moveSpeed,0f,0f);
    }

    void MoveLeft()
    {
        this.transform.Translate(1.0f * moveSpeed, 0f, 0f);
    }
}
