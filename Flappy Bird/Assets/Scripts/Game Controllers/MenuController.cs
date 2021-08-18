using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [SerializeField] GameObject[] birds;

    private bool isGreenBirdUnlocked, isRedBirdUnlocked;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 1f;
    }

    private void Start()
    {
        birds[GameController.instance.SelectedBird].SetActive(true);
        CheckIfBirdsAreUnlocked();
        ShowSelectedBird();
    }

    private void MakeInstance()
    {
        if (instance != null)
            instance = this;
    }

    private void CheckIfBirdsAreUnlocked()
    {
        if (GameController.instance.IsGreenBirdUnlocked() == 1)
            isGreenBirdUnlocked = true;
        if (GameController.instance.IsRedBirdUnlocked() == 1)
            isRedBirdUnlocked = true;
    }

    private void ShowSelectedBird()
    {
        birds[GameController.instance.SelectedBird].SetActive(true);
    }

    public void ChangeBird()
    {
        // 0 - blue
        // 1 - green
        // 2 - red
        Debug.Log("Selected " + GameController.instance.SelectedBird);
        Debug.Log(GameController.instance.IsGreenBirdUnlocked());

        if(GameController.instance.SelectedBird == 0) // blue bird selected 
        {
            if(isGreenBirdUnlocked)
            {
                birds[0].SetActive(false);
                GameController.instance.SelectedBird = 1;
                birds[GameController.instance.SelectedBird].SetActive(true);
            }
        } 
        else if (GameController.instance.SelectedBird == 1)
        {
            if(isRedBirdUnlocked)
            {
                birds[1].SetActive(false);
                GameController.instance.SelectedBird = 2;
                birds[GameController.instance.SelectedBird].SetActive(true);
            }
            else
            {
                birds[1].SetActive(false);
                GameController.instance.SelectedBird = 0;
                birds[GameController.instance.SelectedBird].SetActive(true);
            }
        }
        else if(GameController.instance.SelectedBird == 2)
        {
            birds[2].SetActive(false);
            GameController.instance.SelectedBird = 0;
            birds[GameController.instance.SelectedBird].SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeInAndOut("Gameplay");
    }
}
