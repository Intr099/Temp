using UnityEngine;
using UnityEngine.SceneManagement;

// Q4: Scene management helper (scenes must be added in File > Build Profiles / Build Settings)
public class SceneLoader : MonoBehaviour
{
    public static void Load(string sceneName) => SceneManager.LoadScene(sceneName);
    public void LoadByName(string sceneName) => Load(sceneName);          // for UI Buttons
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void Quit() => Application.Quit();
}
