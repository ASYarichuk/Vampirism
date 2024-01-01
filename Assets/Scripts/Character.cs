using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health = 3;

    public event Action<float, int, int> HealthChanged;

    public int Health => _health;

    private float _percent;

    [SerializeField] private int _maxHealth = 3;

    private void Awake()
    {
        _health = Health;
    }

    private void Update()
    {
        ChangeHealth();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amountHealth)
    {
        _health += amountHealth;
    }

    private void ChangeHealth()
    {
        _health = Health;
        _percent = (float)_health / _maxHealth;
        HealthChanged?.Invoke(_percent, _health, _maxHealth);
    }
}
