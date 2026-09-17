using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHook : MonoBehaviour

{
    private float LineLength = 30;
    private float RemainingLine;
    private bool IsFishing = false;
    private PlayerController controls;
    private Rigidbody2D rb;
    private float vertical_thrust = 0.5f;
    private float horizontal_thrust = 0.7f;
    private float MaximumCapacity = 3;

    [HideInInspector]
    public float RemainingCapacity;

    //private bool returning = false;
    public bool returnCheck1 = false;


    private void Awake()
    {
        controls = new PlayerController();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    public void Start()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        RemainingLine = LineLength;
        RemainingCapacity = MaximumCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFishing == false && controls.Hook.Cast.IsPressed() && returnCheck1 == false)
        {
            IsFishing = true;
            RemainingLine = LineLength;
            Debug.Log("Hook Cast");
        }
        if (RemainingLine <= 0)
        {
            IsFishing = false;
            if (returnCheck1 == false)
            {
                returnCheck1 = true;
            }
            //Debug.Log("Stopped Fishing");
        }
        //if (returning == true && transform.position.y <= 3.0f && returnCheck1 == false)
        //{
        //    rb.AddForce(transform.up * thrust * 10);
        //    Debug.Log("Hook Returning");
        //}
        //if (returning == true && transform.position.y >= 3.0f && returnCheck1 == false)
        //{
        //    returnCheck1 = true;
        //}
        if (returnCheck1 == true)
        {
            if (transform.position == new Vector3(0.0f, 0.0f, 0.0f))
            {
                //returning = false;
                returnCheck1 = false;
                Debug.Log("Hook Returned");
                RemainingLine = LineLength;
                RemainingCapacity = MaximumCapacity;
            }
            if (transform.position.y >= -1.0f && transform.position.y <= 1.0f && transform.position.x >= -1.0f && transform.position.x <= 1.0f)
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(0.0f, 0.0f, 0.0f);

                //returning = false;
                returnCheck1 = false;
                RemainingLine = LineLength;
            }
            else
            {
                Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
                Vector3 direction = target - transform.position;
                rb.AddForce(direction * vertical_thrust * 0.3f);
            }
            if (transform.position.y > 0)
            {
                vertical_thrust = 10f;
                Debug.Log("0.25");
            }
            else
            {
                vertical_thrust = 0.5f;
            }

        }
        /*if (!IsFishing && transform.position != new Vector3(transform.position.x, transform.position.y, 0.0f))
        {
            if (returning == false)
            {
                rb.AddForce(transform.up * thrust * 10);
                returning = true;
                Debug.Log("Hook Retracted");
            }
            else if (returning == true && transform.position == new Vector3(0.0f, 0.0f, 0.0f))
            {
                returning = false;
            }
        }*/

        if (IsFishing)
        {
            Movement();
        }

    }
    private void Movement()
    {
        if (RemainingLine > 0)
        {
            IsFishing = true;
            rb.AddForce(transform.up * vertical_thrust * -1);
            //Debug.Log("Moved");
            RemainingLine -= 0.005f;
            //Debug.Log(RemainingLine);
            float movement = controls.Hook.Move.ReadValue<float>();
            if (movement < 0)
            {
                rb.AddForce(transform.right * movement * horizontal_thrust);
            }
            else if (movement > 0)
            {
                rb.AddForce(transform.right * movement * horizontal_thrust);
            }

        }

    }

}