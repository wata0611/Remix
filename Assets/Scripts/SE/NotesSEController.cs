using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSEController : MonoBehaviour
{
    [SerializeField] float deadTime;
    [SerializeField] float playStartTime;
    [SerializeField] AudioSource audioSource;
    float timer;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource.time = playStartTime;
        audioSource.Play();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= deadTime)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
