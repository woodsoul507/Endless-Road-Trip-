using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
  void Update()
  {
    Disable();
  }
  protected abstract void Movement();

  protected virtual void Disable()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player.transform.position.z > gameObject.transform.position.z + 10f)
    {
      gameObject.SetActive(false);
    }
  }
}
