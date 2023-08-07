using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CamController : MonoBehaviour
{
    CharacherController characterController;

    public GameObject bulletPrefab;

    public GameObject spawnPosition;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float camSens = Constants.CAM_SENSIVITY;

    [SerializeField]
    private float crouchHeight;

    public float startYscale;

    float mousex;

    [SerializeField]
    Transform headPos = null;

    [SerializeField]
    Transform Cam = null;

    Rigidbody rb;

    //clampRotation: max angle that mouse can reach in vertically
    float clampRotation = Constants.CLAMP_ROTATION;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startYscale = transform.localScale.y;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacherController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LookAround();
        Crouch();
        Fire();
    }

    private void FixedUpdate()
    {
        // player movement depends on the inputs
        if (characterController.GetMoveAxis().magnitude > 0)
        {
            rb.velocity =
                transform.forward *
                characterController.GetMoveAxis().y *
                speed +
                transform.right * characterController.GetMoveAxis().x * speed +
                transform.up * rb.velocity.y;
        }
        else if (rb.velocity.z != 0 || rb.velocity.x != 0)
        {
            rb.velocity = Vector3.up * rb.velocity.y;
            rb.velocity.Normalize();
        }
    }

    // Camera position with mouse
    void LookAround()
    {
        Cam.position = headPos.position;
        mousex -= characterController.GetMouseAxis().y;
        Cam.localRotation =
            Quaternion
                .Euler(Mathf
                    .Clamp(mousex * camSens, -clampRotation, clampRotation),
                0,
                0);

        transform
            .Rotate(transform.up,
            characterController.GetMouseAxis().x * camSens);
    }

    // Crouching with keyboard input
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale =
                new Vector3(transform.localScale.x,
                    crouchHeight,
                    transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale =
                new Vector3(transform.localScale.x,
                    startYscale,
                    transform.localScale.z);
        }
    }

    // Firing bullet with mouse input
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject =
                Instantiate(bulletPrefab,
                spawnPosition.transform.position,
                Cam.transform.rotation);
        }
    }
}
