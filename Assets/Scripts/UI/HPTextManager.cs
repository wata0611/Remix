using System;
using UnityEngine;
using UnityEngine.UI;

public class HPTextManager : MonoBehaviour
{
    [SerializeField] EnemyBoss enemy;
    [SerializeField] PlayerManager player;
    [SerializeField] Slider playerHPSlider;
    [SerializeField] Text playerHPText;
    [SerializeField] Slider enemyHPSlider;
    [SerializeField] Text enemyHPText;
    [SerializeField] Text timeText;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerHPText.text = "PLAYER：" + player.HP.ToString();
        playerHPSlider.maxValue = player.HP;
        playerHPSlider.value = player.HP;
        enemyHPText.text = "ENEMY：" + enemy.HP.ToString();
        enemyHPSlider.maxValue = enemy.HP;
        enemyHPSlider.value = enemy.HP;
        timeText.text = "TIME: " + ((int)gameManager.ElapsedTime).ToString() + "/" + gameManager.GameEndTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        SetHPUI();
    }

    void SetHPUI()
    {
        playerHPText.text = "PLAYER：" + player.HP.ToString();
        enemyHPText.text = "ENEMY：" + enemy.HP.ToString();
        playerHPSlider.value = player.HP;
        enemyHPSlider.value = enemy.HP;
        timeText.text = "TIME: " + ((int)gameManager.ElapsedTime).ToString() + "/" + gameManager.GameEndTime.ToString();
    }
}
