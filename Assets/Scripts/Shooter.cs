using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected float CurrentHealth;

    public Cannon cannon;
    public Shield shield;

    public Action<float> healthChanged;
    public Action<bool> killed;

    public float health, damage;
    [Range(0, 59)] public int shieldCooldown;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !shield.IsActive())
        {
            CurrentHealth -= damage;
            healthChanged?.Invoke(CurrentHealth / health * 100);
            Destroy(other.gameObject);
        }

        if (CurrentHealth <= 0)
            killed?.Invoke(CompareTag("Player"));
    }

    public bool UseShield()
    {
        return shield.Activate(shieldCooldown);
    }
}