using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //public LevelController levelController;
    public DropdownController dropdownController;
    public Dropdown dropdownLevelSelector;

    // Start is called before the first frame update
    void Start()
    {
        LevelDataCollection levelDataCollection = LevelController.Instance.LoadLevelCollection();

        for(int disableIndex = 1; disableIndex < 5; disableIndex++)
        {
            dropdownController.indexesToDisable.Add(disableIndex);
        }

        for (int levelIndex = 0; levelIndex < levelDataCollection.LevelsData.Count - 1; levelIndex++)
        {
            if(levelDataCollection.LevelsData[levelIndex].IsCompleted)
            {
                dropdownController.indexesToDisable.Remove(levelIndex + 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel()
    {
        LevelController.Instance.SelectedLevel = dropdownLevelSelector.value;
        SceneManager.LoadScene(1);
    }
}
