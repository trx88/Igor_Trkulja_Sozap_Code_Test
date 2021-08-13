using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for making sample map JSONs
public class TestSerialization : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;

    public void SaveIntoJson()
    {
        string map = JsonUtility.ToJson(mapData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/MapData.json", map);
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveIntoJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
