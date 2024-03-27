using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeakerAttack : MonoBehaviour
{
    [SerializeField] BossSpeaker bossSpeaker;

    public void GameObjectOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            collision.GetComponent<PlayerManager>().Damage(bossSpeaker.ATK);
        }
    }
}
