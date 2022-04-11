using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  RoadSpawner _roadSpawner;

  void Start()
  {
    _roadSpawner = GetComponent<RoadSpawner>();
  }

  public void SpawnTriggerExit()
  {
    _roadSpawner.MoveRoad();
  }
}
