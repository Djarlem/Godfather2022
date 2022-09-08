using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<SimpleEnemy> enemiesPrefabs = new List<SimpleEnemy>();
    [SerializeField] float[] _spawnRate;
    [SerializeField] float[] _spawnDist;
    [SerializeField] float[] _stack;
    [SerializeField] float[] _waitBetweenSpawn;
    Timer _spawnTimerLine;
    Timer _spawnTimerSpirale;
    private void Start() {
        _spawnTimerLine = new Timer(this, _spawnRate[0]);
        _spawnTimerLine.OnActivate += () => StartCoroutine(SpawnEnemyLine(0));
        _spawnTimerLine.Start();
        _spawnTimerSpirale = new Timer(this, _spawnRate[1]);
        _spawnTimerSpirale.OnActivate += () => StartCoroutine(SpawnEnemyLine(1));
        _spawnTimerSpirale.Start();
    }

    IEnumerator SpawnEnemyLine(int prefab) {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector3 position = new Vector3(x, y, 0f).normalized * _spawnDist[prefab];
        if (prefab == 0) {
            for (int i = 0; i < _stack[prefab]; i++) {
                Vector3 right = Quaternion.Euler(0, 0, 90) * (transform.position - position).normalized;
                SimpleEnemy newEnemy = Instantiate(enemiesPrefabs[prefab], transform.position + position + 0.7f * i * right - 0.7f * _stack[prefab] / 2 * right, Quaternion.Euler(0, 0, 0));
            }
            yield return null;
        } else {
            for (int i = 0; i < _stack[prefab]; i++) {
                SimpleEnemy newEnemy = Instantiate(enemiesPrefabs[prefab], transform.position + position, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(_waitBetweenSpawn[prefab]);
            }
        }
    }


}
