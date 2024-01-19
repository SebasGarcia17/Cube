using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    // Variables para la velocidad y otros ajustes
    private float walkSpeed = 7f;
    private float turnSpeed = 200f;
    private float raycastLength = 1f;
    public LayerMask Mask; // Máscara de capa para el Raycast
    private Vector3 hitPoint; // Punto de impacto del Raycast
    public Vector3 Forward = new Vector3(0, -1, 1); // Vector de dirección hacia adelante
    public Vector3 forwardOffset = Vector3.zero; // Desplazamiento hacia adelante
    public Vector3 moveOffset = Vector3.zero; // Desplazamiento general
    public Vector3 backward = Vector3.zero;
    private float spaceDistance = 0.1f;


    private Vector3 lastNormal = Vector3.zero;

    void Update()
    {
        Movement(); // Lógica de movimiento
    }

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 3f);
        Quaternion rotacion = Quaternion.LookRotation(hit.normal);
        Debug.DrawRay(transform.position+transform.forward*1f,rotacion*Vector3.forward*3f,Color.blue);
        Debug.DrawRay(transform.position+transform.forward*1f,rotacion*Vector3.up*3f,Color.green);
        Debug.DrawRay(transform.position+transform.forward*1f,rotacion*Vector3.right*3f,Color.red);
        if (!CheckDirection(transform.position, Vector3.down))
        {
            Debug.Log("Entre");
            Rotation(); // Lógica de rotación si no hay suelo debajo
            MoveToPosition(transform.position, Vector3.down);

        }
    }

    void Movement()
    {
        // Captura de entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movimiento hacia adelante/atrás
        transform.Translate(walkSpeed * Time.deltaTime * Vector3.forward * verticalInput);

        // Rotación izquierda/derecha
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput);

    }

    bool CheckDirection(Vector3 position, Vector3 direction)
    {
        // Verifica si hay suelo debajo usando un Raycast
        return Physics.Raycast(position, transform.TransformDirection(Vector3.down),raycastLength, Mask);
    }
    void MoveToPosition(Vector3 position, Vector3 direction)
    {
        // Verifica si hay suelo debajo usando un Raycast
        if (Physics.Raycast(position, transform.TransformDirection(Vector3.down), out RaycastHit hitPoint, raycastLength, Mask))
        {
            Vector3 targetPosition = hitPoint.point + (transform.up * spaceDistance);
            transform.position = targetPosition;
            
            Quaternion rotacion = Quaternion.LookRotation(hitPoint.normal);
            float angle =  Vector3.Angle(transform.up, rotacion * Vector3.forward); 
            float signoAngle = Mathf.Sign(Vector3.SignedAngle(transform.up, rotacion * Vector3.forward, transform.forward)); 
            
            print($"hitpoint{hitPoint.normal}angle{angle}signoangle{signoAngle}");
            Quaternion addRotation = Quaternion.Euler(0, 0,signoAngle*angle );
            transform.rotation = transform.rotation * addRotation;
            
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);

            //Vector3 targetDirection = hitPoint.normal;

            // Calcular el ángulo en grados para el eje X
            // float anguloX = Mathf.Atan2(targetDirection.y, targetDirection.z) * Mathf.Rad2Deg;

            // Calcular el ángulo en grados para el eje Y
            // float anguloY = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

            // Calcular el ángulo en grados para el eje Z

            // float anguloZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            // Quaternion rotationQuaternion = Quaternion.Euler(anguloX, anguloY, anguloZ);
            // transform.up = targetDiretion;}

        }

    }

    void Rotation()
    {
        transform.Translate(moveOffset);
        transform.Rotate(Vector3.right * 90);
        

    }

    private void OnDrawGizmos()
    {
        // Dibuja gizmos para visualizar rayos y otros elementos
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + forwardOffset, transform.TransformDirection(Forward) * raycastLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(moveOffset) * raycastLength);
        Gizmos.DrawCube(transform.position + moveOffset, Vector3.one * 0.05f);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(backward) * raycastLength);
        Gizmos.DrawCube(transform.position + backward, Vector3.one * 0.05f);


        Gizmos.color = Color.magenta;
        if (hitPoint != Vector3.zero)
        {
            Gizmos.DrawSphere(hitPoint, 0.1f);
        }
    }
}

