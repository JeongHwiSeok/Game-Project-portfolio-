using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mike : Weapon
{
    [SerializeField] public int itemLV;

    [SerializeField] GameObject soundWave;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float duration;
    [SerializeField] float range;
    [SerializeField] float slow;

    [SerializeField] Vector3 target;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbySoundWave;

    private void Start()
    {
        atk = 10;
        normalspeed = 10;
        knockBack = 1f;
        atkBuff = 1;
        speedBuff = 1;
        duration = 3;
        range = 1f;
        slow = 1;

        parent = GameObject.Find("Attack Manager").transform;

        GameObject bullet = Instantiate(soundWave, parent);

        bullet.GetComponent<SoundWave>().StatInput(atk, normalspeed, knockBack);

        bullet.SetActive(false);

        standbySoundWave.Add(bullet);

        GameManager.instance.weaponItemList.Add(bullet);

        StartCoroutine(Fire());

    }
    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                break;
            case 2:
                atkBuff = 1.2f;
                break;
            case 3:
                range = 1.2f;
                break;
            case 4:
                knockBack = 1.2f;
                break;
            case 5:
                duration = 2;
                break;
            case 6:
                atkBuff = 1.4f;
                break;
            case 7:
                range = 1.5f;
                slow = 0.5f;
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (standbySoundWave[0].activeSelf != true)
                {
                    standbySoundWave[0].transform.position = new Vector3(0, 0, 0);
                    standbySoundWave[0].GetComponent<SoundWave>().StatInput(atk * atkBuff, normalspeed * speedBuff * BuffDebuffManager.instance.pwsSpeedBuff, knockBack);
                    standbySoundWave[0].GetComponent<SoundWave>().Slow = slow;
                    standbySoundWave[0].transform.localScale = new Vector3(5.6f * range, 5.6f * range, 5.6f * range);

                    if (PlayerManager.instance.spriteRenderer.flipX)
                    {
                        standbySoundWave[0].transform.position = new Vector3(-1.5f, 0, 0);
                        standbySoundWave[0].transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        standbySoundWave[0].transform.position = new Vector3(1.5f, 0, 0);
                        standbySoundWave[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    standbySoundWave[0].SetActive(true);
                }
                yield return new WaitForSeconds(duration);
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return null;
        }
    }
}