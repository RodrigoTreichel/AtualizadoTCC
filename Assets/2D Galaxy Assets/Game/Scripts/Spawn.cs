using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject inimigoShipPrefab;
    [SerializeField]
    private GameObject[] powerups; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InimigoSpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator InimigoSpawnRoutine()
    {
        while(true)
        {
            Instantiate(inimigoShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(true)
        {
            int ramdomPowerups = Random.Range(0, 3);
            Instantiate(powerups[ramdomPowerups], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
