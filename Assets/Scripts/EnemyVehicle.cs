using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVehicle : Vehicle
{
  [SerializeField] public FrontCollider _frontCollider;

  public bool IsDamageAble { get { return _isDamageAble; } set { _isDamageAble = value; } }

  bool _isDamageAble = true;

  new void Update()
  {
    base.Update();

    if (_frontCollider.isCollider)
    {
      base.IsBreaking = true;
    }
    else
    {
      base.IsBreaking = false;
    }
  }

  protected override void Turn()
  {
    // Not needed yet
  }
}
