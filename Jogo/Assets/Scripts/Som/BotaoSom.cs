using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotaoSom : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip somHover;
    [SerializeField] private AudioClip somClique;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private AudioSource fonte;
    private Button botao;

    private void Awake()
    {
        botao = GetComponent<Button>();
        fonte = gameObject.AddComponent<AudioSource>();
        fonte.playOnAwake = false;
        fonte.spatialBlend = 0f; // 2D
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (botao.interactable && somHover != null)
            fonte.PlayOneShot(somHover, volume);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (botao.interactable && somClique != null)
            fonte.PlayOneShot(somClique, volume);
    }
}