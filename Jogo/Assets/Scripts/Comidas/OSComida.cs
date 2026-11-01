using UnityEngine;

[CreateAssetMenu(fileName = "OSComida", menuName = "ComidaOS/Comida")]
public class OSComida : ScriptableObject
{
    public string nome;
    public Texture texture;
    public Sprite sprite;
}
