using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class startBUTTON : MonoBehaviour
{
    private Button start_button;
    private FIVE fiveScript;

    private void Awake()
    {
        start_button = GetComponent<Button>();
        start_button.onClick.AddListener(StartButton_);
    }
    private void Start()
    {
        fiveScript = GetComponent<FIVE>();
    }

    private void StartButton_()
    {
        //fiveScript.StartGame();
    }
    
}
