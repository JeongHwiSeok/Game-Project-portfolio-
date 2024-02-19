using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapY : MonoBehaviour
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

        if (player != null)
        {
            if (mapLoadZone.transform.position.y < -15)
            {
                for (int i = 0; i < 3; i++)
                {
                    float newY = mapManager.mapList[i].transform.localPosition.y + mapManager.offsetY;

                    mapManager.mapList[i + 6].transform.localPosition = new Vector3(mapManager.mapList[i + 6].transform.localPosition.x, newY , 0);
                }

                for (int i = 0; i < 3; i++)
                {
                    GameObject empty;

                    empty = mapManager.mapList[i + 6];

                    mapManager.mapList[i + 6] = mapManager.mapList[i + 3];

                    mapManager.mapList[i + 3] = mapManager.mapList[i];

                    mapManager.mapList[i] = empty;
                }
            }
            else if (mapLoadZone.transform.position.y > 15)
            {
                for (int i = 0; i < 3; i++)
                {
                    float newY = mapManager.mapList[i + 6].transform.localPosition.y - mapManager.offsetY;

                    mapManager.mapList[i].transform.localPosition = new Vector3(mapManager.mapList[i + 6].transform.localPosition.x, newY, 0);
                }

                for (int i = 0; i < 3; i++)
                {
                    GameObject empty;

                    empty = mapManager.mapList[i];

                    mapManager.mapList[i] = mapManager.mapList[i + 3];

                    mapManager.mapList[i + 3] = mapManager.mapList[i + 6];

                    mapManager.mapList[i + 6] = empty;
                }
            }

            mapLoadZone.transform.localPosition = new Vector3(mapManager.mapList[4].transform.localPosition.x, mapManager.mapList[4].transform.localPosition.y, 0);
        }
    }
}
