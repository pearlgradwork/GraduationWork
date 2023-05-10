using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public bool doublePoints;
    public bool safeMode;

    public float powerupLength;

    private PowerUpManager thePowerUpManager;

    public Sprite[] powerUpSprites;

    // Start is called before the first frame update
    void Start()
    {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
    }

    private void Awake()
    {
        int powerUpSelector = Random.Range(0, 2);

        switch(powerUpSelector)
        {
            case 0: doublePoints = true; break;
            case 1: safeMode = true; break;
        }

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(doublePoints, safeMode, powerupLength);
        } 
       
       gameObject.SetActive(false);
    }
}
