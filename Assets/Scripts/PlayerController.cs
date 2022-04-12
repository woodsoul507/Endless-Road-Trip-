using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] float sideSpeed = 8f;
  [SerializeField] float forwardSpeed = 40f;
  [SerializeField] SpawnManager spawnManager;

  bool moveLeft;
  bool moveRight;

  void Update()
  {
    transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;

    if (moveLeft)
    {
      transform.position += Vector3.left * sideSpeed * Time.deltaTime;
    }

    if (moveRight)
    {
      transform.position += Vector3.right * sideSpeed * Time.deltaTime;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "SpawnTrigger")
    {
      spawnManager.SpawnTriggerExit();
    }
  }

  public void MoveLeftOn()
  {
    moveLeft = true;
  }

  public void MoveLeftOff()
  {
    moveLeft = false;
  }

  public void MoveRightOn()
  {
    moveRight = true;
  }

  public void MoveRightOff()
  {
    moveRight = false;
  }
}
