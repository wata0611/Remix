using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController3 : MonoBehaviour
{
    [SerializeField] GameObject beam;
    public float AttackTime { set; private get; }
    GameManager gameManager;
    bool launched;

    // Start is called before the first frame update
    void Start()
    {
        launched = false;
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        AttackManager();
    }

    void AttackManager()
    {
        if(!launched && gameManager.ElapsedTime >= AttackTime)
        {
            LaunchBeam();
        }
    }

    void LaunchBeam()
    {
        beam.SetActive(true);
        launched = true;
    }
}
