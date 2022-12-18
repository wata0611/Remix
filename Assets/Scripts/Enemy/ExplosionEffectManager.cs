using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectManager : MonoBehaviour
{
    [SerializeField] GameObject[] explosionEffect;
    [SerializeField] GameObject enemyBossObj;
    [SerializeField] float explosionSpan = 0.5f;

    float explosionTimer;
    EnemyBoss enemyBoss;
    // Start is called before the first frame update
    void Start()
    {
        enemyBoss = enemyBossObj.GetComponent<EnemyBoss>();
        explosionTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Explosion();
    }

    void Explosion()
    {
        explosionTimer += Time.deltaTime;
        if (enemyBoss.GetEffectStart() && !enemyBoss.DoneEffect && explosionTimer >= explosionSpan)
        {
            int idx = Random.Range(0, 2);
            Transform enemyBossTransform = enemyBossObj.transform;
            float posX = Random.Range(-Mathf.Sqrt(enemyBossTransform.localScale.x), Mathf.Sqrt(enemyBossTransform.localScale.x)) + enemyBossTransform.position.x;
            if(posX < enemyBossTransform.position.x - Mathf.Sqrt(enemyBossTransform.localScale.x))
            {
                posX = enemyBossTransform.position.x - Mathf.Sqrt(enemyBossTransform.localScale.x);
            }
            if(posX > enemyBossTransform.position.x + Mathf.Sqrt(enemyBossTransform.localScale.x))
            {
                posX = enemyBossTransform.position.x + Mathf.Sqrt(enemyBossTransform.localScale.x);
            }

            float posY = Random.Range(-enemyBossTransform.localScale.y, Mathf.Sqrt(enemyBossTransform.localScale.y)) + enemyBossTransform.position.y;
            if (posY < enemyBossTransform.position.y - Mathf.Sqrt(enemyBossTransform.localScale.y))
            {
                posY = enemyBossTransform.position.y - Mathf.Sqrt(enemyBossTransform.localScale.y);
            }
            if (posY > enemyBossTransform.position.y + Mathf.Sqrt(enemyBossTransform.localScale.y))
            {
                posY = enemyBossTransform.position.y + Mathf.Sqrt(enemyBossTransform.localScale.y);
            }

            Debug.Log(new Vector2(posX, posY));

            GameObject effect = Instantiate(explosionEffect[idx]);
            effect.transform.position = new Vector3(posX, posY, 0f);
            explosionTimer = 0f;
            
        }
    }
}
