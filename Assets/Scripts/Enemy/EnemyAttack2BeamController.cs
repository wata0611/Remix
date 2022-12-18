using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack2BeamController : EnemyAttack2
{
    [SerializeField] float vanishBeamTime = 5f;

    float elapsedTimeFromLaunch;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTimeFromLaunch = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<EnemyAttack2>().LaunchFlg)
        {
            elapsedTimeFromLaunch += Time.deltaTime;
        }

        if (elapsedTimeFromLaunch >= vanishBeamTime)
        {
            Debug.Log("VANISH");
            VanishBeam();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            BeamDamage(player);
        }
    }

    void VanishBeam()
    {
        gameObject.SetActive(false);
    }

    void BeamDamage(PlayerController player)
    {
        player.Damage(damage);
    }

}
