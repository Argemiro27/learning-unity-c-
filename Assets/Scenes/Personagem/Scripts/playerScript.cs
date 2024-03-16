using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerScript : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed;
    public float gravity;
    public float rotSpeed;

    private float rot;
    private Vector3 direcaoMovimento;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // Está tocando o chão
        if (controller.isGrounded)
        {
            // Se pressionar W
            if (Input.GetKey(KeyCode.W))
            {
                // Quando pressionado W, o vector3.forward vai aumentar o valor de z, que é andar pra frente
                direcaoMovimento = Vector3.forward * speed;
                // Quando estiver andando, seta o transition como 1
                animator.SetInteger("transition", 1);
            }
            // Se soltar a tecla W
            if (Input.GetKeyUp(KeyCode.W)) 
            {
                direcaoMovimento = Vector3.zero;
                // Quando não estiver andando, seta transition como
                animator.SetInteger("transition", 0);
            }

        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        // Como y é pra baixo, ele força o personagem a ir pra baixo
        direcaoMovimento.y -= gravity * Time.deltaTime;
        direcaoMovimento = transform.TransformDirection(direcaoMovimento);

        controller.Move(direcaoMovimento * Time.deltaTime);
    }


}
