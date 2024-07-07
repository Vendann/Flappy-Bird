using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed = 0.04f;

    private void Awake() {
        if (Random.value > 0.5)
            speed *= -1;
    }

    private void Update() {
        transform.Rotate(new Vector3(0f, 0f, speed * Time.deltaTime));
    }
}
