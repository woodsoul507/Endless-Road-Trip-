using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public SpawnManager spawnManager;
  void Start()
  {

  }

  void Update()
  {

  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "SpawnTrigger")
    {
      spawnManager.SpawnTriggerExit();
    }
  }
}
