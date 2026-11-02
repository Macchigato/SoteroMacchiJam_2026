using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance { get; private set; }
    public OSComida[] oSComidas;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;

    }

    public OSComida ComidaAleatoria()
    {
        return oSComidas[Random.Range(0, oSComidas.Length)];
    }

    

}
