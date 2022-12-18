using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController3 : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] GameObject attackSound;
    [SerializeField] float destroyTime = 1f;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] float fadeTime = 0.5f;
    public float AttackTime { set; private get; }
    GameManager gameManager;
    bool launched;
    bool doneDestroy;
    bool fadeOutOK;
    bool doneFadeIn;
    float fadeTimer;

    // Start is called before the first frame update
    void Awake()
    {
        fadeTimer = 0f;
        fadeOutOK = false;
        doneFadeIn = false;
        launched = false;
        doneDestroy = false;
        transform.position -= transform.right * Time.deltaTime * fadeTime * 2f;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        AttackManager();
        VanishEnemy();
        Fade();
        FadeIn();
    }

    void AttackManager()
    {
        if (!launched && gameManager.ElapsedTime >= AttackTime)
        {
            Instantiate(attackSound);
            LaunchBeam();
        }
    }

    void LaunchBeam()
    {
        beam.SetActive(true);
        launched = true;
    }

    void VanishEnemy()
    {
        if (launched && !beam.activeSelf)
        {
            if (!fadeOutOK)
            {
                fadeTime = 0f;
                fadeOutOK = true;

            }
        }
    }

    void Fade()
    {
        if (fadeOutOK)
        {
            fadeTimer += Time.deltaTime;
            float alpha = (1f - fadeTimer / fadeTime);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
            transform.position -= transform.right * Time.deltaTime; 
            if (!doneDestroy && fadeTimer > fadeTime)
            {
                Destroy(gameObject);
                doneDestroy = true;
            }
        }
    }

    public bool GetLaunchedFlg()
    {
        return launched;
    }

    void FadeIn()
    {
        if (!doneFadeIn)
        {
            fadeTimer += Time.deltaTime;
            float alpha = fadeTimer / fadeTime;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
            transform.position += transform.right * Time.deltaTime;
            if(fadeTimer > fadeTime)
            {
                doneFadeIn = true;
                fadeTimer = 0f;
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
            }
        }
    }

}
