using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    const string NOTES_TAG = "Notes";
    [SerializeField] JudgeBeatManager manager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == NOTES_TAG)
        {
            manager.AddJudgeGoodBeat(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == NOTES_TAG)
        {
            if (manager.JudgeGoodBeatContains(collision.gameObject))
            {
                collision.gameObject.GetComponent<NotesController>().EnableBeat = false;
                collision.gameObject.GetComponent<NotesController>().Status = NotesController.NOTES_STATUS.MISS;
                collision.gameObject.GetComponent<NotesController>().ColorChanger();
            }
            manager.RemoveJudgeGoodBeat(collision.gameObject);
        }
    }
}
