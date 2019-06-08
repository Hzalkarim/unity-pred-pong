using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDisk : MonoBehaviour
{
    public Vector3 initialDirection;
    public Server server;
    public float initialSpeed;
    public float initialAngularSpeed;
    public float goalDistance = 40f;
    public bool onlyBat = false;
    public bool arenaMultiplier = false;
    public float arenaStepUp = 0.1f;
    public int charge = 5;
    public int unleashCount = 3;

    public Transform startPoint;

    private Rigidbody rb;
    private Vector3 angularSpeed;
    private float currentMultiplier = 1;
    private int currentCharge = 0;
    private bool overCharging = false;
    private TrailRenderer[] trail;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angularSpeed = new Vector3(0, initialAngularSpeed, 0);
        trail = GetComponentsInChildren<TrailRenderer>();
        trail[1].emitting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetDisk(server, startPoint);
        }

    }

    private void LaunchDisk()
    {
        initialDirection = new Vector3(Random.Range(-2, 2), 0, 15);
        rb.velocity = (int)server * initialSpeed * initialDirection.normalized;
    }

    public void ResetDisk(Server whoServe, Transform myStartPoint)
    {
        server = whoServe;

        transform.position = myStartPoint.position;
        transform.rotation = myStartPoint.rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        LeashTheOvercharge();
        Invoke("LaunchDisk", 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (overCharging)
        {
            UnleashCharge();
            return;
        }

        if (currentCharge < charge && !overCharging)
        {
            currentCharge++;
            currentMultiplier += arenaStepUp;
        }
        else
        {
            overCharging = true;
            currentCharge = 0;
            Debug.Log("UNLEASHED!!");
            Time.timeScale = .6f;

            trail[0].emitting = false;
            trail[1].emitting = true;
        }

        if (!onlyBat || collision.gameObject.CompareTag("Bat"))
        {
            rb.velocity = initialSpeed * rb.velocity.normalized;
        }
    }

    private void UnleashCharge()
    {
        if (currentCharge < unleashCount)
        {
            currentCharge++;
            return;
        }
        
        rb.velocity = currentMultiplier * initialSpeed * rb.velocity.normalized;
        rb.angularVelocity = angularSpeed;

        currentCharge++;
        LeashTheOvercharge();

    }

    private void LeashTheOvercharge()
    {
        currentCharge = 0;
        currentMultiplier = 1;
        overCharging = false;
        Time.timeScale = 1f;

        trail[0].emitting = true;
        trail[1].emitting = false;
    }
}

public enum Server { Yellow = 1, Purlple = -1 }

