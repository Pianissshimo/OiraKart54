using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    public float now_speed = 10f;
    public float jumpForce = 5f;       // ÉWÉÉÉìÉvóÕ
    private Rigidbody rb;
    private bool isGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            this.transform.position += this.transform.forward * now_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += this.transform.right * -now_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += this.transform.forward * -now_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * now_speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }
    // ínñ Ç∆ÇÃê⁄êGîªíË
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            isGrounded = true;
        }
    }
}
