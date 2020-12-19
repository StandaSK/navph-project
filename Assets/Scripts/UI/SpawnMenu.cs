using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    public Player player;

    private bool spawnMenuOpen = true;

    public void CloseSpawnMenu()
    {
        if (spawnMenuOpen)
        {
            var mypos = transform.position;
            mypos.x -= 100;
            transform.position = mypos;

            spawnMenuOpen = false;
        }
    }

    public void OpenSpawnMenu()
    {
        if (!spawnMenuOpen)
        {
            var mypos = transform.position;
            mypos.x += 100;
            transform.position = mypos;

            spawnMenuOpen = true;
        }
    }

    public void ToggleBuildMenu()
    {
        if (spawnMenuOpen)
        {
            CloseSpawnMenu();
        }
        else
        {
            OpenSpawnMenu();
        }
    }
}
