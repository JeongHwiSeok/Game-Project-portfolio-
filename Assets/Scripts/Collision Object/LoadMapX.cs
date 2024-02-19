using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapX : MonoBehaviour
{
    [SerializeField] GameObject mapLoadZone;
    [SerializeField] MapManager mapManager;

    private void Awake()
    {
        mapLoadZone = transform.parent.gameObject;
        mapManager = mapLoadZone.transform.parent.GetComponent<MapManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerManager player = other.GetComponent<PlayerManager>();

        if(player != null)
        {
            if(mapLoadZone.transform.position.x > 27)
            {
                for (int i = 0; i < 3; i++)
                {
                    float newX = mapManager.mapList[i * 3].transform.localPosition.x - mapManager.offsetX;

                    mapManager.mapList[i * 3 + 2].transform.localPosition = new Vector3(newX, mapManager.mapList[i * 3].transform.localPosition.y, 0);
                }

                for (int i = 0; i < 3; i++)
                {
                    GameObject empty;

                    empty = mapManager.mapList[i * 3 + 2];

                    mapManager.mapList[i * 3 + 2] = mapManager.mapList[i * 3 + 1];

                    mapManager.mapList[i * 3 + 1] = mapManager.mapList[i * 3];

                    mapManager.mapList[i * 3] = empty;
                }
            }
            else if (mapLoadZone.transform.position.x < -27)
            {
                for (int i = 0; i < 3; i++)
                {
                    float newX = mapManager.mapList[i * 3 + 2].transform.localPosition.x + mapManager.offsetX;

                    mapManager.mapList[i * 3].transform.localPosition = new Vector3(newX, mapManager.mapList[i * 3 + 2].transform.localPosition.y, 0);
                }

                for (int i = 0; i < 3; i++)
                {
                    GameObject empty;

                    empty = mapManager.mapList[i * 3];

                    mapManager.mapList[i * 3] = mapManager.mapList[i * 3 + 1];

                    mapManager.mapList[i * 3 + 1] = mapManager.mapList[i * 3 + 2];

                    mapManager.mapList[i * 3 + 2] = empty;
                }
            }

            mapLoadZone.transform.localPosition = new Vector3(mapManager.mapList[4].transform.localPosition.x, mapManager.mapList[4].transform.localPosition.y, 0);
        }
    }
}
