using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -5.50f) {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7.5f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        // laser

        if(other.transform.tag == "Laser") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        // Player

        if(other.transform.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();

            if(player != null) {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        
        }
    }
}
