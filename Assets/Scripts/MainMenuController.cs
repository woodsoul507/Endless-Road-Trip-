using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
  public void StartApp()
  {
    SaveSceneName();
    SceneManager.LoadScene("Level1");
  }
  public void QuitApp()
  {
    Application.Quit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
  }

  // ABSTRACTION
  void SaveSceneName()
  {
    string currentScene = SceneManager.GetActiveScene().name;
    PlayerPrefs.SetString("LastScene", currentScene);
    PlayerPrefs.Save();
  }
}
