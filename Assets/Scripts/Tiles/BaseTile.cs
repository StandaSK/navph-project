using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The player's base is located here. Nothing can be built on this tile.
/// </summary>
public class BaseTile : Tile
{
    public Alignment alignment;

    [SerializeField]
    private int startingHealth = 10;

    public UnityEvent baseDied;

    private int healthPoints;

    protected override void Awake()
    {
        base.Awake();
        Buildable = false;
        healthPoints = startingHealth;
    }

    public bool IsDead()
    {
        return healthPoints <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= healthPoints)
        {
            // The base is destroyed
            healthPoints = 0;
            baseDied.Invoke();
        }
        else
        {
            healthPoints -= damage;
            Debug.Log("Remaining health of " + transform.parent.name + ": " + healthPoints);
        }
    }
}
