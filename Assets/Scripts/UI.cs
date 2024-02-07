using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class UI : MonoBehaviour
{
    [SerializeField] InputActionReference hideInput;


    public GameObject background;
    public GameObject text;
    public GameObject tab;

    public bool isOn;




    public void Update()
    {
        if (hideInput.action.WasReleasedThisFrame())
        {
            isOn = !isOn;
            Hide();
        }
    }





    private void Hide()
    {
        if (!isOn)
        {
            background.SetActive(false);
            text.SetActive(false);
            tab.SetActive(true);
        }
        else
        {
            background.SetActive(true);
            text.SetActive(true);
            tab.SetActive(false);
        }
    }


    
}
