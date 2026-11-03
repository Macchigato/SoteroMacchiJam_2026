using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    public GameObject creditos;

    void Start()
    {
        creditos.SetActive(false);
    }
    public void AbrirFecharTela(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void NovaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
