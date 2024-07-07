using UnityEngine;

public class CirclePipes : MonoBehaviour
{
    [SerializeField] private PipesSpeed pipesSpeed;
    //[SerializeField] private PipeSpawner pipeSpawner;

    private float leftEdge;

    private void Start() {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 6f;
        //pipeSpawner = FindObjectOfType<PipeSpawner>();
        //pipeSpawner.gameObject.SetActive(false);
    }

    private void Update() {
        transform.position += pipesSpeed.Speed * Time.deltaTime * Vector3.left;
        
        //if (transform.position.x < leftEdge + 5f)
            //pipeSpawner.gameObject.SetActive(true);

        if (transform.position.x < leftEdge)
            Destroy(gameObject);
    }
}
