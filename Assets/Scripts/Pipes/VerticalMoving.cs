using UnityEngine;

public class VerticalMoving : MonoBehaviour
{
    [SerializeField] private PipesSpeed pipesSpeed;
    [SerializeField] private float highPosition = 3f;
    [SerializeField] private float lowPosition = -3f;
    [SerializeField] private bool UpMove = false;

    private void Awake() {
        if (Random.value > 0.5)
            UpMove = true;
    }

    private void Update()
    {
        Vector3 dir = transform.up;

        if (transform.position.y > highPosition)
            UpMove = false;
        else if (transform.position.y < lowPosition)
            UpMove = true;

        if (UpMove)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, pipesSpeed.Speed / 3 * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, transform.position - dir, pipesSpeed.Speed / 3 * Time.deltaTime);
    }
}
