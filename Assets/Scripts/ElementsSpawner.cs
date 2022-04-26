using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// INHERITANCE
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
    var rnd = new System.Random();
    var tempElementList = elementList.OrderBy(item => rnd.Next());
    foreach (GameObject element in tempElementList)
    {
      _elements.Enqueue(element);
    }
  }

  void Update()
  {
    _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    currTime += Time.deltaTime;

    // Adding some randomize on the enemies spawn
    if (UnityEngine.Random.Range(0, 1) == 0)
    {
      _elements.Enqueue(_elements.Dequeue());
    }

    if (!_elements.Peek().activeSelf && currTime >= spawnRateTime)
    {
      RelocateElement();
      currTime = 0f;
    }
    else
    {
      _elements.Enqueue(_elements.Dequeue());
    }
  }

  // ABSTRACTION
  void RelocateElement()
  {
    GameObject currElement = _elements.Dequeue();
    Vector3 newPosition = positionGenerator();

    currElement.transform.rotation = Quaternion.Euler(0, 180, 0);
    currElement.transform.position = newPosition;
    currElement.SetActive(true);
    if (currElement.gameObject.tag == "Enemy")
    {
      currElement.gameObject.GetComponent<EnemyVehicle>()._frontCollider.isCollider = false;
      currElement.gameObject.GetComponent<EnemyVehicle>().IsBreaking = false;
      currElement.gameObject.GetComponent<EnemyVehicle>().IsDamageAble = true;
      currElement.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * -2000f, ForceMode.Impulse);
    }

    _elements.Enqueue(currElement);
  }

  // ABSTRACTION
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
