using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Windows")] 
    [SerializeField] private GameObject WindowPlay; [SerializeField] private bool isWindowPlayActive;
    [SerializeField] private GameObject WindowSettings; [SerializeField] private bool isWindowSettingsActive;
    [SerializeField] private GameObject WindowAbout; [SerializeField] private bool isWindowAboutActive;
    

    public void ButtonPlay()
    {
        if (!isWindowPlayActive) 
        { 
            CloseAll();
            WindowPlay.SetActive(true);
            WindowPlay.GetComponent<Animator>().SetTrigger("Appear"); 
            isWindowPlayActive = true; 
        }
        else if (isWindowPlayActive)
        {
            WindowPlay.GetComponent<Animator>().SetTrigger("Disappear"); 
            isWindowPlayActive = false;
        }
    }
    
    public void ButtonAbout()
    {
        if (!isWindowAboutActive) 
        { 
            CloseAll();
            WindowAbout.SetActive(true);
            WindowAbout.GetComponent<Animator>().SetTrigger("Appear"); 
            isWindowAboutActive = true; 
        }
        else if (isWindowAboutActive)
        {
            WindowAbout.GetComponent<Animator>().SetTrigger("Disappear"); 
            isWindowAboutActive = false;
        }
    }

    public void CloseAll()
    {
        if (isWindowPlayActive) { WindowPlay.GetComponent<Animator>().SetTrigger("Disappear"); isWindowPlayActive = false; }
        if (isWindowSettingsActive) { WindowSettings.GetComponent<Animator>().SetTrigger("Disappear"); isWindowSettingsActive = false; }
        if (isWindowAboutActive) { WindowAbout.GetComponent<Animator>().SetTrigger("Disappear"); isWindowAboutActive = false; }
    }
}
