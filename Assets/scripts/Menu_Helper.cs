using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Helper : MonoBehaviour
{

    [SerializeField]
    Canvas instructionsCanvas;
    [SerializeField]
    Canvas optionsCanvas;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowInstructions()
    {
        instructionsCanvas.gameObject.SetActive(true);
      
    }

    public void ShowOptions()
    {
        optionsCanvas.gameObject.SetActive(true);
        
    }
}
