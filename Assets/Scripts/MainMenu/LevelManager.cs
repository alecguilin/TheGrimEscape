using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;

    // Start is called before the first frame update
    void Start()
    {
        
        level1.onClick.AddListener(LoadLevel1);
        level2.onClick.AddListener(LoadLevel2);
        level3.onClick.AddListener(LoadLevel3);

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    

    void LoadLevel1() { SceneManager.LoadScene("Level1"); }
    void LoadLevel2() { SceneManager.LoadScene("Test_Level"); }
    void LoadLevel3() { SceneManager.LoadScene("Level3"); }

}
