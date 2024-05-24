using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateZombs : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < 25)
        {
            xPos = Random.Range(-195, -76);
            zPos = Random.Range(38, -78);
            Instantiate(theEnemy, new Vector3(xPos, 18, zPos), Quaternion.identity);
            yield return new WaitForSeconds(10f);
            enemyCount += 1;
        }
    }
    


}
