using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _powerupID;
    // ID for PowerUp
    // 0 for powerups
    // 1 = speed
    // 2 = shields
    [SerializeField]
    private float _speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6.0f) {
            Destroy(this.gameObject);
        }
        // move down at the speed of 3
        // when we leave the screen, destroy object
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player") {
            Player player = other.transform.gameObject.GetComponent<Player>();
            if (player != null) {
                if(_powerupID == 0) {
                    player.TripleShotActive();
                }
                if(_powerupID == 1) {
                    player.playerSpeedBoostActive();
                }
                if(_powerupID == 2) {
                    player.playerShieldActive();
                }
            }
            Destroy(this.gameObject);
        }
    }
    //Ontriggercollison2d
    // obnly be collectable by the player (use tag)
}
