using UnityEngine;
using UnityEngine.SceneManagement;

// Q5: Main Menu scene (needed for the "Main Menu" profiling row). Hook to UI Buttons.
public class MainMenu : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene("Level1");
    public void Quit() => Application.Quit();
}
