using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum GAME_PHASE
{
    ATTACK_PHASE,
    DEFENCE_PHASE,
    WAIT_PHASE,
}

public class GameManager : MonoBehaviour
{
    const int STATE_DATE_ELAPSED_TIME_IDX = 0;
    const int STATE_DATE_PHASE_IDX = 1;
    struct StateData
    {
        public float timing;
        public GAME_PHASE phase;
    }

    [SerializeField] string stateManagerCSVPath;
    private List<StateData> stateDataList;
    private int stateDataIdx;

    public GAME_PHASE GamePhase { set; get; }
    public float ElapsedTime { set; get; }

    private void Awake()
    {
        stateDataList = new List<StateData>();
        SetStateDataList();
    }

    // Start is called before the first frame update
    void Start()
    {
        ElapsedTime = 0f;
        GamePhase = GAME_PHASE.WAIT_PHASE;
        stateDataIdx = 0;
        Debug.Log(stateDataList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;

        StateManager();
    }

    void StateManager()
    {
        //Debug.Log(ElapsedTime);
        //Debug.Log(stateDataIdx);
        if (stateDataIdx < stateDataList.Count)
        {
            if (stateDataList[stateDataIdx].timing <= ElapsedTime)
            {
                UpdateState(stateDataList[stateDataIdx].phase);
                Debug.Log("elapsed time:" + ElapsedTime.ToString() + ", update phase:" + GamePhase.ToString());
                stateDataIdx++;
            }
        }
    }

    void UpdateState(GAME_PHASE gamePhase)
    {
        GamePhase = gamePhase;
    }

    void SetStateDataList()
    {
        List<string[]> dataList = ReadCSV(stateManagerCSVPath);
        for(int i = 0; i < dataList.Count; i++)
        {
            StateData stateDataUnit;
            stateDataUnit.timing = float.Parse(dataList[i][STATE_DATE_ELAPSED_TIME_IDX]);
            stateDataUnit.phase = String2GamePhase(dataList[i][STATE_DATE_PHASE_IDX]);
            stateDataList.Add(stateDataUnit);
        }
    }

    // ”Ä—p“I‚ÈCSVReader
    List<string[]> ReadCSV(string filePath)
    {
        List<string[]> dataList = new List<string[]>();
        TextAsset csv = Resources.Load(filePath) as TextAsset;
        StringReader reader = new StringReader(csv.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if(line.StartsWith("#"))
            {
                continue;
            }
            dataList.Add(line.Split(','));
        }

        return dataList;
    }

    // string‚ðGAME_PHASE ‚Å•Ô‚·
    GAME_PHASE String2GamePhase(string str)
    {
        switch (str)
        {
            case "WAIT":
                return GAME_PHASE.WAIT_PHASE;
            case "ATTACK":
                return GAME_PHASE.ATTACK_PHASE;
            case "DEFENCE":
                return GAME_PHASE.DEFENCE_PHASE;
            default:
                Debug.LogError("GamePhase not Found");
                return GAME_PHASE.WAIT_PHASE;
        }
    }
}
