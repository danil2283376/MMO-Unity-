using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaPlayer : MonoBehaviour
{

    public int maxStamina = 100;

    [SerializeField] private GameObject _staminaBarUI;

    private int _currentStamina { get; set; }

    private Image _imageStaminaBar;

    public int CurrentStamina
    {
        get
        {
            return (_currentStamina);
        }
        private set
        {
            _currentStamina = value;
            if (_currentStamina < 0)
                _currentStamina = 0;
            if (_currentStamina > maxStamina)
                _currentStamina = maxStamina;
            UpdateSatietyBar();
            if (_currentStamina == 0)
                LackOfStamina();
        }
    }

    private void Start()
    {
        _imageStaminaBar = _imageStaminaBar.GetComponent<Image>();
        CurrentStamina = maxStamina;
    }

    public void UpStamina(int stamina)
    {
        if (stamina < 0)
            throw new InvalidOperationException("UpSatiety not be negative number!!!");
        if (stamina > maxStamina)
            throw new InvalidOperationException("UpSatiety not be larger maxSatiety!!!");
        CurrentStamina += stamina;
    }

    public void DownStamina(int stamina)
    {
        if (stamina < 0)
            throw new InvalidOperationException("DownSatiety not be negative number!!!");
        CurrentStamina -= stamina;
    }

    private void LackOfStamina()
    {

    }

    private void UpdateSatietyBar()
    {
        UpdateImageSatietyBar();
    }

    private void UpdateImageSatietyBar()
    {
        _imageStaminaBar.fillAmount = (((float)_currentStamina * 100f) / (float)maxStamina) / 100f;
    }
}
