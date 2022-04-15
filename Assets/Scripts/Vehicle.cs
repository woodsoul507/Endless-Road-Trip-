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
  [SerializeField] bool fixWheelsRotation = false;
  [SerializeField] protected float frontRightRotationFix = 0f;
  [SerializeField] protected float frontLeftRotationFix = 0f;
  [SerializeField] protected float backRightRotationFix = 0f;
  [SerializeField] protected float backLeftRotationFix = 0f;
  [SerializeField] float acceleration = 500f;
  [SerializeField] float initialAcceleration = 3000f;
  [SerializeField] float maxSpeed = 120f;
  [SerializeField] float keySpeed = 80f;
  //[SerializeField] float breaking = 300f;

  protected float currSpeed;

  float _currentBrk = 0f;
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
      frontRightWheel.motorTorque = currSpeed < keySpeed ? initialAcceleration : acceleration;
      frontLeftWheel.motorTorque = currSpeed < keySpeed ? initialAcceleration : acceleration;
    }
    else
    {
      frontRightWheel.motorTorque = 0f;
      frontLeftWheel.motorTorque = 0f;
    }

    frontRightWheel.brakeTorque = _currentBrk;
    frontLeftWheel.brakeTorque = _currentBrk;
    backRightWheel.brakeTorque = _currentBrk;
    backLeftWheel.brakeTorque = _currentBrk;

    UpdateWheel(frontRightWheel, frontRightTransform, frontRightRotationFix);
    UpdateWheel(frontLeftWheel, frontLeftTransform, frontLeftRotationFix);
    UpdateWheel(backRightWheel, backRightTransform, backRightRotationFix);
    UpdateWheel(backLeftWheel, backLeftTransform, backLeftRotationFix);
  }

  protected virtual void Disable()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player.transform.position.z > gameObject.transform.position.z + 10f)
    {
      gameObject.SetActive(false);
    }
  }

  protected virtual void UpdateWheel(WheelCollider col, Transform trans, float fixRotation)
  {
    Vector3 position;
    Quaternion rotation;
    col.GetWorldPose(out position, out rotation);
    if (fixWheelsRotation)
    {
      rotation = rotation * Quaternion.Euler(new Vector3(0, fixRotation, 0));
    }
    trans.position = position;
    trans.rotation = rotation;
  }

  protected abstract void Turn();
}
