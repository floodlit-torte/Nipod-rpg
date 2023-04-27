using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;
    public int MaxHealthPoints
    {
        get { return maxHealthPoints; }
    }
    
    [SerializeField] private int maxSoulPoints;

    public int MaxSoulPoints
    {
        get { return maxSoulPoints; }
    }

    private int _currentHP;
    private int _currentSP;

    private void Awake()
    {
        _currentHP = maxHealthPoints;
        _currentSP = maxSoulPoints;
    }


    public void TakeDamage(int damage)
    {
        _currentHP -= Mathf.Abs(damage);

        if (_currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
