using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utils;

public enum GAME_PHASE
{
    ATTACK_PHASE,
    DEFENCE_PHASE,
    WAIT_PHASE,
}

public enum PLAY_PHASE
{
    START_PHASE,
    INIT_PHASE,
    GAME_PHASE,
    EMD_PHASE,
}

public enum BEFORE_GAME_PHASE
{
    ATTACK_PHASE,
    DEFENCE_PHASE,
}

public class GameManager : MonoBehaviour
{
    string stateManagerCSVPath = "CSV/StateManager";
    const int STATE_DATE_ELAPSED_TIME_IDX = 0;
    const int STATE_DATE_PHASE_IDX = 1;
    struct StateData
    {
        public float timing;
        public GAME_PHASE phase;
    }

    private List<StateData> stateDataList;
    private int stateDataIdx;

    public GAME_PHASE GamePhase { set; get; }
    public BEFORE_GAME_PHASE BeforeGamePhase { set; get; }
    public PLAY_PHASE PlayPhase { set; get; }
    public float ElapsedTime { set; get; }

    public bool DoneStartPhase { set; get; }
    public bool DoneInitPhase { set; get; }
    public bool DoneGamePhase { set; get; }
    public bool DoneEndPhase { set; get; }

    [SerializeField] public float EnemyAttackBufferTime { set; get; }
    [SerializeField] public float BeatBufferTime { set; get; }

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
        PlayPhase = PLAY_PHASE.START_PHASE;
        BeforeGamePhase = BEFORE_GAME_PHASE.DEFENCE_PHASE;
        stateDataIdx = 0;
        DoneStartPhase = false;
        DoneInitPhase = false;
        DoneGamePhase = false;
        DoneEndPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayPhaseMain();
    }

    void StateManager()
    {
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
        List<string[]> dataList = CSVReader.ReadCSV(stateManagerCSVPath);
        for(int i = 0; i < dataList.Count; i++)
        {
            StateData stateDataUnit;
            stateDataUnit.timing = float.Parse(dataList[i][STATE_DATE_ELAPSED_TIME_IDX]);
            stateDataUnit.phase = String2GamePhase(dataList[i][STATE_DATE_PHASE_IDX]);
            stateDataList.Add(stateDataUnit);
        }
    }

    // string��GAME_PHASE �ŕԂ�
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

    void PlayPhaseMain()
    {
        switch (PlayPhase)
        {
            case PLAY_PHASE.START_PHASE:

                break;
            case PLAY_PHASE.INIT_PHASE:
                break;
            case PLAY_PHASE.GAME_PHASE:
                ElapsedTime += Time.deltaTime;
                StateManager();
                break;
            case PLAY_PHASE.EMD_PHASE:
                break;
            default:
                break;
        }
    }

    void GamePhaseMain()
    {
        switch (GamePhase)
        {
            case GAME_PHASE.WAIT_PHASE:
                break;
            case GAME_PHASE.ATTACK_PHASE:
                if(BeforeGamePhase != BEFORE_GAME_PHASE.DEFENCE_PHASE)
                {
                    BeforeGamePhase = BEFORE_GAME_PHASE.DEFENCE_PHASE;
                }
                break;
            case GAME_PHASE.DEFENCE_PHASE:
                if (BeforeGamePhase != BEFORE_GAME_PHASE.ATTACK_PHASE)
                {
                    BeforeGamePhase = BEFORE_GAME_PHASE.ATTACK_PHASE;
                }
                break;
        }
    }
}
