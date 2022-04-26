using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject player;
  [SerializeField] GameObject gameOver;
  [SerializeField] int startingFuel;
  [SerializeField] int stuckDamage = 7;
  [SerializeField] float disableEnemiesAfterStuckRange = 120f;

  int _playerFuelBar;

  void Start()
  {
    Time.timeScale = 1;
    player.GetComponent<PlayerController>().FuelBar = startingFuel;
  }

  void Update()
  {
    if (player.GetComponent<PlayerController>().FreezeTime >=
    player.GetComponent<PlayerController>().FreezeTimeAllowed ||
    player.GetComponent<PlayerController>().RoadsPushedBack >= 2)
    {
      player.GetComponent<PlayerController>().FreezeTime = 0f;
      player.GetComponent<PlayerController>().RoadsPushedBack = 0;
      PlayerFreezed();
    }

    _playerFuelBar = player.GetComponent<PlayerController>().FuelBar;

    if (_playerFuelBar <= 0)
    {
      GameOver();
    }
  }

  // ABSTRACTION
  void PlayerFreezed()
  {
    player.gameObject.GetComponent<PlayerController>().GettingDamage(stuckDamage);

    Time.timeScale = 0;

    List<EnemyVehicle> enemies = new List<EnemyVehicle>(FindObjectsOfType<EnemyVehicle>());
    enemies.ForEach(enemy =>
    {
      if (enemy.gameObject.activeSelf && (enemy.gameObject.GetComponent<EnemyVehicle>().FreezeTime >=
      enemy.gameObject.GetComponent<EnemyVehicle>().FreezeTimeAllowed ||
      enemy.gameObject.transform.position.z < player.gameObject.transform.position.z + disableEnemiesAfterStuckRange))
      {
        enemy.gameObject.SetActive(false);
      }
    });

    player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    player.gameObject.transform.position = new Vector3(
      player.gameObject.transform.position.x,
      0f,
      player.gameObject.transform.position.z
    );
    player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5500, ForceMode.Impulse);

    Time.timeScale = 1;
  }

  // ABSTRACTION
  void GameOver()
  {
    gameOver.SetActive(true);
    Time.timeScale = 0;
  }

  // ABSTRACTION
  public void TryAgain()
  {
    SaveSceneName();
    SceneManager.LoadScene("Level1");
  }

  public void Exit()
  {
    SaveSceneName();
    SceneManager.LoadScene("Main Menu");
  }

  void SaveSceneName()
  {
    string currentScene = SceneManager.GetActiveScene().name;
    PlayerPrefs.SetString("LastScene", currentScene);
    PlayerPrefs.Save();
  }
}
