using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack2 : EnemyAttackAbstract
{
    [SerializeField] Vector3 lauchDirection;
    [SerializeField] GameObject beam;

    GameManager gameManager;
    public bool LaunchFlg { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        LaunchFlg = false;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        beam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Action();
    }

    protected override void Action()
    {
        if (actionTime <= gameManager.ElapsedTime)
        {
            Launch();
        }
        
    }


    void Launch()
    {
        if (!LaunchFlg)
        {
            if (!beam.activeSelf)
            {
                beam.SetActive(true);
                beam.transform.localEulerAngles = lauchDirection;
                beam.transform.Translate(transform.up * 1);
            }
            LaunchFlg = true;
        }
    }

}
