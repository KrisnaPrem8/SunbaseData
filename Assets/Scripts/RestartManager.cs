using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartManager : MonoBehaviour
{
    public GameObject panel;
    public static RestartManager instance;

    void Start()
    {
        instance = this;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }
}
