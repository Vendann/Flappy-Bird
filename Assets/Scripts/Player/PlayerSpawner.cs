using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private int characterID;
    private GameObject playerObject;
    private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool isFlappy;

    private void Awake() {
        characterID = PlayerPrefs.GetInt("Character");
        Instantiate(playerPrefabs[characterID], transform.position, Quaternion.identity);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        gameManager.Player = player;
        player.IsFlappy = isFlappy;
    }
}
