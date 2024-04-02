using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Sholve : MonoBehaviour
{
    public GameEventBase<string> ShovelName;

    [SerializeField] private Sprite shovelSprite;
    [SerializeField] private GameObject shovelObject;
    [SerializeField] private Image Icon;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(selectSholve);
    }

    private void selectSholve()
    {
        ShovelName.Raise(shovelObject.name);
    }

    private void OnValidate()
    {
        if (shovelSprite)
        {
            Icon.enabled = true;
            Icon.sprite = shovelSprite;
        }
        else
        {
            Icon.enabled = false;
        }
    }


}
