using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemySpawner : MonoBehaviour
{
    const string CSV_PATH = "CSV/EnemyAttackManager";
    const int TIME_IDX = 0;
    const int POS_IDX = 1;
    const int BEAM_ANGLE_IDX = 2;

    [SerializeField] GameObject enemyObject;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform[] targetTransform;
    struct EnemySpawnData
    {
        public float attackTime;
        public int targetPosNum;
        public float beamAngle;
    }

    List<EnemySpawnData> enemySpawnDataList;
    int spawnEnemyIdx;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnDataList = new List<EnemySpawnData>();
        SetEnemySpawnDataList();
        spawnEnemyIdx = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SpawnManager();
    }

    void SetEnemySpawnDataList()
    {
        List<string[]> dataList = CSVReader.ReadCSV(CSV_PATH);
        for(int i = 0; i < dataList.Count; i++)
        {
            EnemySpawnData enemySpawnData;
            enemySpawnData.attackTime = float.Parse(dataList[i][TIME_IDX]);
            enemySpawnData.targetPosNum = int.Parse(dataList[i][POS_IDX]);
            enemySpawnData.beamAngle = float.Parse(dataList[i][BEAM_ANGLE_IDX]);
            enemySpawnDataList.Add(enemySpawnData);
        }
    }

    void SpawnManager()
    {
        if (spawnEnemyIdx < enemySpawnDataList.Count) {
            float enemySpawnTime = enemySpawnDataList[spawnEnemyIdx].attackTime - gameManager.EnemyAttackBufferTime;
            if (gameManager.ElapsedTime >= enemySpawnTime)
            {
                SpawnEnemy(enemySpawnDataList[spawnEnemyIdx].attackTime ,enemySpawnDataList[spawnEnemyIdx].targetPosNum, enemySpawnDataList[spawnEnemyIdx].beamAngle);
                spawnEnemyIdx++;
            }
        }
    }

    void SpawnEnemy(float attackTime, int targetPosNum, float beamAngle)
    {
        GameObject enemyObj = Instantiate(enemyObject, targetTransform[targetPosNum].position, Quaternion.Euler(0, 0, beamAngle));
        enemyObj.GetComponent<EnemyAttackController3>().AttackTime = attackTime;
        enemyObj.GetComponent<EnemyAttackController3>().SetGameManager(gameManager);
    }
}
