using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPlayer : MonoBehaviour
{
    public int maxHP = 100;

    [SerializeField] private GameObject _healthBarUI;
    private int _currentHP { get; set; }

    private Image _imageHealthBar;
    private TextMeshProUGUI _textCountHealth;
    public int CurrentHP
    {
        get
        {
            return (_currentHP);
        }
        private set 
        {
            _currentHP = value;
            if (_currentHP < 0)
                _currentHP = 0;
            if (_currentHP > maxHP)
                _currentHP = maxHP;
            UpdateHealthBar();
            if (_currentHP == 0)
                PlayerDied();
        }
    }

    private void Start()
    {
        _imageHealthBar = _healthBarUI.GetComponent<Image>();
        _textCountHealth = _healthBarUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CurrentHP = maxHP;
    }

    public void TakeDamage(int damage) 
    {
        if (damage < 0)
            throw new InvalidOperationException("Damage not be negative number!!!");
        _currentHP -= damage;
    }

    public void HealingPlayer(int healing)
    {
        if (healing < 0)
            throw new InvalidOperationException("Healing not be negative number!!!");
        if (healing > maxHP)
            throw new InvalidOperationException("Healing not be larger maxHP!!!");

        _currentHP += healing;
    }

    private void PlayerDied()
    {

    }

    private void UpdateHealthBar() 
    {
        UpdateImageHealthBar();
        UpdateTextHealthBar();
    }

    private void UpdateTextHealthBar() 
    {
        _textCountHealth.text = $"{_currentHP}";
    }

    private void UpdateImageHealthBar()
    {
        // Formula for the percentage of two numbers
        _imageHealthBar.fillAmount = (((float)_currentHP * 100f) / (float)maxHP) / 100f;
    }
}