using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject player;
  [SerializeField] GameObject gameOver;
  [SerializeField] int startingHealth;

  int _playerHealthBar;

  void Start()
  {
    Time.timeScale = 1;
    player.GetComponent<PlayerController>().HealthBar = startingHealth;
  }

  void Update()
  {
    _playerHealthBar = player.GetComponent<PlayerController>().HealthBar;

    if (_playerHealthBar <= 0)
    {
      GameOver();
    }
  }

  void GameOver()
  {
    gameOver.SetActive(true);
    Time.timeScale = 0;
  }
}
