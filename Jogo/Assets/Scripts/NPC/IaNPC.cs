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
    public AudioClip somPedido;
    public AudioClip somAgradecimento;
    [Range(0f, 1f)] public float volume = 1f;
    private AudioSource fonteAudio;

    private Cadeira cadeiraAlvo;
    private Estado estadoAtual;
    private float timerComendo;
    private float timerDePaciencia;
    private CapsuleCollider2D colisor;
    private bool podeInteragir;

    private SpriteRenderer spriteRenderer;
    float velocidadeFade = 0.5f;

    private DialogoManager dialogo;


    void Start()
    {
        colisor = GetComponent<CapsuleCollider2D>();
        colisor.enabled = false;
        cozinha = FindAnyObjectByType<Cozinha>();
        dialogo = FindAnyObjectByType<DialogoManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fonteAudio = gameObject.AddComponent<AudioSource>();
        fonteAudio.playOnAwake = false;
        fonteAudio.spatialBlend = 0f;

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
                Reclamando();
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

                transform.position = cadeiraAlvo.transform.position + (Vector3.up * 1.2f);

                if (spriteRenderer != null)
                {
                    Color cor = spriteRenderer.color;
                    cor.a = 0f;
                    spriteRenderer.color = cor;
                }
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

        Color corAtual = spriteRenderer.color;

        if (corAtual.a < 1f)
        {
            corAtual.a += velocidadeFade * Time.deltaTime;
            spriteRenderer.color = corAtual;
        }
        else
        {

            corAtual.a = 1f;
            spriteRenderer.color = corAtual;

            MudarEstado(Estado.Pedindo);
            colisor.enabled = true;

            if (somPedido != null) fonteAudio.PlayOneShot(somPedido, volume);

            UINPC.telaUI.SetActive(true);
            UINPC.esperaUI.SetActive(true);
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
                if (Movement.Instance.comidaMao != null && Movement.Instance.comidaMao.nome == comidaEscolhida.nome)
                {
                    Movement.Instance.comidaMao = null;
                    MudarEstado(Estado.Comendo);

                    if (somAgradecimento != null) fonteAudio.PlayOneShot(somAgradecimento, volume);

                    timerComendo = tempoComendo;
                    UINPC.telaUI.SetActive(false);
                    UINPC.pedidoUI.SetActive(false);
                }
            }
        }
        else
        {
            cadeiraAlvo.ocupado = false;
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
            if (dialogo.contador >= 3 && dialogo.TemDialogoDisponivel())
            {
                dialogo.contador = 0;
                UINPC.telaUI.SetActive(true);
                UINPC.reclamacaoUI.SetActive(true);
                MudarEstado(Estado.Reclamando);
            }
            else
            {
                cadeiraAlvo.ocupado = false;
                dialogo.contador++;
                MudarEstado(Estado.Saindo);
            }
        }
    }

    void Saindo()
    {
        Color corAtual = spriteRenderer.color;
        if (corAtual.a > 0f)
        {
            corAtual.a -= velocidadeFade * Time.deltaTime;
            spriteRenderer.color = corAtual;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    bool dialogExiste;
    void Reclamando()
    {
        if (podeInteragir && Input.GetKeyDown(KeyCode.E))
        {
            dialogo.IniciarDialogo();
            dialogExiste = true;
        }

        if (!dialogExiste) return;
        colisor.enabled = false;
        if (!dialogo.dialogoAtivo)
        {
            cadeiraAlvo.ocupado = false;
            MudarEstado(Estado.Saindo);
        }
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
