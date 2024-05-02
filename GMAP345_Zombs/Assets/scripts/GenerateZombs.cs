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
        while (enemyCount < 20)
        {
            xPos = Random.Range(1, -115);
            zPos = Random.Range(-35, 80);
            Instantiate(theEnemy, new Vector3(xPos, 2, zPos), Quaternion.identity);
            yield return new WaitForSeconds(10f);
            enemyCount += 1;
        }
    }
    


}
