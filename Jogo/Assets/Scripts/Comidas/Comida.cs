using UnityEngine;

public class Comida : MonoBehaviour
{
    public OSComida comida;
    public bool podeInteragir;

    public SpriteRenderer spriteRenderer;

    public float tamanhoDesejado = 1f;
    void Update()
    {
        if (podeInteragir && Input.GetKeyDown(KeyCode.E))
        {
            if (Movement.Instance.comidaMao == null)
            {
                Movement.Instance.comidaMao = comida;
                Destroy(this.gameObject);
            }
        }
    }
    public void SetComida(OSComida comidaAtual)
    {
        comida = comidaAtual;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = comida.sprite;

        float larguraOriginal = spriteRenderer.sprite.bounds.size.x;
        float alturaOriginal = spriteRenderer.sprite.bounds.size.y;

        // Altera a escala do GameObject para forçar a imagem a ter o 'tamanhoDesejado'
        transform.localScale = new Vector3(
            tamanhoDesejado / larguraOriginal,
            tamanhoDesejado / alturaOriginal,
            1f);
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
