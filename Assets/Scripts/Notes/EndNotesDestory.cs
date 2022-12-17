using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNotesDestory : MonoBehaviour
{
    const string NOTES_TAG = "Notes";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == NOTES_TAG)
        {
            Destroy(collision.gameObject);
        }
    }

}
