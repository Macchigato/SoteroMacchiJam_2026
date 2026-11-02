using UnityEngine;

public class camerasegue : MonoBehaviour
{


    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.FloorToInt((player.transform.position.x + 10.0f) / 20) * 20,0.0f ,-10.0f);
    }
}
