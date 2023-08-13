using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private bool hasBeenUsed = false;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                Destroy(gameObject);
                Debug.Log("HealthPickup destroyed");
            }
            else
            {
                Debug.Log("Healing not possible, HealthPickup not destroyed");
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}