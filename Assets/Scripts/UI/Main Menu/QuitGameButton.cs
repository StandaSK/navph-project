using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitGameButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        // Code from http://answers.unity.com/answers/1157271/view.html
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
