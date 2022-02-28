using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Listener
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    public int passengers;
    [SerializeField] int maxPassengers;
    [SerializeField] float speed;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }
    void Start()
    {
        GameManager.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal, vertical).normalized * new Vector2(horizontal, vertical).magnitude * speed;
    }

    public override void GameStarted()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void GameEnded()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void GameReset()
    {
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Finish"))
        {
            GameManager.score += passengers;
            passengers = 0;
            Debug.Log("Score:" + GameManager.score + " Passengers:" + passengers);

            GameManager.MapReset();
        }
    }
}
