using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Diagnostics;
using UnityEngine;

using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //public ArmController arm;
    public GameObject CraftingUI;

    public float moveSpeed;

    private Rigidbody mRigidbody;

    public float maxHealth = 100;
    public float currentHealth = 0;

    public bool useController;

    private Vector3 jump;
    public float jumph, jumpForce;
    private bool isGrounded;

    PlayerInteraction playerInteraction;

    public Text txt;

    public AudioClip farm, field, battle, boss;
    public AudioSource source;
    public int tracks = 0;

    // Keyboard Variables

    private Vector3 moveInput;
    private Vector3 movement;

    // Mouse Variables

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        tracks = 1;
        source = gameObject.GetComponent<AudioSource>();
        source.clip = farm;
        source.Play();

        mRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        currentHealth = maxHealth;

        jump = new Vector3(0f, jumph, 0f);
        mRigidbody = GetComponent<Rigidbody>();

        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        

        txt.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // Keyboard Input

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, mRigidbody.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed);

        //moveVelocity = moveInput * moveSpeed;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            mRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            mRigidbody.AddForce(Input.GetAxisRaw("Horizontal") * 50, 0, Input.GetAxisRaw("Vertical") * 50, ForceMode.Impulse);
        }
        
        // Mouse Input

        float shoot = Input.GetAxis("Shoot");
        float interact = Input.GetAxis("Interact");
        //Debug.Log("Shoot: " + shoot);

        if (!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        if(useController == true)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        /*
        if(Input.GetButtonDown("Shoot") || shoot > 0 )
        {
            arm.isThrowing = true;             
        }

        if(Input.GetButtonUp("Shoot") || shoot <= 0)
        {
            arm.isThrowing = false;
        }
        */

        if (Input.GetButtonDown("Interact"))// || interact > 0)
        {
            playerInteraction.Interact();
            playerInteraction.ItemInteract();
            if(!CraftingUI.activeSelf)
            {
                playerInteraction.CraftingTableInteract();
            }
            else if(CraftingUI.activeSelf)
            {
                CraftingUI.SetActive(false);
            }
        }
    }
    void FixedUpdate()
    {
        mRigidbody.velocity = moveInput;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void BGM(int track)
    {
        if(track == 1)
        {
            tracks = 1;
            source.clip = farm;
            source.Play();
        }

        else if(track == 2)
        {
            tracks = 2;
            source.clip = field;
            source.Play();
        }

        else if (track == 3)
        {
            tracks = 3;
            source.clip = battle;
            source.Play();
        }

        else if (track == 4)
        {
            tracks = 4;
            source.clip = boss;
            source.Play();
        }
    }
}
