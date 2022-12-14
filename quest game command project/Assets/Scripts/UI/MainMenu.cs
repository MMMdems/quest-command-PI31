using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Windows")] 
    [SerializeField] private GameObject WindowPlay;
    [SerializeField] private bool isWindowPlayActive;

    public void ButtonPlay()
    {
        if (!isWindowPlayActive) { WindowPlay.GetComponent<Animator>().SetTrigger("Appear"); isWindowPlayActive = true; }
        else if (isWindowPlayActive) { WindowPlay.GetComponent<Animator>().SetTrigger("Disappear"); isWindowPlayActive = false; }
    }

    public void ButtonCloseAll()
    {
        
    }
}
