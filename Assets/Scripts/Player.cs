using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShotActive;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private bool _shieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _tripleShotActive = false;
        _shieldActive = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        FireLaser();
    }

    void calculateMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //transform.Translate(new Vector3(1, 0, 0) * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction;
        
        direction = new Vector3(horizontalInput, verticalInput, 0);

        if(transform.position.x <= -9f) {
            transform.position = new Vector3(-9f, transform.position.y, 0);
        } else if (transform.position.x >= 9f) {
            transform.position = new Vector3(9f, transform.position.y, 0);
        }

        if(transform.position.y <= -3.5f) {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        } else if (transform.position.y >= 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        transform.Translate(direction * _speed * Time.deltaTime);

    }

    public void Damage() {
        
        if(_shieldActive == true) {
            return;
        }

        _lives -= 1;

        if(_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    void FireLaser() {
        if (Input.GetKeyDown("space") && Time.time > _nextFire) {

            _nextFire = Time.time + _fireRate;

            if(_tripleShotActive == true) {
                Instantiate(_tripleLaserPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            } else {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
            }
        }
    }

    public void playerShieldActive() {
        transform.GetChild(0).gameObject.SetActive(true);
        _shieldActive = true;
        StartCoroutine(playerShieldRoutine());
    }

    IEnumerator playerShieldRoutine() {
        yield return new WaitForSeconds(5.0f);
        _shieldActive = false;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return null;
    }

    public void TripleShotActive() {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void playerSpeedBoostActive() {
        _speed *= _speedMultiplier;
        StartCoroutine(playerSpeedBoostPowerupRoutine());
    }

    IEnumerator playerSpeedBoostPowerupRoutine() {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        yield return null;
    }
}
