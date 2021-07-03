using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SatietyPlayer : MonoBehaviour
{
    public int maxSatiety = 100;
    public float damageAtStarvation = 2f;
    public float starve = 10f;

    [SerializeField] private GameObject _satietyBarUI;

    private float _currentSatiety { get; set; }

    private Image _imageSatietyBar;

    public float CurrentSatiety
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

    private void Update()
    {
        if (CurrentSatiety > 0)
        {
            DownSatiety(starve * Time.deltaTime);
        }
        else
            Starvation();
    }

    public void UpSatiety(int satiety)
    {
        if (satiety < 0)
            throw new InvalidOperationException("UpSatiety not be negative number!!!");
        if (satiety > maxSatiety)
            throw new InvalidOperationException("UpSatiety not be larger maxSatiety!!!");
        CurrentSatiety += satiety;
    }

    public void DownSatiety(float satiety)
    {
        if (satiety < 0)
            throw new InvalidOperationException("DownSatiety not be negative number!!!");
        CurrentSatiety -= satiety;
    }

    private void Starvation()
    {
        HealthPlayer healthPlayer = gameObject.GetComponent<HealthPlayer>();
        healthPlayer.TakeDamage(damageAtStarvation * Time.deltaTime);
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
