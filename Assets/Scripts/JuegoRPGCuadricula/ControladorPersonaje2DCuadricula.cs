using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonaje2DCuadricula : MonoBehaviour {

    [Range(0.5f, 3f)]
    public float velocidadMovimiento = 0.5f;
    [Tooltip("Layer de los objetos que no podrá atravesar (estos deben tener collider)")]
    public LayerMask layerColision;
    [Tooltip("Tamaño de las casillas del Tilemap, si se usa el Tilemap de Unity será el CellSize del Grid")]
    public float tamanioCasilla = 1;

    private Vector2 siguientePosicion;
    private bool collisioned;
    private RaycastHit2D ray;
    private Vector2 direccionRayo;
    
	private void Start () {
        siguientePosicion = transform.position;
    }
    
	
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direccionRayo = Vector2.right;
            if (PuedeMoverseALaSiguienteCasilla())
            {
                siguientePosicion.x += tamanioCasilla;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direccionRayo = Vector2.left;
            if (PuedeMoverseALaSiguienteCasilla())
            {
                siguientePosicion.x -= tamanioCasilla;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direccionRayo = Vector2.up;
            if (PuedeMoverseALaSiguienteCasilla())
            {
                siguientePosicion.y += tamanioCasilla;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direccionRayo = Vector2.down;
            if (PuedeMoverseALaSiguienteCasilla())
            {
                siguientePosicion.y -= tamanioCasilla;
            }
        }
        
        //Lanza un rayo cuando se ejecuta el juego desde Unity. Para pruebas. Se puede borrar.
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + tamanioCasilla * direccionRayo.x, transform.position.y + tamanioCasilla * direccionRayo.y), Color.red, 0);

        transform.position = Vector2.MoveTowards(transform.position, siguientePosicion, velocidadMovimiento * Time.deltaTime);            
        
    }

    private bool PuedeMoverseALaSiguienteCasilla()
    {
        //Si en la dirección del próximo movimiento hay un collider del layer definido como obstáculo no se puede mover
        if(Physics2D.Raycast(transform.position, direccionRayo, tamanioCasilla, layerColision))
        {
            return false;
        }
        //Si está casi en la siguiente posición sí puede volver a moverse
        else if (Mathf.Abs(siguientePosicion.y - transform.position.y) < float.Epsilon && Mathf.Abs(siguientePosicion.x - transform.position.x) < float.Epsilon)
        {
            return true;
        }

        return false;
    }
    
}
