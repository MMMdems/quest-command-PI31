using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    public bool isPause { get; private set; } = false;
    [SerializeField] private InputtingManager inputSetting;
    [SerializeField] private GameObject pauseInterface;

    private void Update()
    {
        if (Input.GetKeyDown(inputSetting.PauseKey) && !isPause) { isPause = true; }
        if (Input.GetKeyDown(inputSetting.PauseKey) && isPause) { isPause = false; }

        switch (isPause)
        {
            case true: { Time.timeScale = 0; pauseInterface.SetActive(true); break; }
            case false: { Time.timeScale = 1; pauseInterface.SetActive(false); break; }
        }
    }
}
