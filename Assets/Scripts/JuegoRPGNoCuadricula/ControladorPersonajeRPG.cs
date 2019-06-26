using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControladorPersonajeRPG : MonoBehaviour {

    [Range(2f, 10f)]
    public float velocidadMovimiento = 2f;

    private Rigidbody2D rb2D;

    private void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        
    }
    
    private void FixedUpdate () {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadMovimiento, Input.GetAxis("Vertical") * velocidadMovimiento);
    }

    
}
