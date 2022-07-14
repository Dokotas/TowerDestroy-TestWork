using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float health, damage;
    
    private float _currentHealth;
    private bool _available = true;

    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            _currentHealth -= damage;
            Destroy(other.gameObject);
        }
        
        if (_currentHealth <= 0)
            Init();
    }
    
    private void Init()
    {
        gameObject.SetActive(false);
        _currentHealth = health;
    }

    public bool Activate(int cooldown)
    {
        if (!gameObject.activeSelf && _available)
        {
            gameObject.SetActive(true);
            StartCoroutine(CooldownTimer(cooldown));
            return true;
        }
        return false;
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    IEnumerator CooldownTimer(int cooldown)
    {
        _available = false;
        for (int i = 0; i < cooldown; i++)
        {
            yield return new WaitForSeconds(1f);
        }

        _available = true;
    }

}