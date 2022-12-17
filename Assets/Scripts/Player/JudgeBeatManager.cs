using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBeatManager : MonoBehaviour
{
    List<GameObject> judgeGoodTargetObjList;
    List<GameObject> judgeBadTargetObjList;
    // Start is called before the first frame update
    void Start()
    {
        judgeGoodTargetObjList = new List<GameObject>();
        judgeBadTargetObjList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        JudgeBeat();
    }

    void JudgeBeat()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(judgeGoodTargetObjList.Count > 0)
            {
                GameObject notesObj = judgeGoodTargetObjList[0];
                NotesController notes = notesObj.GetComponent<NotesController>();
                notes.EnableBeat = false;
                notes.Status = NotesController.NOTES_STATUS.GOOD;
                notes.ColorChanger();

                RemoveJudgeBadBeat(notesObj);
                RemoveJudgeGoodBeat(notesObj);
                return;
            }

            if(judgeBadTargetObjList.Count > 0)
            {
                GameObject notesObj = judgeBadTargetObjList[0];
                NotesController notes = notesObj.GetComponent<NotesController>();
                notes.EnableBeat = false;
                notes.Status = NotesController.NOTES_STATUS.BAD;
                notes.ColorChanger();

                RemoveJudgeBadBeat(notesObj);
                return;
            }
        }
    }

    public void AddJudgeGoodBeat(GameObject beat)
    {
        if (beat.GetComponent<NotesController>().EnableBeat)
        {
            judgeGoodTargetObjList.Add(beat);
        }
    }

    public void RemoveJudgeGoodBeat(GameObject beat)
    {
        if (judgeGoodTargetObjList.Contains(beat))
        {
            judgeGoodTargetObjList.Remove(beat);
        }
    }

    public void AddJudgeBadBeat(GameObject beat)
    {
        judgeBadTargetObjList.Add(beat);
    }

    public void RemoveJudgeBadBeat(GameObject beat)
    {
        if (judgeBadTargetObjList.Contains(beat))
        {
            judgeBadTargetObjList.Remove(beat);
        }
    }

    public bool JudgeGoodBeatContains(GameObject beat)
    {
        return judgeGoodTargetObjList.Contains(beat);
    }
}
