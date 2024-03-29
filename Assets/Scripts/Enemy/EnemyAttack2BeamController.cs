using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack2BeamController : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float vanishBeamTime = 5f;

    float elapsedTimeFromLaunch;

    // Start is called before the first frame update
    void Awake()
    {
        elapsedTimeFromLaunch = 0f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<EnemyAttackController3>().GetLaunchedFlg())
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
