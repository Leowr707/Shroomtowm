using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAttack : MonoBehaviour
{
    public float forca = 5.0f;
    float cd = Time.deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forca);
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * forca * Time.deltaTime;



    }

    void Update()
    {

    }
}





































/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    GameObject projectilePrefab;
    private float speed = 20;
    private Rigidbody rigidbody;
    private float nextFireTime = 0.0f;
    public Transform projectileSpawnPoint;
    public float fireRate = 0.5f;

    void Start()
    {
        //pega o componente Rigidbody
        rigidbody = GetComponent<Rigidbody>();
        //agora tem que colocar velocidade na bala
        //transform.forward eh o codigo que pega a a direcao q to olhando... sera q vai da ruim?
        rigidbody.velocity = transform.position * speed;




        {

        }

        void Update()
        {

        }
    }
}*/



