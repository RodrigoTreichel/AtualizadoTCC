using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAI : MonoBehaviour
{

    [SerializeField]
    private GameObject _inimigo_explode_Prefabs;

    private float _speed = 5.0f;

    private UI _ui;

    // Start is called before the first frame update
    void Start()
    {
        _ui = GameObject.Find("Canvas").GetComponent<UI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7)
        {
            transform.position = new Vector3(Random.Range(-7, 7), 7);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Lazer")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(_inimigo_explode_Prefabs, transform.position, Quaternion.identity);
            _ui.UpdateScore();
            Destroy(this.gameObject);

        }
        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                Instantiate(_inimigo_explode_Prefabs, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                player.Damage();
            }
        }
        Instantiate(_inimigo_explode_Prefabs, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        
    }
}
