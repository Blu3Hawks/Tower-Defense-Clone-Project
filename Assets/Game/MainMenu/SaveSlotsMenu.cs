using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private StartingMenu startingMenu;

    private SaveSlot[] saveSlots;

    [SerializeField] private Button backButton;

    [Header("Confirmation Menu")]
    [SerializeField] private ConfirmationMenu confirmationMenu;
    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();
        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        else if (saveSlot.hasData)
        {
            confirmationMenu.ActivateMenu(
                "Starting a new game with this save slot will override the existing saved data. Are you sure?",
                //in case we pressed 'yes'
                () =>
                {

                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                //in case we pressed 'cancel'
                () =>
                {

                    //TODO - come back to this
                    this.ActivateMenu(isLoadingGame);
                }
                );
                
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }

    }

    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainBaseScene");
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
    public void OnBackClicked()
    {
        startingMenu.ActivateMenu();
        this.DeactivateMenu();

    }

    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);
        
        backButton.interactable = true;

        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesData();

        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        confirmationMenu.ActivateMenu(
            "Are you sure you want to delete this saved file?",
            () =>
            {
                DataPersistenceManager.instance.DeleteProfileId(saveSlot.GetProfileId());
                ActivateMenu(isLoadingGame);
            },
            () =>
            {
                ActivateMenu(isLoadingGame);
            }
            );
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
