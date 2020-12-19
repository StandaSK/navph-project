using UnityEngine;

/// <summary>
/// The player's base is located here. Nothing can be built on this tile.
/// </summary>
public class BaseTile : Tile
{
    public Alignment alignment;

    [SerializeField]
    private int startingHealth = 10;

    private int healthPoints;

    protected override void Awake()
    {
        base.Awake();
        Buildable = false;
        healthPoints = startingHealth;
    }

    public void TakeDamage(int damage, Alignment takenFrom)
    {
        if (damage >= healthPoints)
        {
            // The base is destroyed
            healthPoints = 0;

            // TODO: make a proper victory screen instead of Debug.Log and quitting the game
            Debug.Log(alignment.name + " has lost the game!\n" + takenFrom.name + " has won the game!");

            // Code from http://answers.unity.com/answers/1157271/view.html
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
        else
        {
            healthPoints -= damage;
            Debug.Log("Remaining health of " + transform.parent.name + ": " + healthPoints);
        }
    }
}
