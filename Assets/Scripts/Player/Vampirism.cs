using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _distance;

    [SerializeField] private int _damage;
    [SerializeField] private int _duration;
    [SerializeField] private int _tickTime;

    [SerializeField] private List<Enemy> _enemies;

    public event UnityAction<int, int, int, List<Enemy>> Enabled;

    [SerializeField] private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = _distance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Enabled?.Invoke(_damage, _duration, _tickTime, _enemies);
        }
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.TryGetComponent(out Enemy currentEnemy))
        {
            _enemies.Add(currentEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.gameObject.TryGetComponent(out Enemy currentEnemy))
        {
            _enemies.Remove(currentEnemy);
        }
    }
}
