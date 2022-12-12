using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack1 : EnemyAttackAbstract
{
    [SerializeField] Vector3 targetPos;

    private float elapsedTime;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        Action();
    }

    protected override void Action()
    {
        MoveToTargetPos();
    }

    private void MoveToTargetPos()
    {
        float rate = Mathf.Clamp01(elapsedTime / actionTime);
        gameObject.transform.position = Vector3.Lerp(startPos, targetPos, rate);
    }
    
}
