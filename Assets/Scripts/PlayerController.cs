using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    private float fuel;
    public float consumtiom;
    public float speed;
    public ParticleSystem rcsUP, rcsLeft, rcsRight;
    public GameObject GameOver, Win, explosion;
    public AudioSource audio;
    public TextMeshProUGUI text;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        fuel = 100;
        rcsUP.Stop();
        rcsRight.Stop();
        rcsLeft.Stop();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical") * 3;
        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);
        rb.AddForce(movement * speed);
        setFuelText();
    }

    // Update is called once per frame
    void Update () {
        if (fuel > 0)
        {
            setFuelText();
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                fuel -= Time.deltaTime * 100;
                Debug.Log(fuel);
                rcsUP.Play();
                audio.Play();
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rcsUP.Stop();
                audio.Stop();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                fuel -= Time.deltaTime * 100;
                rcsRight.Play();
                audio.Play();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                rcsRight.Stop();
                audio.Stop();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                fuel -= Time.deltaTime * 100;
                rcsLeft.Play();
                audio.Play();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                rcsLeft.Stop();
                audio.Stop();
            }
        }
        else
        {
            fuel = 0;
            setFuelText();
            rcsUP.Stop();
            rcsRight.Stop();
            rcsLeft.Stop();
            audio.Stop();
        }
    }
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Bad")) {
            var ex = Instantiate(explosion, transform);
            ex.transform.position = transform.position;
            ex.transform.localScale = new Vector3(2, 2, 2);
            rb.useGravity = false;
            yield return new WaitForSeconds(0.6f);
            gameObject.SetActive(false);
            GameOver.SetActive(true);
        }
        if (collision.collider.gameObject.CompareTag("Finish"))
        {
            Win.SetActive(true);
        }
    }

    private void setFuelText() {
        text.SetText("Fuel: " + fuel.ToString());
    }
}
