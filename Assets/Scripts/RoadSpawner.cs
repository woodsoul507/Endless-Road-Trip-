using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
  [SerializeField] List<GameObject> roads;

  float offSet = 50f;

  void Start()
  {
    if (roads != null && roads.Count > 0)
    {
      roads = roads.OrderBy(road => road.transform.position.z).ToList();
    }
  }

  // ABSTRACTION
  public void MoveRoad()
  {
    GameObject movedRoad = roads[0];
    roads.Remove(movedRoad);
    float newZ = roads[roads.Count - 1].transform.position.z + offSet;
    movedRoad.transform.position = new Vector3(0, 0, newZ);
    movedRoad.gameObject.GetComponentInChildren<Road>().WasTriggered = false;
    roads.Add(movedRoad);
  }
}
