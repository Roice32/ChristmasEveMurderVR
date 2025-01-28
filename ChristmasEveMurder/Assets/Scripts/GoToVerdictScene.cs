using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToVerdictScene : MonoBehaviour
{
    public void GoToVerdict()
    {
        SceneManager.LoadScene("Verdict", LoadSceneMode.Single);
    }
}
