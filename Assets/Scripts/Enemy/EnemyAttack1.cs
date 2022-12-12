using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack1 : EnemyAttackAbstract
{
    [SerializeField] Vector3 targetPos;

    GameManager gameManager;

    private float actionDuration; // ê∂ê¨Ç≥ÇÍÇƒÇ©ÇÁÉAÉNÉVÉáÉìäÆóπÇ‹Ç≈ÇÃéûä‘
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        actionDuration = actionTime - gameManager.ElapsedTime;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Action();
    }

    protected override void Action()
    {
        MoveToTargetPos();
    }

    private void MoveToTargetPos()
    {
        float rate = Mathf.Clamp01(elapsedTimeFromSpown / actionDuration);
        gameObject.transform.position = Vector3.Lerp(startPos, targetPos, rate);
    }
    
}
