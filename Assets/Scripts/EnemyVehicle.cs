using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVehicle : Vehicle
{
  [SerializeField] FrontCollider _frontCollider;

  new void Update()
  {
    base.Update();

    if (_frontCollider.isCollider)
    {
      base.isBreaking = true;
    }
    else
    {
      base.isBreaking = false;
    }
  }

  protected override void Turn()
  {
    // Not needed yet
  }
}
