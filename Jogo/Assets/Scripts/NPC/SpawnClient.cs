using UnityEditor.Animations;
using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    public GameObject clientePrefab;
    public AnimatorController[] animacoes;

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
                GameObject client = Instantiate(clientePrefab, transform.position, Quaternion.identity);
                Animator animator = client.GetComponent<Animator>();
                animator.runtimeAnimatorController = animacoes[Random.Range(0,animacoes.Length)];
                return;
            }
        }

    }
}
