using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(LoadStarterScene());
    }

    private IEnumerator LoadStarterScene()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        
        yield return wait;
        SceneManager.LoadScene("StarterScene");
    }
}
