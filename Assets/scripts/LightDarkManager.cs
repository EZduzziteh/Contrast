using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LightDarkManager : NetworkBehaviour
{
    public static LightDarkManager instance;
    private NetworkVariable<bool> mode = new NetworkVariable<bool>(); // true = light, false = dark
    [SerializeField]
    private SpriteRenderer bg;
    [SerializeField]
    private List<GameObject> lightSide = new List<GameObject>();
    [SerializeField]
    private List<GameObject> darkSide = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Object.Destroy(this);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        mode.Value = true;
        foreach (GameObject obj in darkSide)
            obj.SetActive(false);
        mode.OnValueChanged += OnValueChanged;
    }

    public void SwitchModes()
    {
        mode.Value = !mode.Value;
        Debug.Log("switch");
    }

    public void OnValueChanged(bool previousValue, bool newValue)
    {
        foreach (GameObject obj in lightSide)
            obj.SetActive(newValue);
        foreach (GameObject obj in darkSide)
            obj.SetActive(previousValue);
        bg.color = newValue ? Color.white : Color.black;
    }


}
