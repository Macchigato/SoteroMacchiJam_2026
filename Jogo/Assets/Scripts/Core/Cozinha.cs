using System.Collections.Generic;
using UnityEngine;

public class Cozinha : MonoBehaviour
{
    public List<OSComida> listaDeComidas;
    public GameObject comidaPrefab;
    public GameObject comidaCriada;
    public Transform aparecerComida;
    float tempoPreparacao = 0f;
    bool podePreparar;

    void Update()
    {

        if (!podePreparar) return;

        if (tempoPreparacao > 0)
        {
            tempoPreparacao -= Time.deltaTime;
        }
        else
        {
            comidaCriada = Instantiate(comidaPrefab, aparecerComida.position, Quaternion.identity);
            Comida comidaAtual = comidaCriada.GetComponent<Comida>();
            comidaAtual.SetComida(listaDeComidas[0]);

            listaDeComidas.RemoveAt(0);

            if (listaDeComidas.Count > 0)
            {
                tempoPreparacao = 5f;
            } else
            {
                podePreparar = false;
            }
        }

    }

    public void PrepararComida(OSComida comida)
    {
        if (tempoPreparacao <= 0)
        {
            tempoPreparacao = 5f;
        }
        podePreparar = true;

        listaDeComidas.Add(comida);

    }
}
