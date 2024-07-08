using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Resources : MonoBehaviour
{
    int startingHealth = 1000;
    int startingGold = 989999;
    int startingGems = 99999;

    [SerializeField] int currentHealth;
    [SerializeField] int currentGems;
    [SerializeField] int currentGold;

    [SerializeField] TextMeshProUGUI displayGold;
    [SerializeField] TextMeshProUGUI displayGems;
    [SerializeField] TextMeshProUGUI displayHealth;

    public int CurrentGold { get { return currentGold; } }
    public int CurrentGems { get { return currentGems; } }


    private void Awake()
    {
        currentHealth = startingHealth;
        currentGold = startingGold;
        currentGems = startingGems;
        UpdateResources();
    }


    public void Deposit(int goldAmount, int gemAmount = 0)
    {
        currentGold += Mathf.Abs(goldAmount);
        currentGems += Mathf.Abs(gemAmount);
        UpdateResources();
        if (currentGold < 0 || currentGems < 0)
        {
            ReloadScene();
        }
    }

    public void Withdraw(int goldAmount, int gemAmount = 0)
    {
        currentGold -= Mathf.Abs(goldAmount);
        currentGems -= Mathf.Abs(gemAmount);
        UpdateResources();
        if (currentGold < 0 || currentGems < 0)
        {
            ReloadScene();
        }
    }

    public void HealthPenalty(int healthPenalty)
    {
        currentHealth -= Mathf.Abs(healthPenalty);
        UpdateResources();
        if (currentHealth <= 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void UpdateResources()
    {
        displayGems.text = "Gems: " + currentGems;
        displayGold.text = "Gold: " + currentGold;
        displayHealth.text = "Health: " + currentHealth;
    }
}
