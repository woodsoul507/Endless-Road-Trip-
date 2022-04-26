using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField] int roadsBeforeSpawn = 4;

  // ENCAPSULATION
  public int RoadsCounter { get { return _roadsCounter; } set { _roadsCounter = value; } }

  RoadSpawner _roadSpawner;
  int _roadsCounter = 0;

  void Start()
  {
    _roadSpawner = GetComponent<RoadSpawner>();
  }

  public void SpawnTriggerExit()
  {
    _roadsCounter++;

    if (_roadsCounter >= roadsBeforeSpawn)
    {
      _roadSpawner.MoveRoad();
      _roadsCounter = roadsBeforeSpawn - 1;
    }
  }
}
