using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnder : MonoBehaviour
{
    public float timer;
    public string targetScene;

    private void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(targetScene);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0.1f;
        }
    }
}
