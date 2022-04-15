using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Vehicle
{
  [SerializeField] float leftConstraint = -10.6f;
  [SerializeField] float rightConstraint = -1.9f;
  [SerializeField] float maxTurnAngle = 15f;
  [SerializeField] float turnRate = 0.02f;
  [SerializeField] SpawnManager spawnManager;

  bool moveLeft;
  bool moveRight;
  float currentTurn = 0f;

  new void Start()
  {
    base.Start();
  }

  new void Update()
  {
    base.Update();

    Turn();

    if (transform.position.x > rightConstraint)
    {
      transform.position = new Vector3(
        rightConstraint,
        transform.position.y,
        transform.position.z
      );
    }

    if (transform.position.x < leftConstraint)
    {
      transform.position = new Vector3(
        leftConstraint,
        transform.position.y,
        transform.position.z
      );
    }

    // Debug.Log("SPEED --- " + currSpeed);
  }

  protected override void Turn()
  {
    if (moveLeft && Math.Abs(currentTurn) < maxTurnAngle)
    {
      currentTurn -= turnRate;
    }

    if (moveRight && Math.Abs(currentTurn) < maxTurnAngle)
    {
      currentTurn += turnRate;
    }

    frontRightWheel.steerAngle = currentTurn;
    frontLeftWheel.steerAngle = currentTurn;

    Debug.Log("TURN --- " + currentTurn);
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
    currentTurn = 0f;
  }

  public void MoveRightOn()
  {
    moveRight = true;
  }

  public void MoveRightOff()
  {
    moveRight = false;
    currentTurn = 0f;
  }
}
