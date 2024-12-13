using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : MonoBehaviour
{
    public bool selected = false; // Whether the skill is selected
    public int attack = 15; // Damage amount
    public float stamina = 10f; // Stamina cost (for potential use)
    private bool canDealDamage = false; // Whether the skill is ready to deal damage

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("A");
            selected = !selected;
        }
        // Check if the skill is selected and the left mouse button is clicked
        if (selected && Input.GetMouseButtonDown(0)) // 0 = left mouse button
        {
            canDealDamage = true; // Enable damage for this frame
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision detected with: {other.gameObject.name}");

        // Check if damage can be dealt and the collided object has the "unit" tag
        if (canDealDamage && other.CompareTag("unit"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                // Deal damage and log the result
                enemy.TakeDamage(attack);
                Debug.Log($"{other.name} took {attack} damage.");
            }
            canDealDamage = false; // Reset damage ability after dealing damage
        }
    }
}
