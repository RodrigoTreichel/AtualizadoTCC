﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //nave dos tres tiros
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedPowerupOn();
                }
                else if (powerupID == 2)
                {
                    player.ShieldOn();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
