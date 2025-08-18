using System;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    [SerializeField] AudioClip[] damageSFXs;
    private int health;
    public event Action<int> OnHealthChanged;
    private AudioSource audioSource;

    void Start()
    {
        health = MaxHealth;
        audioSource = GetComponent<AudioSource>();
        OnHealthChanged?.Invoke(health);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) health = 0;
        OnHealthChanged?.Invoke(health);
        PlayHitSound();
    }

    void PlayHitSound()
    {
        if (damageSFXs.Length == 0) return;
        int index = UnityEngine.Random.Range(0, damageSFXs.Length);
        audioSource.PlayOneShot(damageSFXs[index]);
    }
}