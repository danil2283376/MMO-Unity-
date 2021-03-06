using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class StaminaPlayer : MonoBehaviour
{
    public float maxStamina = 100;

    [SerializeField] private Image _staminaBarUI;
    [SerializeField] private Image _maxStaminaBarUI;

    private float _currentStamina { get; set; }
    public float CurrentStamina
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
            if ((int)_currentStamina > (int)maxStamina)
                _currentStamina = maxStamina;
            UpdateStaminaBar();
            //if (_currentStamina == 0)
            //    LackOfStamina();
        }
    }

    public float MaxStamina
    {
        get
        {
            return (maxStamina);
        }
        set
        {
            maxStamina = value;
            if (maxStamina < 0)
                maxStamina = 0;
            if ((value + maxStamina) < maxStamina)
                maxStamina = value;
            UpdateStaminaBar();
        }
    }

    private void Start()
    {
        //_imageStaminaBar = _staminaBarUI.GetComponent<Image>();

        CurrentStamina = maxStamina;

    }

    public void UpStamina(float stamina)
    {
        if (stamina < 0)
            throw new InvalidOperationException("UpSatiety not be negative number!!!");
        CurrentStamina += stamina;
    }

    public void DownStamina(float stamina)
    {
        if (stamina < 0)
            throw new InvalidOperationException("DownSatiety not be negative number!!!");
        CurrentStamina -= stamina;
    }

    public void LackOfStamina(float subtractMaxStamina)
    {
        maxStamina -= subtractMaxStamina;
    }

    private void UpdateStaminaBar()
    {
        UpdateImageStaminaBar();
        UpdateMaxStaminaBar();
    }

    private void UpdateImageStaminaBar()
    {
        if (_currentStamina <= maxStamina)
            _staminaBarUI.fillAmount = (((float)_currentStamina * 100f) / 100f) / 100f;
    }

    private void UpdateMaxStaminaBar()
    {
        _maxStaminaBarUI.fillAmount = maxStamina / 100;
    }
}
