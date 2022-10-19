using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Task = System.Threading.Tasks.Task;

public class UI_Dissolve : MonoBehaviour
{
    private Image _image;
    private Material _mat;

    private bool _isFadeOut = false;
    [SerializeField] private float timeBfStart = 0f;
    private float _startTimer = 0f; private bool _isAwait = true;
    
    [SerializeField] private float fadeTime = 0.4f;
    private float _timer = 0, _kTime = 0;
    private void Start()
    {
        _image = GetComponent<Image>();
        _mat = _image.material;
        _mat.SetFloat("_Level",0f);
    }
    
    private void Update()
    {
        if (_isAwait)
        {
            StartFadeOut();
            _startTimer += Time.deltaTime;
            if (_startTimer > timeBfStart) _startTimer = timeBfStart;
        }

        if (_isFadeOut) FadeOut();
    }

    private async void StartFadeOut()
    {
        while (_startTimer != timeBfStart) { await Task.Yield(); }
        if (_startTimer == timeBfStart) { _startTimer = 0; }
        print("Fade Out Start");
        _isAwait = false;
        _isFadeOut = true;
    }
    private void FadeOut()
    {
        _timer += Time.deltaTime;
        if (_timer > fadeTime) { _timer = fadeTime; }
        _kTime = _timer / fadeTime;
        _mat.SetFloat("_Level", _kTime);
        if (_kTime == 1f)
        {
            _isFadeOut = false;
            _timer = 0;
            gameObject.SetActive(false);
            _mat.SetFloat("_Level", 0f);
        }
    }
}
