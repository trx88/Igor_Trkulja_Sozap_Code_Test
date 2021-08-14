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

        for(int disableIndex = 1; disableIndex < levelDataCollection.LevelsData.Count; disableIndex++)
        {
            dropdownController.indexesToDisable.Add(disableIndex);
        }

        for (int levelIndex = 1; levelIndex < levelDataCollection.LevelsData.Count; levelIndex++)
        {
            if(levelDataCollection.LevelsData[levelIndex].IsCompleted)
            {
                dropdownController.indexesToDisable.Remove(levelIndex);
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
