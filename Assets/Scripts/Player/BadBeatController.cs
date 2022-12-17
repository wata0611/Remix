using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBeatController : MonoBehaviour
{
    const string NOTES_TAG = "Notes";
    [SerializeField] JudgeBeatManager manager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == NOTES_TAG)
        {
            if (collision.gameObject.GetComponent<NotesController>().EnableBeat)
            {
                manager.AddJudgeBadBeat(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == NOTES_TAG)
        {
            manager.RemoveJudgeBadBeat(collision.gameObject);
        }
    }
}
