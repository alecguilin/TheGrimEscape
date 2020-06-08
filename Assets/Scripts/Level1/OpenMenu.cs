using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    public Button exitButton;
    public Canvas canvas_1;
    private bool menuSwitch = false;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(loadMainScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("OpenMenu") == 1) {
            menuSwitch = !menuSwitch;
        }
        if (menuSwitch)
            canvas_1.enabled = true;
        else
            canvas_1.enabled = false;
    }

    void loadMainScreen() { SceneManager.LoadScene("MainMenu"); }
}
