using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
  [SerializeField] protected float rotateRate = 75f;
  [SerializeField] protected float bounceForce = 2f;
  [SerializeField] protected float maxHeight = 0.5f;
  [SerializeField] protected float onCollectRotateRate = 1500f;
  [SerializeField] protected float onCollectBounceForce = 15;
  [SerializeField] protected float offScreenDisable = 60f;

  bool wasCollected;

  new Rigidbody rigidbody;
  GameObject _player;

  void Awake()
  {
    _player = GameObject.FindGameObjectWithTag("Player");
    rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    if (_player.transform.position.z > gameObject.transform.position.z + offScreenDisable)
    {
      Disable();
    }

    gameObject.transform.Rotate(Vector3.up * Time.deltaTime * (wasCollected ? onCollectRotateRate : rotateRate), Space.Self);

    if (transform.position.y <= 0)
    {
      rigidbody.AddForce(Vector3.up * (wasCollected ? onCollectBounceForce : bounceForce), ForceMode.Impulse);
    }

    if (transform.position.y >= maxHeight && !wasCollected)
    {
      rigidbody.velocity = Vector3.zero;
    }
  }

  protected void OnTriggerEnter(Collider other)
  {
    if (!wasCollected)
    {
      wasCollected = true;
      OnCollected();
      Effect(other);
    }
  }

  protected void OnCollected()
  {
    StartCoroutine(CollectingBounceWaitTime());
  }

  IEnumerator CollectingBounceWaitTime()
  {
    yield return new WaitForSeconds(1);
    wasCollected = false;
    Disable();
  }

  protected void Disable()
  {
    gameObject.SetActive(false);
  }

  protected abstract void Effect(Collider other);
}
