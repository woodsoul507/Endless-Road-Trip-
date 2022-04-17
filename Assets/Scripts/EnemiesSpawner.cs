using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
  [SerializeField] List<GameObject> enemies;
  [SerializeField] List<float> lanes;

  Vector3 _playerPosition;
  int _activeEnemies;

  void Start()
  {

  }

  void Update()
  {
    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
  }

  int ActiveEnemies()
  {
    int result = 0;
    foreach (GameObject enemy in enemies)
    {
      if (enemy.activeSelf)
      {
        result++;
      }
    }
    return result;
  }
}
