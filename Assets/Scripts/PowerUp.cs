using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
  [SerializeField] protected float rotateRate = 75f;
  [SerializeField] protected float bounceForce = 2f;
  [SerializeField] protected float maxHeight = 0.5f;
  [SerializeField] protected float OnCollectRotateRate = 1500f;
  [SerializeField] protected float OnCollectBounceForce = 15;

  bool wasCollected;

  new Rigidbody rigidbody;

  void Awake()
  {
    rigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    gameObject.transform.Rotate(Vector3.up * Time.deltaTime * (wasCollected ? OnCollectRotateRate : rotateRate), Space.Self);

    if (transform.position.y <= 0)
    {
      rigidbody.AddForce(Vector3.up * (wasCollected ? OnCollectBounceForce : bounceForce), ForceMode.Impulse);
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
      GetCollected();
      Effect(other);
    }
  }

  protected void GetCollected()
  {
    StartCoroutine(CollectingBounceWaitTime());
  }

  IEnumerator CollectingBounceWaitTime()
  {
    yield return new WaitForSeconds(1);
    wasCollected = false;
    gameObject.SetActive(false);
  }

  protected abstract void Effect(Collider other);
}
