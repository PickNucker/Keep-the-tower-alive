using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float turnSpeed = 0.1f;
    [SerializeField] float gravity = -30;
    [SerializeField] float gravityMultiplikator = 10f;
    [Space]
    [SerializeField] AudioTrigger engineSFX;
    public Transform hitPlace;
   // [SerializeField] GameObject moveDirObject;

    Animator anim;
    Vector3 worldPos;
    Vector3 velocity;
    CharacterController cc;

    bool isRunning;
    public bool isDead;

    float currentVelocity;

    void Awake()
    {
        instance = this;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isDead = false;
        engineSFX.Play(Vector3.zero, this.gameObject.transform);
    }

    void Update()
    {
        if (isDead) return;
        UpdateAnimation();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;

        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPos = ray.GetPoint(distance);
        }

        transform.LookAt(worldPos);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);

        if (dir.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            cc.Move(moveDir * Time.deltaTime * movementSpeed);
        }

        velocity.y = Mathf.Max(velocity.y - Time.deltaTime * gravityMultiplikator, cc.isGrounded ? -1 : gravity);

        cc.Move(velocity * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
    }
}
