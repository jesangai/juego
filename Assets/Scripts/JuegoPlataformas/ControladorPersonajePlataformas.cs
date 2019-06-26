using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControladorPersonajePlataformas : MonoBehaviour {

    [Range(50f, 300f)]
    public float velocidadMovimiento = 50f;
    [Range(100f, 600f)]
    public float fuerzaSalto = 100f;
    [Range(100f, 600f)]
    public float fuerzaSaltoDoble = 100f;
    [Tooltip("Actívalo si quieres que pueda saltar una segunda vez")]
    public bool tieneSaltoDoble = false;
    [Tooltip("Actívalo si quieres que el sprite rote automáticamente en horizontal al cambiar de dirección")]
    public bool autoRotarSprite = true;
    [Tooltip("Actívalo si quieres que no se pueda cambiar de dirección mientras se está en el aire")]
    public bool puedeMoverseMientrasSalta = true;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private bool saltando;
    private bool saltandoDoble;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Saltar();
    }

    private void FixedUpdate()
    {
        Mover();         
    }

    private void Saltar()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!saltando)
            {
                saltando = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, fuerzaSalto));
            }
            else if (tieneSaltoDoble && !saltandoDoble)
            {
                saltandoDoble = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, fuerzaSaltoDoble));
            }
        }
    }

    private void Mover()
    {
        if((puedeMoverseMientrasSalta && saltando) || !saltando)
        {
            rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime, rb2d.velocity.y);

            if (autoRotarSprite)
            {
                if (rb2d.velocity.x > 0.01f)
                    sprite.flipX = false;
                else if (rb2d.velocity.x < -0.01f)
                    sprite.flipX = true;
            }
        }        
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        saltando = false;
        saltandoDoble = false;
    }

    
}
