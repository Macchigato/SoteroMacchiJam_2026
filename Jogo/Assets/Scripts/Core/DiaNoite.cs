using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaNoite : MonoBehaviour
{
    public int pessoasAtendidas = 0;
    public int pistasColetadas = 0;

    [Header("Referências dos Cenários")]
    public SpriteRenderer cenarioDia;
    public SpriteRenderer cenarioNoite;

    [Header("Configurações do Tempo")]
    public float duracaoDoDia = 30f; // Tempo em segundos até começar a mudar para noite
    public float velocidadeTransicao = 1f; // Velocidade da troca de transparência

    public bool chegouNoite;
    public string proximaCena;
    private float timerTempo;
    private bool estaMudando;
    private bool eNoite;

    void Start()
    {
        // Garante que o Dia comece visível e a Noite invisível
        if (cenarioDia != null && cenarioNoite != null)
        {
            DefinirAlpha(cenarioDia, 1f);
            DefinirAlpha(cenarioNoite, 0f);
        }
    }

    void Update()
    {
        if (!estaMudando)
        {
            timerTempo += Time.deltaTime;

            if (timerTempo >= duracaoDoDia)
            {
                estaMudando = true;
                timerTempo = 0f;
            }
        }
        else
        {
            ProcessarTransicao();
        }

        if (chegouNoite)
        {
            TrocarFase();
        }
    }

    public void TrocarFase()
    {
        if (VerificarCadeiras())
        {
            SceneManager.LoadScene(proximaCena);
        }
    }

    bool VerificarCadeiras()
    {
        int contador = 0;
        GameObject[] cadeiras = GameObject.FindGameObjectsWithTag("Cadeira");

        foreach (GameObject cadeira in cadeiras)
        {
            Cadeira status = cadeira.GetComponent<Cadeira>();

            if (!status.ocupado)
            {
                contador++;
            }
        }
      
        if (contador == cadeiras.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ProcessarTransicao()
    {
        float alvoAlphaDia = eNoite ? 1f : 0f;
        float alvoAlphaNoite = eNoite ? 0f : 1f;

        float novoAlphaDia = Mathf.MoveTowards(cenarioDia.color.a, alvoAlphaDia, velocidadeTransicao * Time.deltaTime);
        float novoAlphaNoite = Mathf.MoveTowards(cenarioNoite.color.a, alvoAlphaNoite, velocidadeTransicao * Time.deltaTime);

        DefinirAlpha(cenarioDia, novoAlphaDia);
        DefinirAlpha(cenarioNoite, novoAlphaNoite);

        if (Mathf.Approximately(novoAlphaNoite, alvoAlphaNoite))
        {
            estaMudando = false;
            chegouNoite = true;
            eNoite = !eNoite;
        }
    }

    void DefinirAlpha(SpriteRenderer sprite, float alpha)
    {
        Color cor = sprite.color;
        cor.a = alpha;
        sprite.color = cor;
    }

}
