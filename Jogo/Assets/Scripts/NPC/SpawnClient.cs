using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    public GameObject clientePrefab;

    void Start()
    {
        InvokeRepeating("Spawn", 2f, 4f);
    }

    void Spawn()
    {
        GameObject[] cadeiras = GameObject.FindGameObjectsWithTag("Cadeira");
        foreach (GameObject cadeira in cadeiras)
        {
            Cadeira status = cadeira.GetComponent<Cadeira>();
            if (!status.ocupado)
            {
                Instantiate(clientePrefab, transform.position, Quaternion.identity);
                return;
            }
        }

    }
}
