using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string sceneToLoad; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // make sure your player has tag "Player"
        {
            SceneManager.LoadScene("BossStage");
        }
    }
}
