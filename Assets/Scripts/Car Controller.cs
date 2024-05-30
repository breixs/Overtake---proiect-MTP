using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    private float horizontal;
    private float currentAngle, currentBreak;
    private bool isBreaking, isAccelerating;
    public GameOverScreen gameOverscreen;
    public Smoke smoke;
    public Smoke exhaustLeft;
    public Smoke exhaustRight;
    public bool hit;
    public AudioSource hitAudio;
    public AudioSource crashAudio;
    public BrakeLight brakeLight;
    public BrakeLight brakeLightRight;
    private string filePath;
    int multiplier = 1, counter = 1;

    [SerializeField] private float defaultForce, motorForce, breakForce, maxSteerAngle;

    public float maxSpeed;

    [SerializeField] private WheelCollider FLwheelCollider, FRwheelCollider;
    [SerializeField] private WheelCollider RLwheelCollider, RRwheelCollider;

    [SerializeField] private Transform FLtransform, FRtransform;
    [SerializeField] private Transform RLtransform, RRtransform;


    private void GameOver()
    {
        crashAudio.Play();
        hitAudio.Play();
        gameOverscreen.Setup();
        smoke.Setup();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");

        isAccelerating = Input.GetKey(KeyCode.W);

        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void Motor()
    {
        var Speed =GetComponent<Rigidbody>().velocity;
        //var maxSpeed = new Vector3(Speed.x, Speed.y, 10);
        //motorForce = defaultForce;

        //Debug.Log(Speed);

        if (Speed.z>9)
        {
            motorForce = 0;
        }
        else if(Speed.z<9)
        {
            motorForce = defaultForce;
        }
        FLwheelCollider.motorTorque = motorForce;
        FRwheelCollider.motorTorque = motorForce;

        if (isAccelerating)
        {
            exhaustLeft.ExhaustStart();
            exhaustRight.ExhaustStart();
            if (Speed.z > maxSpeed-1)
            {
                motorForce = 0;
            }
            else if (Speed.z < maxSpeed-1)
            {
                motorForce = defaultForce;
            }
            FLwheelCollider.motorTorque = motorForce*2;
            FRwheelCollider.motorTorque = motorForce*2;
        }
        else
        {
            exhaustLeft.ExhaustStop();
            exhaustRight.ExhaustStop();
        }

        if (isBreaking)
        {
           // brakeLight.Setup();
           // brakeLightRight.Setup();
            currentBreak = breakForce;
        }
        else
        {
           // brakeLight.Stop();
           // brakeLightRight.Stop();
            currentBreak = 0f;
        }
        
    }

    private void Break()
    {
        FRwheelCollider.brakeTorque = currentBreak;
        FLwheelCollider.brakeTorque = currentBreak;
        RLwheelCollider.brakeTorque = currentBreak;
        RRwheelCollider.brakeTorque= currentBreak;
    }

    private void Steer()
    {
        currentAngle = maxSteerAngle * horizontal;
        FLwheelCollider.steerAngle = currentAngle;
        FRwheelCollider.steerAngle = currentAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWhell(FLwheelCollider, FLtransform);
        UpdateSingleWhell(FRwheelCollider, FRtransform);
        UpdateSingleWhell(RLwheelCollider, RRtransform);
        UpdateSingleWhell(RRwheelCollider, RRtransform);
    }

    private void UpdateSingleWhell(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.rotation = rot;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        if (!hit)
        {
            GetInput();
            Motor();
            Steer();
            Break();
            UpdateWheels();
            checkUpsiedDown();
        }
    }

    private void Start()
    {
        filePath = Application.dataPath + "/TxtData/CarData.txt";
        getData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Npc") || other.gameObject.CompareTag("NpcIncoming"))
        {
            if (!hit)
            {
                GameOver();
                hit = true;
                motorForce = 0;
                FLwheelCollider.motorTorque = motorForce;
                FRwheelCollider.motorTorque = motorForce;
            }
        }

        if(other.gameObject.CompareTag("TriggerOvertake"))
        {
            if (!hit)
            {
                if(counter==5)
                {
                    Debug.Log("powerup");
                    randomPowerup();
                    counter = 1;
                }
                else
                counter++;

                Debug.Log("overtake");
                other.enabled = false;
                Destroy(other.gameObject);
                ScoreManager.instance.AddPoint(multiplier);
            }
        }
    }

    private void checkUpsiedDown()
    {
        if (gameObject.transform.rotation.x<-1)
           {
            Debug.Log("upside down");
            hit = true;
        }
    }

    private void getData()
    {
        string[] lines = File.ReadAllLines(filePath);
        string[] data = lines[0].Split(',');
        maxSpeed = Convert.ToInt32(data[0]);
        defaultForce = Convert.ToInt32(data[1]);
        breakForce=Convert.ToInt32(data[2]);


        if (maxSpeed < 20)
            maxSpeed = 20;
        if (defaultForce < 900)
            defaultForce = 900;
        if (breakForce < 9000)
            breakForce = 9000;
    }

    private void randomPowerup()
    {
        int rnd = Random.Range(1, 7);

        switch(rnd)
        {
            case 1:
                Debug.Log("acceleration");
                defaultForce += 0.10f * defaultForce;
                ScoreManager.instance.activateText(1);
                break;
            case 2:
                Debug.Log("multiplier");
                ScoreManager.instance.activateText(2);
                if (multiplier <= 6)
                    multiplier+=1;
                break;
            case 3:
                Debug.Log("5 overtakes");
                ScoreManager.instance.activateText(3);
                ScoreManager.instance.AddPoint(5);
                break;
            case 4:
                Debug.Log("brakes");
                ScoreManager.instance.activateText(4);
                breakForce = breakForce + 2000;
                break;
            case 5:
                Debug.Log("1 overtake");
                ScoreManager.instance.activateText(5);
                ScoreManager.instance.AddPoint(1);
                break;
            case 6:
                Debug.Log("the purge");
                ScoreManager.instance.activateText(6);
                NPCBehaviour.instance.Purge();
                break;
            case 7:
                break;

        }
    }
}
