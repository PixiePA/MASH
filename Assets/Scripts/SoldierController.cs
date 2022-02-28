using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    // Start is called before the first frame update
    public MapGeneratorScript MapGen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayerController playerComponent = collision.gameObject.GetComponent<PlayerController>();
            if (playerComponent.passengers < 3)
            {
                Destroy(this.gameObject);
                playerComponent.passengers++;
            }
            Debug.Log(playerComponent.passengers);

        }
    }

    private void OnDestroy()
    {
        MapGen.SpawnedSoldiers.Remove(this.gameObject);
    }
}
