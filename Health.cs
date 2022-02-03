using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private UnityEvent unityEvent;
    [SerializeField] private Team team;
    [SerializeField] private Image imageHealth;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Image imageHealth2;
    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }

    public Team Team { get => team; set => team = value; }
    public int _Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth;}

    private void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            HealthBarUpdate();
            if (health <= 0)
            {
                health = 0;
                unityEvent?.Invoke();
                if (explosionEffect != null)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public bool Healing(int count)
    {
        if (health == maxHealth)
        {
            return false;
        }
        if (health + count > maxHealth)
        {
            health = maxHealth;
            HealthBarUpdate();
            return true;
        }
        else
        {
            health += count;
            HealthBarUpdate();
            return true;
        }
    }

    private void HealthBarUpdate()
    {
        if (imageHealth != null)
        {
            imageHealth.fillAmount = (float)health / (float)maxHealth;
        }
        if (imageHealth2 != null)
        {
            imageHealth2.fillAmount = (float)health / (float)maxHealth;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if ((bullet != null) && (Team != bullet.Team))
        {
            
            TakeDamage(bullet.Damage);
            Destroy(collision.gameObject);
        }
    }
}
