using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void StartTheGame()
    {
        SceneFader.Instance.LoadLevel("Level1");
    }
}
