using UnityEngine;

public class FlipTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public KeyCode keyE;
    public float f_thrust = 20f;
    public float u_thrust = 20f;

    Rigidbody rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyE))
        {
            Debug.Log("E pressed");
            //gameObject.transform.position = new Vector3(5, 1, 3);
            //Debug.Log(gameObject.transform.up * thrust);
            rb.AddForce(gameObject.transform.up * u_thrust);
            rb.AddForce(gameObject.transform.forward * f_thrust);
        }
    }
}
