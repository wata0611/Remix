using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    [SerializeField] float showTime = 2f;
    [SerializeField] float hidetime = 0.5f;
    [SerializeField] Text text;
    [SerializeField] GameStartPanelContrller startPanel;

    float timer;
    bool show = true;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SwitchShowFlg();
        ShowText();
        
    }

    void ShowText()
    {
        if (show)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        }
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
    }

    void SwitchShowFlg()
    {
        if(show && timer >= showTime * (startPanel.FadeOK ? 0.3f : 1f))
        {
            show = false;
            timer = 0f;
        }
        else if(!show && timer >= hidetime * (startPanel.FadeOK ? 0.3f : 1f))
        {
            show = true;
            timer = 0f;
        }
    }
}
