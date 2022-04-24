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
  [SerializeField] float turnFixing = 5f;
  [SerializeField] int scoreRate = 10;
  [SerializeField] SpawnManager spawnManager;
  [SerializeField] TextMeshProUGUI fuelBarText;
  [SerializeField] TextMeshProUGUI scoreBarText;

  public int FuelBar { get; set; }

  int _score = 0;
  bool _moveLeft;
  bool _moveRight;
  int _tempScore = 0;
  float _currentTurn = 0f;

  new void Start()
  {
    base.Start();
    fuelBarText.text = "Fuel\n" + FuelBar + "%";
  }

  new void Update()
  {
    base.Update();

    _tempScore = (int)transform.position.z / scoreRate;

    if (_score < _tempScore)
    {
      _score = _tempScore;
    }

    scoreBarText.text = "Score\n" + _score;
    fuelBarText.text = "Fuel\n" + FuelBar + "%";

    Turn();

    KeyboardControls();

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
      transform.Rotate(Vector3.up * Time.deltaTime * turnFixing);
    }

    if (_moveRight && Math.Abs(_currentTurn) < maxTurnAngle)
    {
      _currentTurn += turnRate;
    }
    if (!_moveRight && transform.localEulerAngles.y > 1f && transform.localEulerAngles.y < 180f)
    {
      transform.Rotate(Vector3.down * Time.deltaTime * turnFixing);
    }

    frontRightWheel.steerAngle = _currentTurn;
    frontLeftWheel.steerAngle = _currentTurn;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      GettingDamage(collision.gameObject.GetComponent<EnemyVehicle>().Damage);
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "SpawnTrigger")
    {
      spawnManager.SpawnTriggerExit();
    }
  }

  void KeyboardControls()
  {

    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
    {
      MoveLeftOn();
    }

    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
    {
      MoveLeftOff();
    }

    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
    {
      MoveRightOn();
    }

    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
    {
      MoveRightOff();
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

  public void GettingDamage(int contactDamage)
  {
    FuelBar = FuelBar - contactDamage < 0 ? 0 : FuelBar - contactDamage;
  }

  public void GettingHeal(int healRate)
  {
    FuelBar = FuelBar == 100 ? 100 : FuelBar + healRate;
  }
}
