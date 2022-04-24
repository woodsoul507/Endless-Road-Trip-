using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsSpawner : MonoBehaviour
{
  [SerializeField] List<GameObject> elementList;
  [SerializeField] List<float> lanes;
  [SerializeField] float spawnRateTime = 1.25f;
  [SerializeField] float yPos = 0f;
  [SerializeField] float spawnDistance = 300f;

  Queue<GameObject> _elements = new Queue<GameObject>();
  float currTime = float.MaxValue;
  Vector3 _playerPosition;
  float _latestLane;

  void OnEnable()
  {
    foreach (GameObject element in elementList)
    {
      _elements.Enqueue(element);
    }
  }

  void Update()
  {
    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    currTime += Time.deltaTime;

    // Adding some random order on the enemies spawn
    if (Random.Range(0, 1) == 0)
    {
      GameObject tempEnemy = _elements.Dequeue();
      _elements.Enqueue(tempEnemy);
    }

    if (!_elements.Peek().activeSelf && currTime >= spawnRateTime)
    {
      RelocateEnemies();
      currTime = 0f;
    }
  }

  void RelocateEnemies()
  {
    GameObject currEnemy = _elements.Dequeue();
    Vector3 newPosition = positionGenerator();

    currEnemy.transform.rotation = Quaternion.Euler(0, 180, 0);
    currEnemy.transform.position = newPosition;
    currEnemy.SetActive(true);
    if (currEnemy.gameObject.tag == "Enemy")
    {
      currEnemy.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * -2000f, ForceMode.Impulse);
    }

    _elements.Enqueue(currEnemy);
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
              yPos,
              _playerPosition.z + spawnDistance
            );
  }
}
