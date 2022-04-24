using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : Vehicle
{
  [SerializeField] float leftConstraint = -10.1f;
  [SerializeField] float rightConstraint = -2.4f;
  [SerializeField] float maxTurnAngle = 15f;
  [SerializeField] float turnRate = 0.02f;
  [SerializeField] int contactDamage = 7;
  [SerializeField] int powerUpsHeal = 7;
  [SerializeField] int scoreRate = 10;
  [SerializeField] SpawnManager spawnManager;
  [SerializeField] TextMeshProUGUI healthBarText;
  [SerializeField] TextMeshProUGUI scoreBarText;

  public int HealthBar { get; set; }

  int _score = 0;
  bool _moveLeft;
  bool _moveRight;
  float _currentTurn = 0f;

  new void Start()
  {
    base.Start();
    healthBarText.text = "Health\n" + HealthBar + "%";
  }

  new void Update()
  {
    base.Update();

    if (_score < transform.position.z)
    {
      _score = (int)transform.position.z / scoreRate;
    }

    scoreBarText.text = "Score\n" + _score;

    Turn();

    if (transform.position.x > rightConstraint)
    {
      transform.position = new Vector3(
        rightConstraint,
        transform.position.y,
        transform.position.z
      );
    }

    if (transform.position.x < leftConstraint)
    {
      transform.position = new Vector3(
        leftConstraint,
        transform.position.y,
        transform.position.z
      );
    }
  }

  protected override void Turn()
  {
    if (_moveLeft && Math.Abs(_currentTurn) < maxTurnAngle)
    {
      _currentTurn -= turnRate;
    }
    if (!_moveLeft && transform.localEulerAngles.y > 179f)
    {
      transform.Rotate(Vector3.up * Time.deltaTime * 5f);
    }

    if (_moveRight && Math.Abs(_currentTurn) < maxTurnAngle)
    {
      _currentTurn += turnRate;
    }
    if (!_moveRight && transform.localEulerAngles.y > 1f && transform.localEulerAngles.y < 180f)
    {
      transform.Rotate(Vector3.down * Time.deltaTime * 5f);
    }

    frontRightWheel.steerAngle = _currentTurn;
    frontLeftWheel.steerAngle = _currentTurn;
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "SpawnTrigger")
    {
      spawnManager.SpawnTriggerExit();
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      GettingDamage();
      healthBarText.text = "Health\n" + HealthBar + "%";
    }

    if (collision.gameObject.tag == "PowerUp")
    {
      GettingHeal();
      healthBarText.text = "Health\n" + HealthBar + "%";
    }
  }

  public void MoveLeftOn()
  {
    _moveLeft = true;
  }

  public void MoveLeftOff()
  {
    _moveLeft = false;
    _currentTurn = 0f;
  }

  public void MoveRightOn()
  {
    _moveRight = true;
  }

  public void MoveRightOff()
  {
    _moveRight = false;
    _currentTurn = 0f;
  }

  void GettingDamage()
  {
    HealthBar = HealthBar - contactDamage < 0 ? 0 : HealthBar - contactDamage;
  }

  void GettingHeal()
  {
    HealthBar = HealthBar == 100 ? 100 : HealthBar + powerUpsHeal;
  }
}
