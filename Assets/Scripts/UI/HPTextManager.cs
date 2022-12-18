using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        playerHPText.text = "PLAYER�F" + player.HP.ToString();
        playerHPSlider.maxValue = player.HP;
        playerHPSlider.value = player.HP;
        enemyHPText.text = "ENEMY�F" + enemy.HP.ToString();
        enemyHPSlider.maxValue = enemy.HP;
        enemyHPSlider.value = enemy.HP;
    }

    // Update is called once per frame
    void Update()
    {
        SetHPUI();
    }

    void SetHPUI()
    {
        playerHPText.text = "PLAYER�F" + player.HP.ToString();
        enemyHPText.text = "ENEMY�F" + enemy.HP.ToString();
        playerHPSlider.value = player.HP;
        enemyHPSlider.value = enemy.HP;
    }
}
