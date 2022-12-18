using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController3 : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] GameObject attackSound;
    [SerializeField] float destroyTime = 1f;
    public float AttackTime { set; private get; }
    GameManager gameManager;
    bool launched;
    bool doneDestroy;

    // Start is called before the first frame update
    void Awake()
    {
        launched = false;
        doneDestroy = false;
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
    }

    void AttackManager()
    {
        if(!launched && gameManager.ElapsedTime >= AttackTime)
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
        if(launched && !beam.activeSelf)
        {
            if (!doneDestroy)
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
    
}
