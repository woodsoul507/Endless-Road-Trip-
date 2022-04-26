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
  [SerializeField] protected float frontRightRotationFix = 0f;
  [SerializeField] protected float frontLeftRotationFix = 0f;
  [SerializeField] protected float backRightRotationFix = 0f;
  [SerializeField] protected float backLeftRotationFix = 0f;
  [SerializeField] protected float _freezeTimeAllowed = 3f;
  [SerializeField] protected float minVelocityAllowed = 3f;
  [SerializeField] protected int _damage = 7;
  [SerializeField] bool fixWheelsRotation = false;
  [SerializeField] float acceleration = 500f;
  [SerializeField] float initialAcceleration = 3000f;
  [SerializeField] float maxSpeed = 120f;
  [SerializeField] float keySpeed = 70f;
  [SerializeField] float breakingForce = 300f;
  [SerializeField] float offScreenDisable = 15f;

  public bool IsBreaking { get; set; }
  public int Damage { get { return _damage; } }
  public float FreezeTime { get { return _freezeTime; } set { _freezeTime = value; } }
  public float FreezeTimeAllowed { get { return _freezeTimeAllowed; } }

  protected float _currSpeed;
  protected float _freezeTime = 0;

  protected Rigidbody _rigidbody;
  GameObject _player;

  protected void Start()
  {
    _player = GameObject.FindGameObjectWithTag("Player");
    _rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  protected void Update()
  {
    _currSpeed = _rigidbody.velocity.magnitude * 3.6f;

    if (_rigidbody.velocity.sqrMagnitude <= minVelocityAllowed)
    {
      _freezeTime += Time.deltaTime;
    }
    else
    {
      _freezeTime = 0f;
    }

    Disable();
  }

  protected void FixedUpdate()
  {
    if (_currSpeed < maxSpeed)
    {
      frontRightWheel.motorTorque = _currSpeed < keySpeed ? initialAcceleration : acceleration;
      frontLeftWheel.motorTorque = _currSpeed < keySpeed ? initialAcceleration : acceleration;
    }
    else
    {
      frontRightWheel.motorTorque = 0f;
      frontLeftWheel.motorTorque = 0f;
    }

    if (IsBreaking)
    {
      frontRightWheel.brakeTorque = breakingForce;
      frontLeftWheel.brakeTorque = breakingForce;
      backRightWheel.brakeTorque = breakingForce;
      backLeftWheel.brakeTorque = breakingForce;
    }
    else
    {
      frontRightWheel.brakeTorque = 0f;
      frontLeftWheel.brakeTorque = 0f;
      backRightWheel.brakeTorque = 0f;
      backLeftWheel.brakeTorque = 0f;
    }


    UpdateWheel(frontRightWheel, frontRightTransform, frontRightRotationFix);
    UpdateWheel(frontLeftWheel, frontLeftTransform, frontLeftRotationFix);
    UpdateWheel(backRightWheel, backRightTransform, backRightRotationFix);
    UpdateWheel(backLeftWheel, backLeftTransform, backLeftRotationFix);
  }

  protected virtual void Disable()
  {
    if (_player.transform.position.z > gameObject.transform.position.z + offScreenDisable)
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
