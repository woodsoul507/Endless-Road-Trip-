using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : PowerUp
{
  [SerializeField] int healAmount = 7;

  new void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      base.OnTriggerEnter(other);
    }
  }

  // POLYMORPHISM
  protected override void Effect(Collider other)
  {
    other.gameObject.GetComponent<PlayerController>().GettingHeal(healAmount);
  }
}
