using UnityEngine;
using UnityEngine.SceneManagement;

public class asd : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
    }
}
