using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
  [SerializeField] GameObject leftRed;
  [SerializeField] GameObject rightRed;

  void Start()
  {
    if (PlayerPrefs.GetString("LastScene") == "Main Menu")
    {
      StartCoroutine(InstructionButtonsWait());
    }
  }

  IEnumerator InstructionButtonsWait()
  {
    leftRed.SetActive(true);
    yield return new WaitForSeconds(2);
    leftRed.SetActive(false);
    rightRed.SetActive(true);
    yield return new WaitForSeconds(2);
    rightRed.SetActive(false);
  }

}
