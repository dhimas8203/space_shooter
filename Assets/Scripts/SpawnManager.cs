using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnEnemyRoutine()); 
       StartCoroutine(SpawnPowerupRoutine()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }

    IEnumerator SpawnEnemyRoutine() {
         {
            while(_stopSpawning == false) {
                GameObject _enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 7.5f, 0), Quaternion.identity);
                _enemy.transform.parent = _enemyContainer.transform; 

                yield return new WaitForSeconds(5.0f);
            }
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        while(_stopSpawning == false) {
            float randSpawnTime = Random.Range(3.0f, 8.0f);
            yield return new WaitForSeconds(randSpawnTime);
            Vector3 postToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7.5f, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject _powerup = Instantiate(powerups[randomPowerup], postToSpawn, Quaternion.identity);
            _powerup.transform.parent = _enemyContainer.transform; 
        }
    }
}
