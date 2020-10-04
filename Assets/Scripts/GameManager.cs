using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform positionSpawn;
    public int nbEnnemyAtStart = 30;
    public int spawningSize = 10;
    public GameObject ennemyPrefab;
    public List<GameObject> listEnnemies;
    public float spawnPeriod = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbEnnemyAtStart; i++){
            GameObject e;
            // Instantiate(ennemyPrefab, positionSpawn.position + Random.onUnitSphere * spawningSize, Quaternion.identity, positionSpawn);
            float directionFacing = Random.Range (0f, 360f);
            float xPos = Random.Range(-10f, 10f);
            float zPos = Random.Range(-10f, 10f);
            e = Instantiate(ennemyPrefab, new Vector3(positionSpawn.position.x + xPos, 22, positionSpawn.position.z + zPos),  
                Quaternion.Euler(new Vector3 (0f, directionFacing, 0f)), positionSpawn);
            listEnnemies.Add(e);
        }
        StartCoroutine(SpawningLoop());
    }

    void Update()
    {
        foreach (GameObject e in listEnnemies)
        {
            if (e != null) {
                Ennemy ennemy = e.GetComponent<Ennemy>();
                //Debug.Log(ennemy.gameObject +" n°"+ listEnnemies.IndexOf(e) +" : "+ennemy.currentWayPoint);
                if(ennemy.hasReachedCheckpoint) {
                    switch (ennemy.currentWayPoint)
                    {
                        case 0 :
                            ennemy.currentWayPoint = Random.Range(1, 3);
                            break;
                        case 1 :
                            ennemy.currentWayPoint = 3; // ennemy goes from point 1 to last point
                            break;
                        case 2 :
                            ennemy.currentWayPoint = 3; // ennemy goes from point 2 to last point
                            break;
                        default:
                            ennemy.currentWayPoint = 4;
                            break;
                    }
                    if (ennemy.currentWayPoint != 4) {
                        ennemy.targetWayPoint = ennemy.keypointsPath[0].GetChild(ennemy.currentWayPoint);
                        ennemy.hasReachedCheckpoint = false;
                    }
                }
            }
            
        }
    }

    IEnumerator SpawningLoop() {
        GameObject e;
        float directionFacing = Random.Range (0f, 360f);
        float xPos = Random.Range(-10f, 10f);
        float zPos = Random.Range(-10f, 10f);
        e = Instantiate(ennemyPrefab, new Vector3(positionSpawn.position.x + xPos, 22, positionSpawn.position.z + zPos),  
            Quaternion.Euler(new Vector3 (0f, directionFacing, 0f)), positionSpawn);
        listEnnemies.Add(e);
        yield return new WaitForSeconds(spawnPeriod);
    }
}
