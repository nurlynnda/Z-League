using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private void Start()
    {

    }

    void Update()
    {
        transform.Translate(0, 0, 1 * speed * Time.deltaTime);
    }
}
