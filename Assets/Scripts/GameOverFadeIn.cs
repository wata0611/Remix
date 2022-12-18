using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverFadeIn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Text[] textList;
    [SerializeField] Image[] imgList;

    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数
    public bool DoneFadeIn { set; get; }
    float fadeStartTimer;

    // Start is called before the first frame update
    void Start()
    {
        fadeStartTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.PlayPhase == PLAY_PHASE.EMD_PHASE)
        {
            fadeStartTimer += Time.deltaTime;
            float alpha = fadeStartTimer / gameManager.GameEndFadeTime;
            for(int i = 0; i < textList.Length; i++)
            {
                textList[i].color = new Color(textList[i].color.r, textList[i].color.g, textList[i].color.b, alpha);
            }
            for(int i= 0;i<imgList.Length; i++)
            {
                imgList[i].color = new Color(imgList[i].color.r, imgList[i].color.g, imgList[i].color.b, alpha);
            }

        }
    }
}
