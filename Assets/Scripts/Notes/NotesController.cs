using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public enum NOTES_STATUS
    {
        BEFORE_JUDGE,
        GOOD,
        BAD,
        MISS,
    }

    public enum MOVE_TO_TARGET
    {
        BEAT,
        ENEMY,
        MISS_END,
    }

    public NOTES_STATUS Status;

    MOVE_TO_TARGET moveToTarget;

    public float BeatTime { set; private get; }
    public int Damage { private set; get; }

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] float missEndDuration = 5f;
    [SerializeField] int damage = 10;
    GameManager gameManager;
    float elapsedTimeFromSpawn;
    float elapsedTimeFromMiss;
    float actionDuration;
    Vector2 startPos;
    Vector2 targetPos;
    Vector2 endPos;
    Vector2 enemyPos;

    bool moveToEndPos;
    
    public bool EnableBeat { set; get; }
    public int Number { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosVec3 = GameObject.FindGameObjectWithTag("TargetPos").transform.position;
        Vector3 endTargetPosVec3 = GameObject.FindGameObjectWithTag("endTargetPos").transform.position;
        Vector3 enemyTargetPosVec3 = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        targetPos = new Vector2(targetPosVec3.x, targetPosVec3.y);
        endPos = new Vector2(endTargetPosVec3.x, endTargetPosVec3.y);
        enemyPos = new Vector2(enemyTargetPosVec3.x, enemyTargetPosVec3.y);
        startPos = new Vector2(transform.position.x, transform.position.y);
        elapsedTimeFromSpawn = 0f;
        elapsedTimeFromMiss = 0f;
        actionDuration = BeatTime - gameManager.ElapsedTime;
        Status = NOTES_STATUS.BEFORE_JUDGE;
        EnableBeat = true;
        moveToEndPos = false;
        moveToTarget = MOVE_TO_TARGET.BEAT;
        Damage = damage;
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTargetPos();
    }

    private void MoveToTargetPos()
    {
        if(Status == NOTES_STATUS.GOOD && moveToTarget != MOVE_TO_TARGET.ENEMY)
        {
            moveToTarget = MOVE_TO_TARGET.ENEMY;
        }
        switch (moveToTarget)
        {
            case MOVE_TO_TARGET.BEAT:
                elapsedTimeFromSpawn += Time.deltaTime;
                float rate = Mathf.Clamp01(elapsedTimeFromSpawn / actionDuration);

                gameObject.transform.position = Vector2.Lerp(startPos, targetPos, rate);

                if (transform.position.x == targetPos.x)
                {
                    moveToTarget = MOVE_TO_TARGET.MISS_END;
                }
                break;

            case MOVE_TO_TARGET.MISS_END:
                elapsedTimeFromMiss += Time.deltaTime;
                float missEndRate = Mathf.Clamp01(elapsedTimeFromMiss / missEndDuration);
                gameObject.transform.position = Vector2.Lerp(targetPos, endPos, missEndRate);
                if (transform.position.x == targetPos.x)
                {
                    Destroy(gameObject);
                }
                break;
            case MOVE_TO_TARGET.ENEMY:
                elapsedTimeFromMiss += Time.deltaTime;
                float enemyRate = Mathf.Clamp01(elapsedTimeFromMiss / missEndDuration);
                gameObject.transform.position = Vector2.Lerp(targetPos, enemyPos, enemyRate);
                break;
            default:
                break;

        }
    }

    public void ColorChanger()
    {
        if(Status == NOTES_STATUS.GOOD)
        {
            renderer.color = new Color(1f,0f,0f,1f);
        }
        else if(Status == NOTES_STATUS.MISS){
            renderer.color = new Color(0f, 0f, 1f, 1f);
        }
        else if(Status == NOTES_STATUS.BAD)
        {
            renderer.color = new Color(0f, 1f, 0f, 1f);
        }
    }
}
