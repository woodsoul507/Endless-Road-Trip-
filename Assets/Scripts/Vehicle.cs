using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
  [SerializeField] protected WheelCollider frontRightWheel;
  [SerializeField] protected WheelCollider frontLeftWheel;
  [SerializeField] protected WheelCollider backRightWheel;
  [SerializeField] protected WheelCollider backLeftWheel;
  [SerializeField] protected Transform frontRightTransform;
  [SerializeField] protected Transform frontLeftTransform;
  [SerializeField] protected Transform backRightTransform;
  [SerializeField] protected Transform backLeftTransform;
  [SerializeField] float acceleration = 500f;
  [SerializeField] float maxSpeed = 200f;
  //[SerializeField] float breaking = 300f;

  protected float currSpeed;

  float currentBrk = 0f;
  Rigidbody _rigidbody;

  protected void Start()
  {
    _rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  protected void Update()
  {
    currSpeed = _rigidbody.velocity.magnitude * 3.6f;
    Disable();
  }

  protected void FixedUpdate()
  {
    if (currSpeed < maxSpeed)
    {
      frontRightWheel.motorTorque = acceleration;
      frontLeftWheel.motorTorque = acceleration;
    }
    else
    {
      frontRightWheel.motorTorque = 0f;
      frontLeftWheel.motorTorque = 0f;
    }

    frontRightWheel.brakeTorque = currentBrk;
    frontLeftWheel.brakeTorque = currentBrk;
    backRightWheel.brakeTorque = currentBrk;
    backLeftWheel.brakeTorque = currentBrk;

    UpdateWheel(frontRightWheel, frontRightTransform);
    UpdateWheel(frontLeftWheel, frontLeftTransform);
    UpdateWheel(backRightWheel, backRightTransform);
    UpdateWheel(backLeftWheel, backLeftTransform);
  }

  protected virtual void Disable()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player.transform.position.z > gameObject.transform.position.z + 10f)
    {
      gameObject.SetActive(false);
    }
  }

  protected virtual void UpdateWheel(WheelCollider col, Transform trans)
  {
    Vector3 position;
    Quaternion rotation;
    col.GetWorldPose(out position, out rotation);
    trans.position = position;
    trans.rotation = rotation;
  }

  protected abstract void Turn();
}
