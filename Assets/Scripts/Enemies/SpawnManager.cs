using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<SimpleEnemy> enemiesPrefabs = new List<SimpleEnemy>();
    [SerializeField] float _spawnRate = 2f;
    [SerializeField] float _spawnDist = 2f;
    Timer _spawnTimer;
    private void Start() {
        _spawnTimer = new Timer(this, _spawnRate);
        _spawnTimer.OnActivate += SpawnEnemy;
        _spawnTimer.Start();
    }

    void SpawnEnemy() {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector3 position = new Vector3(x, y, 0f).normalized * _spawnDist;
        int enemyType = Random.Range(0, enemiesPrefabs.Count);
        SimpleEnemy newEnemy = Instantiate(enemiesPrefabs[enemyType], transform.position + position, Quaternion.Euler(0,0,0));
    }

}
