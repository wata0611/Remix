using UnityEngine;
using UnityEngine.UI;

public class GameStartPanelContrller : MonoBehaviour
{
    [SerializeField] Image panel;
    [SerializeField] float fadeTime = 3f;
    public bool DoneFade { set; get; }
    public bool FadeOK { set; get; }
    public float fadeTimer;
    // Start is called before the first frame update
    void Start()
    {
        DoneFade = false;
        FadeOK = false;
        fadeTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        if (FadeOK)
        {
            fadeTimer += Time.deltaTime;
            float alpha = fadeTimer / fadeTime;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            if (alpha >= 1f)
            {
                DoneFade = true; ;
            }
        }
    }
}
