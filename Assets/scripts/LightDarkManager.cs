using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkManager : MonoBehaviour
{
    public static LightDarkManager instance;
    private bool mode = false; // true = light, false = dark
    [SerializeField]
    private SpriteRenderer bg;
    [SerializeField]
    private List<GameObject> lightSide = new List<GameObject>();
    [SerializeField]
    private List<GameObject> darkSide = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Object.Destroy(this);
    }

    public void ModeSwap()
    {
        mode = !mode;
        foreach (GameObject obj in lightSide)
            obj.SetActive(mode);
        foreach (GameObject obj in darkSide)
            obj.SetActive(!mode);
        bg.color = mode ? Color.black : Color.white;
    }


}
