using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SatietyPlayer : MonoBehaviour
{
    public int maxSatiety = 100;

    [SerializeField] private GameObject _satietyBarUI;

    private int _currentSatiety { get; set; }

    private Image _imageSatietyBar;

    public int CurrentSatiety
    {
        get
        {
            return (_currentSatiety);
        }
        private set
        {
            _currentSatiety = value;
            if (_currentSatiety < 0)
                _currentSatiety = 0;
            if (_currentSatiety > maxSatiety)
                _currentSatiety = maxSatiety;
            UpdateSatietyBar();
            if (_currentSatiety == 0)
                Starvation();
        }
    }

    private void Start()
    {
        _imageSatietyBar = _satietyBarUI.GetComponent<Image>();
        CurrentSatiety = maxSatiety;
    }

    public void UpSatiety(int satiety)
    {
        if (satiety < 0)
            throw new InvalidOperationException("UpSatiety not be negative number!!!");
        if (satiety > maxSatiety)
            throw new InvalidOperationException("UpSatiety not be larger maxSatiety!!!");
        CurrentSatiety += satiety;
    }

    public void DownSatiety(int satiety)
    {
        if (satiety < 0)
            throw new InvalidOperationException("DownSatiety not be negative number!!!");
        CurrentSatiety -= satiety;
    }

    private void Starvation()
    {

    }

    private void UpdateSatietyBar() 
    {
        UpdateImageSatietyBar();
    }

    private void UpdateImageSatietyBar() 
    {
        _imageSatietyBar.fillAmount = (((float)_currentSatiety * 100f) / (float)maxSatiety) / 100f;
    }
}
