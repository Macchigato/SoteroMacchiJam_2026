using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
    public OSComida comidaEscolhida;
    public float velocidade = 3f;
    public float tempoComendo = 2.5f;
    public UI_NPCManager UINPC;
    public Cozinha cozinha;

    private Cadeira cadeiraAlvo;
    private Estado estadoAtual;
    private float timerComendo;
    private float timerDePaciencia;
    private CapsuleCollider2D colisor;
    private bool podeInteragir;


    void Start()
    {
        colisor = GetComponent<CapsuleCollider2D>();
        colisor.enabled = false;
        cozinha = FindAnyObjectByType<Cozinha>();

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
                AnotarPedido();
                break;

            case Estado.Esperando:
                EsperadoComida();
                break;

            case Estado.Comendo:
                Comendo();
                break;

            case Estado.Reclamando:
                break;

            case Estado.Saindo:
                Saindo();
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

    void AnotarPedido()
    {
        if (podeInteragir && Input.GetKeyDown(KeyCode.E))
        {
            MudarEstado(Estado.Esperando);
            UINPC.esperaUI.SetActive(false);
            UINPC.pedidoUI.SetActive(true);
            timerDePaciencia = 20f;

            comidaEscolhida = CookManager.Instance.ComidaAleatoria();
            RawImage image = UINPC.pedidoUI.GetComponent<RawImage>();
            image.texture = comidaEscolhida.texture;

            cozinha.PrepararComida(comidaEscolhida);
        }
    }

    void EsperadoComida()
    {
        if (timerDePaciencia > 0)
        {
            timerDePaciencia -= Time.deltaTime;

            if (podeInteragir && Input.GetKeyDown(KeyCode.E))
            {
                if (Movement.Instance.comidaMao.nome == comidaEscolhida.nome)
                {
                    Movement.Instance.comidaMao = null;
                    MudarEstado(Estado.Comendo);
                    timerComendo = tempoComendo;
                    UINPC.telaUI.SetActive(false);
                }
            }
        }
        else
        {
            UINPC.telaUI.SetActive(false);
            MudarEstado(Estado.Saindo);
        }
    }

    void Comendo()
    {
        if (timerComendo > 0)
        {
            timerComendo -= Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + (Vector3.down * 1.2f);
            cadeiraAlvo.ocupado = false;
            MudarEstado(Estado.Saindo);
        }
    }

    void Saindo()
    {

        transform.Translate(Vector3.right * velocidade * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            podeInteragir = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            podeInteragir = false;
        }
    }
}
