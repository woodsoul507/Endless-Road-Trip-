using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
  public bool WasTriggered { get { return _wasTriggered; } set { _wasTriggered = value; } }

  bool _wasTriggered = false;
}
