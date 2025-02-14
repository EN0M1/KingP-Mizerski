using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GoToCharacterSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
    }
}
