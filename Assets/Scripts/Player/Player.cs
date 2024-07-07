using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioScore;
    [SerializeField] private float strength;
    [SerializeField] private Rigidbody2D rb;
    private event Action Click;
    private event Action Physics;
    [SerializeField] private bool isFlappy;
    public bool IsFlappy { set { isFlappy = value; } }
    [SerializeField] private float inputRocket;
    [SerializeField] private bool inputFlappy;

    private void Awake() {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable() {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
        rb.velocity = new Vector2(0, 0);

        if (isFlappy) {
            Click += InputFlappy;
            Physics += MoveFlappy;
            strength = 6f;
            rb.gravityScale = 2.5f;
        }
        else {
            Click += InputRocket;
            Physics += MoveRocket;
            strength = 40f;
            rb.gravityScale = 2f;
        }
    }

    private void OnDisable() {
        if (isFlappy) {
            Click -= InputFlappy;
            Physics -= MoveFlappy;
        }
        else {
            Click -= InputRocket;
            Physics -= MoveRocket;
        }
    }

    private void Update() {
        Click?.Invoke();
        //Rotate();
    }

    private void FixedUpdate() {
        Physics?.Invoke();
    }

    private void InputFlappy() {
        if (Input.GetButtonDown("Fire1")) inputFlappy = true;
    }

    private void InputRocket() {
        inputRocket = Input.GetAxis("Fire1");
    }

    private void MoveFlappy() {
        if (inputFlappy) {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * strength, ForceMode2D.Impulse);
            inputFlappy = false;
        }
    }

    private void MoveRocket() {
        if (inputRocket > 0)
            rb.AddForce(Vector2.up * inputRocket * strength, ForceMode2D.Force);
    }

    private void AnimateSprite() {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            audioScore.PlayOneShot(audioScore.clip);
            GameManager.Instance.IncreaseScore();
        }
    }
}
