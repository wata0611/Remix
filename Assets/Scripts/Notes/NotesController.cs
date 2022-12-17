using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] Transform targetPosTransform;
    public float BeatTime { set; private get; }
    GameManager gameManager;
    float elapsedTimeFromSpawn;
    float actionDuration;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        elapsedTimeFromSpawn = 0f;
        actionDuration = BeatTime - gameManager.ElapsedTime;
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
        float rate = Mathf.Clamp01(elapsedTimeFromSpawn / actionDuration);
        gameObject.transform.position = Vector3.Lerp(startPos, targetPosTransform.position , rate);
    }
}
