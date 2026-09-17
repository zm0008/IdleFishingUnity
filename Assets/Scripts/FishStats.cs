using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStats : MonoBehaviour
{
    float fishHealth = 100f;
    float hookDamage = 25f; // Get from upgrades when M has coded them
    float currentHealth;
    float exampleVar1 = 0.0f;// Replace with real variables during final buidl
    float exampleVar2 = 1.0f;
    private GameObject Hook;
    private FishInventory fishInventory;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1.53f, -8.39f, 0); // Temporary starting position while M works on fish spawning
        currentHealth = fishHealth;
        fishInventory = FindObjectOfType<FishInventory>();
        Debug.Log("FishInventory found: " + fishInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            //Attach to hook until hook i
            transform.position = new Vector3(Hook.transform.position.x, Hook.transform.position.y, 0.0f);
            if (transform.position == new Vector3(0.0f, 0.0f, 0.0f))
            {
                fishInventory.FishCaught(exampleVar1);
                Debug.Log("called fish caught");
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHook>() && currentHealth > 0)
        {
            Hook = collision.gameObject;
            currentHealth -= hookDamage;
            Debug.Log("Fish Health:" + currentHealth);
        }
    }
}