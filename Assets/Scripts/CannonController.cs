using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float BlastPower = 5;
    public GameObject Cannonball;
    public Transform ShotPoint;
    public float YRotationMax = 45f;
    public float YRotationMin = -45f;
    public float XRotationMax = 30f;
    public float XRotationMin = 0f;
    public AudioSource shoot;
    public float mouseZDist = 10f;
    private Vector3 mousePos;
    private Camera myMainCam;
    private Transform localTrans;

    private void Start()
    {
        shoot = GetComponent<AudioSource>();
        localTrans = GetComponent<Transform>();
        myMainCam=Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = myMainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouseZDist));
            Quaternion targetRot = Quaternion.LookRotation(mousePos - localTrans.position);
            localTrans.rotation = Quaternion.Slerp(localTrans.rotation, targetRot, rotationSpeed * Time.deltaTime);
            YLimitRotation();
            ZLimitRotation();
        }
        // float HorizontalRotation = Input.GetAxis("Horizontal");
        // float VericalRotation = Input.GetAxis("Vertical");
        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        // new Vector3(-VericalRotation * rotationSpeed, HorizontalRotation * rotationSpeed, 0));
        
       

        if (Input.GetMouseButtonUp(0))
        {
            GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
            shoot.Play();
            // Added explosion for added effect
            // Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
            // Shake the screen for added effect
            Screenshake.ShakeAmount = 0.5f;

        }
    }

    private void YLimitRotation()
    {
        Vector3 cannonYLimitRotation = localTrans.rotation.eulerAngles;
        cannonYLimitRotation.y = (cannonYLimitRotation.y > 180) ? cannonYLimitRotation.y - 360 : cannonYLimitRotation.y;
        cannonYLimitRotation.y = Mathf.Clamp(cannonYLimitRotation.y, YRotationMin, YRotationMax);
        localTrans.rotation = Quaternion.Euler(cannonYLimitRotation);
    }
    private void ZLimitRotation()
    {
        Vector3 cannonXLimitRotation = localTrans.rotation.eulerAngles;
        cannonXLimitRotation.x = (cannonXLimitRotation.x > 180) ? cannonXLimitRotation.x - 360 : cannonXLimitRotation.x;
        cannonXLimitRotation.x = Mathf.Clamp(cannonXLimitRotation.x, XRotationMin, XRotationMax);
        localTrans.rotation = Quaternion.Euler(cannonXLimitRotation);
    }

}