using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb2d;
    Joystick joystick;
    Vector2 velocity;
              //Animator animator;

    [SerializeField]
    float hiz = default;

    [SerializeField]
    float hizlanma = default;

    [SerializeField]
    float yavaslama = default;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
              //animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        JoystickKontrol();
    }

    void JoystickKontrol()
    {
        float yatayInput = joystick.Horizontal;      
        float dikeyInput = joystick.Vertical;
        Vector2 scale = transform.localScale;   

        if (yatayInput > 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, yatayInput * hiz, hizlanma * Time.deltaTime);
            //animator.SetBool("Walk", true);
            scale.x = 1.0f;                   
        }
        else if (yatayInput < 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, yatayInput * hiz, hizlanma * Time.deltaTime);
            //animator.SetBool("Walk", true);
            scale.x = -1.0f;  
        }
        if (dikeyInput > 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, dikeyInput * hiz, hizlanma * Time.deltaTime);
            //animator.SetBool("Walk", true);
            scale.x = 1.0f;   //?                  
        }
        else if (dikeyInput < 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, dikeyInput * hiz, hizlanma * Time.deltaTime);
            //animator.SetBool("Walk", true);
            scale.x = -1.0f;  //?
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, yavaslama * Time.deltaTime);
            velocity.y = Mathf.MoveTowards(velocity.y, 0, yavaslama * Time.deltaTime);
            //animator.SetBool("Walk", false);

        }

        transform.localScale = scale;
        transform.Translate(velocity * Time.deltaTime);

        }
    }

