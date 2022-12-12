using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float ElapsedTime { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        ElapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
    }
}
