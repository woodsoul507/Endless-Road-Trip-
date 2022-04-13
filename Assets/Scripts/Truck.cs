using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Enemy
{
  [SerializeField] float expectedVelocity = -2f;

  Rigidbody _rigidbody;

  void OnEnable()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    Movement();
  }

  protected override void Movement()
  {
    _rigidbody.AddForce(Vector3.forward * expectedVelocity, ForceMode.Impulse);
  }
}
