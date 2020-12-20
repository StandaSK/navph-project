using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelChangeButton : MonoBehaviour
{
    [SerializeField]
    private string levelSceneName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
