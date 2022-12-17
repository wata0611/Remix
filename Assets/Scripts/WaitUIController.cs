using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitUIController : MonoBehaviour
{
    [SerializeField] Text counterText;
    [SerializeField] float durationTime = 2f / 3;

    float textChangeTimer;
    int textCounter;


    // Start is called before the first frame update
    void Start()
    {
        InitTextTimer();
    }

    // Update is called once per frame
    void Update()
    {
        TextChangeManager();
    }

    

    void TextChanger()
    {
        if(textChangeTimer >= durationTime && textCounter == 3)
        {
            textCounter = 2;
        }
        else if(textChangeTimer >= durationTime*2 && textCounter == 2)
        {
            textCounter = 1;
        }
        counterText.text = textCounter.ToString();
    }

    public void InitTextTimer()
    {
        textChangeTimer = 0f;
        textCounter = 3;
    }

    void TextChangeManager()
    {
        textChangeTimer += Time.deltaTime;
        TextChanger();
    }
}
