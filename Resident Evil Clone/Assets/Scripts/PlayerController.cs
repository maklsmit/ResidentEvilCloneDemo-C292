using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Transform dropPoint;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float mouseSensitivity = 5f;

    [SerializeField] float verticalLookLimit;
    [SerializeField] Transform fpsCamera;

    [SerializeField] Transform firePoint;
    
    private bool isGrounded;
    private float xRot;
    private Rigidbody rb;

    private Magazine currentMag;
    public Magazine CurrentMag {get => currentMag; set => currentMag = value;}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAround();

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            float distance = 100f;

            Debug.DrawRay(fpsCamera.position, fpsCamera.forward * distance, Color.red, 5f);
            if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out RaycastHit hit, distance)){
                
                if(hit.transform.TryGetComponent(out Magazine magazine)){
                    Debug.Log("Magazine");
                    magazine.OnPickup(this);
                    Debug.Log(currentMag);
                    weapon.CurrentMag = currentMag;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)){
            currentMag.OnDrop(dropPoint);
        }
    }

    private void LookAround(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -verticalLookLimit, verticalLookLimit);

        fpsCamera.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void Move(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }

    private void Jump(){
        if(isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    // private void Shoot(int damage){
    //     RaycastHit hit;
        
    //     if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100)){
    //         Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
    //         if(hit.transform.CompareTag("Zombie")){
    //             hit.transform.GetComponent<Zombie>().TakeDamage(damage);
    //         }
    //     }
    // }
}
