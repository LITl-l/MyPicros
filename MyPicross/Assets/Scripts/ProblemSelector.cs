using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProblemSelector : MonoBehaviour
{
  [SerializeField] ScriptableProblems _problems;

  public void OnPointerDown(string address)
  {
    _problems.ProblemAddress = address;
    SceneManager.LoadScene("PicrossScene", LoadSceneMode.Single);
  }
}
