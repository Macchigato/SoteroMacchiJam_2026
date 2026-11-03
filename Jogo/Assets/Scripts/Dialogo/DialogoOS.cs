using UnityEngine;

[CreateAssetMenu(fileName = "DialogoOS", menuName = "DialogoOS/FalaNPC")]
public class DialogoOS : ScriptableObject
{
    public Sprite imagem;
    [TextArea]
    public string texto;
}
