using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeed = false;
    public bool shield = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _explosao_playe;

    [SerializeField]
    private GameObject _lazerPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldObject;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    //Variavel da velocidade
    [SerializeField]
    private float _speed= 5.0f;

    private UI _ui;

    // Start is called before the first frame update
    private void Start()
    {
        //inicio da posiçâo
        transform.position = new Vector3(0, 0, 0);
        _ui = GameObject.Find("Canvas").GetComponent<UI>();

        if(_ui != null)
        {
            _ui.UpdateLives(lives);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        Movimento();
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if(canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_lazerPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movimento()
    {
        //Movimentaçâo da nave
        float horizontalInput = Input.GetAxis("Horizontal"), verticalInput = Input.GetAxis("Vertical");

        if(isSpeed == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        

        //Limite de movimentação da nave no eixo y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //Limite de movimentação da nave no eixo x
        if (transform.position.x > 13.0f)
        {
            transform.position = new Vector3(-13.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -13.0f)
        {
            transform.position = new Vector3(13.0f, transform.position.y, 0);
        }
    }

    public void Damage()
    {

        if(shield == true)
        {
            shield = false;
            _shieldObject.SetActive(false); 
            return;
        }

        lives--;
        _ui.UpdateLives(lives);
        
        if(lives < 1)
        {
            Instantiate(_explosao_playe, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShootPowerDownRoutine());
    }

    public void SpeedPowerupOn()
    {
        isSpeed = true;
        StartCoroutine(SpeedPowerupDownRouine());
    }

    public void ShieldOn()
    {
        shield = true;
        StartCoroutine(ShieldDownRouine());
        _shieldObject.SetActive(true);
    }

    public IEnumerator TripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedPowerupDownRouine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeed = false;
    }

    public IEnumerator ShieldDownRouine()
    {
        yield return new WaitForSeconds(5.0f);
        shield = false;
    }

}
