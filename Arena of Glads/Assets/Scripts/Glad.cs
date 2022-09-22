using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glad : MonoBehaviour
{
    public Stats stats;

    private void Start()
    {
        stats.Initialize();
    }

    public void TakeDamage(float value)
    {
        stats.DecreaseHP(value);
        gameObject.SetActive(!stats.IsDead);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            TakeDamage(75);
        }
    }
}
