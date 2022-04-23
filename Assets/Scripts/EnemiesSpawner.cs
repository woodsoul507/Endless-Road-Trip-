using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
  [SerializeField] List<GameObject> enemiesList;
  [SerializeField] List<float> lanes;
  [SerializeField] float spawnRateTime = 1.25f;

  Queue<GameObject> _enemies = new Queue<GameObject>();
  float currTime = float.MaxValue;
  Vector3 _playerPosition;
  float _latestLane;

  void OnEnable()
  {
    foreach (GameObject enemy in enemiesList)
    {
      _enemies.Enqueue(enemy);
    }
  }

  void Update()
  {
    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    currTime += Time.deltaTime;

    // Adding some random order on the enemies spawn
    if (Random.Range(0, 1) == 0)
    {
      GameObject tempEnemy = _enemies.Dequeue();
      _enemies.Enqueue(tempEnemy);
    }

    if (!_enemies.Peek().activeSelf && currTime >= spawnRateTime)
    {
      RelocateEnemies();
      currTime = 0f;
    }
  }

  void RelocateEnemies()
  {
    GameObject currEnemy = _enemies.Dequeue();
    Vector3 newPosition = positionGenerator();

    currEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
    currEnemy.transform.position = newPosition;
    currEnemy.SetActive(true);

    _enemies.Enqueue(currEnemy);
  }

  Vector3 positionGenerator()
  {
    float tempLane = lanes[Random.Range(0, 4)];
    while (tempLane == _latestLane)
    {
      tempLane = lanes[Random.Range(0, 4)];
    }
    _latestLane = tempLane;
    return new Vector3(
              tempLane,
              0.4f,
              _playerPosition.z + 300
            );
  }
}
