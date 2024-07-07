using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipesPrefab;
    [SerializeField] private GameObject circlePipesPrefab;
    [SerializeField] private GameObject verticalPipesPrefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float verticalGap = 3f;

    public void Repeater() {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void StopRepeat() {
        CancelInvoke(nameof(Spawn));
    }

    public void Stop() {
        StopAllCoroutines();
    }

    private void Spawn() {
        if (Random.value > 0.95) {
            GameObject circlePipes = Instantiate(circlePipesPrefab, transform.position + new Vector3(6, 1f, 0), Quaternion.identity);
            RestartSpawnCall();
        }

        else if (Random.value > 0.8) {
            GameObject verticalPipes = Instantiate(verticalPipesPrefab, transform.position, Quaternion.identity);
            verticalPipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            var VP = verticalPipes.GetComponent<Pipes>();
            VP.Gap = verticalGap;
        }

        else {
            GameObject pipes = Instantiate(pipesPrefab, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            var VP = pipes.GetComponent<Pipes>();
            VP.Gap = verticalGap;
        }
    }

    [ContextMenu("RestartSpawnCall")]
    public void RestartSpawnCall() {
        StartCoroutine(RestartSpawn());
    }

    private IEnumerator RestartSpawn() {
        StopRepeat();
		yield return new WaitForSeconds(5f);
        Repeater();
	}
}
