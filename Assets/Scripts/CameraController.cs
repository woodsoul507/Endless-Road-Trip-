using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  Transform player;
  float yOffset = 2.5f;
  float zOffset = -3.5f;

  void Start()
  {
    player = GameObject.Find("Player").transform;
  }

  void LateUpdate()
  {
    transform.position = new Vector3(
        player.position.x,
        player.position.y + yOffset,
        player.position.z + zOffset
    );
  }
}
