using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestory : MonoBehaviour
{
    [SerializeField] float explosionDestroyTime = 1f;
    float timer;
    bool doneDestroy;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= explosionDestroyTime && !doneDestroy)
        {
            Destroy(gameObject);
            doneDestroy = true;
        }
        timer += Time.deltaTime;
    }
}
