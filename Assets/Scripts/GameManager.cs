using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform positionSpawn;
    public int nbEnnemyAtStart = 5;
    public int spawningSize = 10;
    public GameObject ennemyPrefab;
    public float spawnPeriod = 0.1f;
    public Text scoreText;
    private int score = 0;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbEnnemyAtStart; i++){
            // Instantiate(ennemyPrefab, positionSpawn.position + Random.onUnitSphere * spawningSize, Quaternion.identity, positionSpawn);
            float directionFacing = Random.Range (0f, 360f);
            float xPos = Random.Range(-10f, 10f);
            float zPos = Random.Range(-10f, 10f);
            Instantiate(ennemyPrefab, new Vector3(positionSpawn.position.x + xPos, 22, positionSpawn.position.z + zPos),  
            Quaternion.Euler(new Vector3 (0f, directionFacing, 0f)), positionSpawn);
        }
        StartCoroutine(SpawningLoop());
    }

    IEnumerator SpawningLoop() {
        // Instantiate(ennemyPrefab, positionSpawn.position + Random.onUnitSphere * spawningSize, Quaternion.identity, positionSpawn);
        float directionFacing = Random.Range (0f, 360f);
        float xPos = Random.Range(-20f, 20f);
        float zPos = Random.Range(-20f, 20f);
        Instantiate(ennemyPrefab, new Vector3(xPos, 22, zPos), 
            Quaternion.Euler(new Vector3 (0f, directionFacing, 0f)), positionSpawn);
        yield return new WaitForSeconds(spawnPeriod);
    }

    public void UpScore()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }

}
