using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCollider : MonoBehaviour
{
  public bool isCollider { get; set; }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      isCollider = true;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      isCollider = false;
    }
  }
}
