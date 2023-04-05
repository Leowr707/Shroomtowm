using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float forca = 20.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //em 5 segundos após ser gerado, o tiro será destruído
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



