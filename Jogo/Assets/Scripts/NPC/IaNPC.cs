using UnityEngine;

public enum Estado
{
    Andando,
    Pedindo,
    Esperando,
    Comendo,
    Reclamando,
    Saindo
}

public class IaNPC : MonoBehaviour
{
    public float velocidade = 3f;
    public float tempoComendo = 2.5f;
    public string comida;
    public UI_NPCManager UINPC;

    private Cadeira cadeiraAlvo;
    private Estado estadoAtual;
    private float timerComendo;
    private float timerDePaciencia;
    private CapsuleCollider2D colisor;


    void Start()
    {
        colisor = GetComponent<CapsuleCollider2D>();
        colisor.enabled = false;

        ProcurarCadeira();
    }

    void Update()
    {
        switch (estadoAtual)
        {
            case Estado.Andando:
                IndoCadeira();
                break;

            case Estado.Pedindo:
                break;

            case Estado.Esperando:
                EsperadoComida();
                break;

            case Estado.Comendo:
                break;

            case Estado.Reclamando:
                break;

            case Estado.Saindo:
                break;

            default:
                break;
        }
    }

    void ProcurarCadeira()
    {
        GameObject[] cadeiras = GameObject.FindGameObjectsWithTag("Cadeira");
        foreach (GameObject cadeira in cadeiras)
        {
            Cadeira status = cadeira.GetComponent<Cadeira>();

            if (!status.ocupado)
            {
                cadeiraAlvo = status;
                cadeiraAlvo.MudarOcupado();
                MudarEstado(Estado.Andando);
                return;
            }
        }
    }

    public void MudarEstado(Estado novoEstado)
    {
        if (estadoAtual == novoEstado) return;
        estadoAtual = novoEstado;
    }

    void IndoCadeira()
    {
        if (cadeiraAlvo == null) return;

        float distancia = Vector2.Distance(transform.position, cadeiraAlvo.transform.position);

        if (distancia <= 0.6)
        {
            transform.position = cadeiraAlvo.transform.position + (Vector3.up * 1.2f);
            MudarEstado(Estado.Pedindo);
            colisor.enabled = true;

            UINPC.telaUI.SetActive(true);
            UINPC.esperaUI.SetActive(true);
        }
        else
        {
            Vector2 direcao = (cadeiraAlvo.transform.position - transform.position).normalized;
            transform.Translate(direcao * velocidade * Time.deltaTime, Space.World);
        }
    }

    void EsperadoComida()
    {
        if (timerDePaciencia > 0)
        {
            timerDePaciencia -= Time.deltaTime;
        }
        else
        {
            MudarEstado(Estado.Saindo);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (estadoAtual)
                {
                    case Estado.Pedindo:
                        MudarEstado(Estado.Esperando);
                        UINPC.esperaUI.SetActive(false);
                        UINPC.pedidoUI.SetActive(true);
                        timerDePaciencia = 20f;

                        comida = "Lasanha";
                        break;

                    case Estado.Esperando:
                        
                        break;

                    default:
                        break;
                }

            }
        }
    }

}
