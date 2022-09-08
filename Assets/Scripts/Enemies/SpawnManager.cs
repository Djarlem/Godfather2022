using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<SimpleEnemy> enemiesPrefabs = new List<SimpleEnemy>();
    [SerializeField] float[] _spawnRate;
    [SerializeField] float[] _spawnDist;
    [SerializeField] Vector2[] _stack;
    [SerializeField] Vector2[] _speed;
    [SerializeField] float[] _waitBetweenSpawn;
    [SerializeField] float time;
    [SerializeField] float difficultyRatio;
    [SerializeField] float difficulty;
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

    private void Update() {
        time += Time.deltaTime;
        difficulty = time / difficultyRatio;
    }
    IEnumerator SpawnEnemyLine(int prefab) {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector3 position = new Vector3(x, y, 0f).normalized * _spawnDist[prefab];
        int stack = (int)Mathf.Lerp(_stack[prefab][0], _stack[prefab][1], difficulty);
        if (prefab == 0) {
            for (int i = 0; i < stack; i++) {
                Vector3 right = Quaternion.Euler(0, 0, 90) * (transform.position - position).normalized;
                SimpleEnemy newEnemy = Instantiate(enemiesPrefabs[prefab], transform.position + position + 0.7f * i * right - 0.7f * stack / 2 * right, Quaternion.Euler(0, 0, 0));
                newEnemy.speed = Mathf.Lerp(_speed[prefab][0], _speed[prefab][1], difficulty);
            }
            yield return null;
        } else {
            for (int i = 0; i < stack; i++) {
                SimpleEnemy newEnemy = Instantiate(enemiesPrefabs[prefab], transform.position + position, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(_waitBetweenSpawn[prefab]);
                newEnemy.speedToPlayer = Mathf.Lerp(_speed[prefab][0], _speed[prefab][1], difficulty);
            }
        }
    }
}
