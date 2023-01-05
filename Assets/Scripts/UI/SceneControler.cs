using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    public static void SetScene(int id = 1)
    {
        SceneManager.LoadScene(id);
    }
    public void SetScesne(int id)
    {
        SceneManager.LoadScene(id);
    }
}