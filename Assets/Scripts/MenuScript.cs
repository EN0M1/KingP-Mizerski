using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public void GoToGame()
    {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public void GoToMenu()
    {
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void GoToCharacterSelection()
    {
        StartCoroutine(WaitForSoundAndTransition("CharacterSelection"));
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
