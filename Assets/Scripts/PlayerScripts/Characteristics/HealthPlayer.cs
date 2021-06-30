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
    private int currentHP { get; set; }

    private Image _imageHealthBar;
    private TextMeshProUGUI _textCountHealth;
    public int CurrentHP
    {
        get
        {
            return (currentHP);
        }
        private set 
        {
            currentHP = value;
            if (currentHP < 0)
                currentHP = 0;
            if (currentHP > maxHP)
                currentHP = maxHP;
            UpdateHealthBar();
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
        currentHP -= damage;
    }

    public void HealingPlayer(int healing)
    {
        if (healing < 0)
            throw new InvalidOperationException("Healing not be negative number!!!");
        if (healing > maxHP)
            throw new InvalidOperationException("Healing not be larger maxHP!!!");

        currentHP += healing;
    }

    private void UpdateHealthBar() 
    {
        UpdateImageHealthBar();
        UpdateTextHealthBar();
    }

    private void UpdateTextHealthBar() 
    {
        _textCountHealth.text = $"{currentHP}";
    }

    private void UpdateImageHealthBar()
    {
        // Formula for the percentage of two numbers
        _imageHealthBar.fillAmount = (((float)currentHP * 100f) / (float)maxHP) / 100f;
    }
}