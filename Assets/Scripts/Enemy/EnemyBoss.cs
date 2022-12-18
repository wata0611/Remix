using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    const string NOTES_TAG = "Notes";
    [SerializeField] int maxHP = 1000;
    [SerializeField] float noneSpan = 1f;
    [SerializeField] float eliminatedTime = 6f;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] float randXrange = 0.1f;
    [SerializeField] float downSpeed = 1f;
    [SerializeField] GameObject bossEliminatedSound;

    public int HP { set; get; }
    public bool Eliminated { set; get; }

    float eliminatedEffectTimer;
    bool effectStart = false;
    public bool DoneEffect { set; get; }

    Vector3 initPos;

    bool playEliminatedSound;

    // Start is called before the first frame update
    void Awake()
    {
        initPos = transform.position;
        HP = maxHP;
        Eliminated = false;
        eliminatedEffectTimer = 0f;
        effectStart = false;
        DoneEffect = false;
        playEliminatedSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        EliminatedEffect();
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
        if (HP > 0)
        {
            HP -= damage;
            if(HP < 0)
            {
                HP = 0;
            }
            Debug.Log("EnemyHP:" + HP.ToString());
        }
    }

    void EliminatedEffect()
    {
        if (Eliminated && !DoneEffect)
        {
            if (!playEliminatedSound)
            {
                Instantiate(bossEliminatedSound);
                playEliminatedSound = true;
            }

            eliminatedEffectTimer += Time.deltaTime;
            if (eliminatedEffectTimer > noneSpan)
            {
                effectStart = true;
            }

            if (effectStart)
            {
                FadeEffect(eliminatedEffectTimer);
            }
        }
    }

    void FadeEffect(float timer)
    {
        float alpha = 1f - ((timer - noneSpan) / eliminatedTime);
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        Magnitude();
        if(timer > eliminatedTime)
        {
            DoneEffect = true;
        }
    }

    void Magnitude()
    {
        float randomX = Random.Range(-randXrange, randXrange);
        float posX = transform.position.x + randomX;
        if(posX < initPos.x - randXrange)
        {
            posX = initPos.x - randXrange;
        }
        else if(posX > initPos.x + randXrange){
            posX = initPos.x + randXrange;
        }
        transform.position = new Vector3(posX, transform.position.y - Time.deltaTime * downSpeed, 0f);
    }
}
