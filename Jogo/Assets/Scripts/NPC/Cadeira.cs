using UnityEngine;

public class Cadeira : MonoBehaviour
{
    public bool ocupado = false;

    public void MudarOcupado()
    {
        ocupado = !ocupado;
    }

}
