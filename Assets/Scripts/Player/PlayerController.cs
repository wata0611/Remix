using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveLimit = 5.72f;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float invisibleTime = 0.5f;
    [SerializeField] float blinkVisibleTime = 0.2f;
    [SerializeField] float blinkUnVisibleTime = 0.05f;
    [SerializeField] SpriteRenderer renderer;

    Rigidbody2D rb;
    Vector2 startPos;

    bool visible;
    bool invisible;
    float invisibleTimer;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        visible = true;
        invisible = false;
        invisibleTimer = 0f;
        blinkTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Invisible();
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
        if (!invisible)
        {
            playerManager.HP -= damage;
            Debug.Log("HP:" + playerManager.HP.ToString());
            invisible = true;
            visible = false;
        }
    }

    public void InitPos()
    {
        transform.position = startPos;
    }

    void Invisible()
    {

        if (invisible)
        {
            Blink();
            InvisibleReset();
        }
    }

    void InvisibleReset()
    {
        if (invisibleTimer > invisibleTime)
        {
            invisible = false;
            invisibleTimer = 0f;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
        }
    }

    void Blink()
    {
        if (!visible)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
            if(blinkTimer >= blinkUnVisibleTime)
            {
                visible = true;
                blinkTimer = 0f;
            }
        }
        else
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
            if(blinkTimer >= blinkVisibleTime)
            {
                visible = false;
                blinkTimer = 0f;
            }
        }
    }

}
