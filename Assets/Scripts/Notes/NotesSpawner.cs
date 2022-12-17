using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class NotesSpawner : MonoBehaviour
{
    const string NOTES_CSV_PATH = "CSV/Notes";

    const int TIME_IDX = 0;
    const int POS_NUM_IDX = 1;
    const int NOTES_TYPE_IDX = 2;

    enum NOTES_TYPE
    {
        LANE_1
    }

    [SerializeField] Transform[] spawnPosTransform;
    [SerializeField] GameObject notes;
    [SerializeField] GameManager gameManager;

    struct NotesData
    {
        public float beatTime;
        public int spawnPosNum;
        public NOTES_TYPE notesType;
    }

    List<NotesData> notesDataList;
    int spawnIdx;

    // Start is called before the first frame update
    void Start()
    {
        notesDataList = new List<NotesData>();
        SetNotesDataList();
        spawnIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnManager();
    }

    void SetNotesDataList()
    {
        List<string[]> dataList = CSVReader.ReadCSV(NOTES_CSV_PATH);
        for (int i = 0; i < dataList.Count; i++)
        {
            NotesData notesData;
            notesData.beatTime = float.Parse(dataList[i][TIME_IDX]);
            notesData.spawnPosNum = 0;
            notesData.notesType = NOTES_TYPE.LANE_1;
            notesDataList.Add(notesData);
        }
    }

    void SpawnManager()
    {
        
        if (spawnIdx < notesDataList.Count)
        {
            float spawnTime = notesDataList[spawnIdx].beatTime - gameManager.BeatBufferTime;
            if (gameManager.ElapsedTime >= spawnTime)
            {
                SpawnEnemy(notesDataList[spawnIdx].beatTime, notesDataList[spawnIdx].spawnPosNum, spawnIdx);
                spawnIdx++;
            }
        }
    }

    void SpawnEnemy(float beatTime, int targetPosNum, int idx)
    {
        GameObject notesObj = Instantiate(notes);
        notesObj.transform.position = transform.position;
        notesObj.GetComponent<NotesController>().BeatTime = beatTime;
        notesObj.GetComponent<NotesController>().Number = idx;
        notesObj.GetComponent<NotesController>().SetGameManager(gameManager);
    }
}
