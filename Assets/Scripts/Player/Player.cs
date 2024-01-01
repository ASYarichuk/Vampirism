using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : Character
{
    [SerializeField] private int _damage = 2;
    [SerializeField] private Vampirism _vampirism;

    private int _maxHealth = 10;

    private Enemy _enemy;

    private void OnEnable()
    {
        _vampirism.Enabled += OnEnabled;
    }

    private void OnDisable()
    {
        _vampirism.Enabled -= OnEnabled;
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.GetComponent<Enemy>())
        {
            _enemy = enemy.gameObject.GetComponent<Enemy>();
            StartCoroutine(CauseDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D enemy)
    {
        if (enemy.gameObject.GetComponent<Enemy>())
        {
            StopCoroutine(CauseDamage());
            _enemy = null;
        }
    }

    private IEnumerator CauseDamage()
    {
        var waitForOneSecond = new WaitForSeconds(1.0f);

        while (_enemy != null)
        {
            _enemy.TakeDamage(_damage);
            yield return waitForOneSecond;
        }
    }

    public void TakeAidKit(int amountHealth)
    {
        CheckHealth(amountHealth);
    }

    private void OnEnabled(int damage, int duration, int tickTime, List<Enemy> enemies)
    {
        StartCoroutine(OnTurnVampire(damage, duration, tickTime, enemies));
    }

    private IEnumerator OnTurnVampire (int damage, int duration, int tickTime, List<Enemy> enemies)
    {
        for (int i = 0; i < duration; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                enemies[j].TakeDamage(damage);
                CheckHealth(damage);
            }

            yield return new WaitForSeconds(tickTime);
        }
    }

    private void CheckHealth(int amountHealth)
    {
        if (Health + amountHealth >= _maxHealth)
        {
            AddHealth(_maxHealth - Health);
        }
        else
        {
            AddHealth(amountHealth);
        }
    }
}
