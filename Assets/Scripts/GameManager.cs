using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;

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
    const string STATE_MANAGER_CSV_PATH = "CSV/StateManager";
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

    public bool GameOverFlg { set; get; }
    public bool GameClearFlg { set; get; }

    [SerializeField] public float EnemyAttackBufferTime = 0.7f;
    [SerializeField] public float BeatBufferTime = 0.7f;
    [SerializeField] GameObject attackWaitCanvas;
    [SerializeField] WaitUIController attackWaitController;
    [SerializeField] GameObject defenceWaitCanvas;
    [SerializeField] WaitUIController defenceWaitController;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] float gameMusicFadeTime;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject clearCanvas;
    [SerializeField] GameObject gameStartSound;
    [SerializeField] GameObject attackMain;
    [SerializeField] GameObject defenceMain;
    [SerializeField] PlayerController playerMove;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyBoss enemy;
    [SerializeField] GameStartPanelContrller startPanel;
    [SerializeField] GameFadeIn gameFadeInPanel;
    [SerializeField] public float GameEndTime = 190f;
    [SerializeField] public float GameEndFadeTime = 5f;

    float musicVolume;
    float fadeTimer;
    public bool CanvasEffectOK { set; get; }

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
        GameOverFlg = false;
        GameClearFlg = false;
        CanvasEffectOK = false;
        musicVolume = gameMusic.volume;
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
        List<string[]> dataList = CSVReader.ReadCSV(STATE_MANAGER_CSV_PATH);
        for(int i = 0; i < dataList.Count; i++)
        {
            StateData stateDataUnit;
            stateDataUnit.timing = float.Parse(dataList[i][STATE_DATE_ELAPSED_TIME_IDX]);
            stateDataUnit.phase = String2GamePhase(dataList[i][STATE_DATE_PHASE_IDX]);
            stateDataList.Add(stateDataUnit);
        }
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

    void PlayPhaseMain()
    {
        switch (PlayPhase)
        {
            case PLAY_PHASE.START_PHASE:
                StartMain();
                if (DoneStartPhase)
                {
                    if (!startPanel.DoneFade && !startPanel.FadeOK)
                    {
                        Instantiate(gameStartSound);
                        startPanel.FadeOK = true;
                    }
                    else if (startPanel.DoneFade)
                    {
                        startCanvas.SetActive(false);
                        PlayPhase = PLAY_PHASE.INIT_PHASE;
                    }
                }
                break;
            case PLAY_PHASE.INIT_PHASE:
                if(!gameFadeInPanel.DoneFade && !gameFadeInPanel.FadeOK)
                {
                    gameFadeInPanel.FadeOK = true;
                }
                else if (gameFadeInPanel.DoneFade)
                {
                    PlayPhase = PLAY_PHASE.GAME_PHASE;
                    DoneInitPhase = true;
                }
                break;
            case PLAY_PHASE.GAME_PHASE:
                if (!gameMusic.isPlaying)
                {
                    gameMusic.Play();
                }

                if (ElapsedTime < GameEndTime)
                {
                    ElapsedTime += Time.deltaTime;
                }
                else
                {
                    GameOverFlg = true;
                    DoneGamePhase = true;
                }
                StateManager();
                GamePhaseMain();
                if (DoneGamePhase)
                {
                    playerMove.Movable = false;
                    PlayPhase = PLAY_PHASE.EMD_PHASE;
                }
                break;
            case PLAY_PHASE.EMD_PHASE:
                if (GameOverFlg)
                {
                    gameOverCanvas.SetActive(true);
                    CanvasEffectOK = true;
                }
                else if(GameClearFlg){
                    clearCanvas.SetActive(true);
                    if (enemy.DoneEffect)
                    {
                        CanvasEffectOK = true;
                    }
                }
                else
                {
                    Debug.Log("END_PHASE_ERROR");
                    DoneEndPhase = true;
                    CanvasEffectOK = true;
                }

                if (CanvasEffectOK)
                {
                    fadeTimer += Time.deltaTime;
                    float volumeRate = fadeTimer / GameEndFadeTime;
                    gameMusic.volume = musicVolume * (1f - volumeRate);
                    if (fadeTimer >= GameEndFadeTime)
                    {
                        DoneEndPhase = true;
                    }
                }

                if (DoneEndPhase)
                {
                    if (Input.anyKeyDown)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
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
                if (attackMain.activeSelf)
                {
                    attackMain.SetActive(false);
                }
                if (defenceMain.activeSelf)
                {
                    defenceMain.SetActive(false);
                }


                if(BeforeGamePhase == BEFORE_GAME_PHASE.ATTACK_PHASE)
                {
                    if (!defenceWaitCanvas.activeSelf)
                    {
                        defenceWaitCanvas.SetActive(true);
                        defenceWaitController.InitTextTimer();
                    }
                }
                else
                {
                    if (!attackWaitCanvas.activeSelf)
                    {
                        attackWaitCanvas.SetActive(true);
                        attackWaitController.InitTextTimer();
                    }
                }
                break;

            case GAME_PHASE.ATTACK_PHASE:
                if (attackWaitCanvas.activeSelf)
                {
                    attackWaitCanvas.SetActive(false);
                }
                if (!attackMain.activeSelf)
                {
                    attackMain.SetActive(true);
                }


                if (BeforeGamePhase != BEFORE_GAME_PHASE.ATTACK_PHASE)
                {
                    BeforeGamePhase = BEFORE_GAME_PHASE.ATTACK_PHASE;
                }

                if (enemy.HP == 0)
                {
                    enemy.Eliminated = true;
                    GameClearFlg = true;
                    DoneGamePhase = true;
                }
                break;

            case GAME_PHASE.DEFENCE_PHASE:
                if (defenceWaitCanvas.activeSelf)
                {
                    defenceWaitCanvas.SetActive(false);
                }
                if (!defenceMain.activeSelf)
                {
                    defenceMain.SetActive(true);
                }

                if (BeforeGamePhase != BEFORE_GAME_PHASE.DEFENCE_PHASE)
                {
                    BeforeGamePhase = BEFORE_GAME_PHASE.DEFENCE_PHASE;
                }
                
                if(playerManager.HP == 0)
                {
                    GameOverFlg = true;
                    DoneGamePhase = true;
                }
                break;
        }
    }

    void StartMain()
    {
        if (Input.anyKeyDown)
        {
            DoneStartPhase = true;
        }
    }
}
