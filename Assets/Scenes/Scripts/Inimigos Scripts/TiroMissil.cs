using UnityEngine;

public class TiroMissil : MonoBehaviour
{
    public float forca;
    public float distanciaMaxima;
    public float desaceleracao;
    public Transform Player;
    public float moveSpeed = 5f; // velocidade de movimento do inimigo

    private float distanciaPercorrida;
    private bool parado;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * forca;
    }

    // Update is called once per frame
    void Update()
    {
        if (!parado)
        {
            float distanciaPercorridaAtual = distanciaPercorrida + GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;
            if (distanciaPercorridaAtual < distanciaMaxima)
            {
                distanciaPercorrida = distanciaPercorridaAtual;
            }
            else
            {
                GetComponent<Rigidbody>().velocity -= GetComponent<Rigidbody>().velocity.normalized * desaceleracao * Time.deltaTime;
                if (GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    parado = true;
                }
            }
        }
        else
        {
            Vector3 direction = Player.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Aplica a rotação
            transform.rotation = rotation;

            if (direction.magnitude < 1.0f)
            {
                direction = Player.position - transform.position;
                direction.Normalize();
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        Vector3 moveDirection = transform.up;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
