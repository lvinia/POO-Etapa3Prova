using UnityEngine;

public class Inimigo : Personagem
{
    [SerializeField]
    private int dano = 1;
    //public float
    [SerializeField]
    private Transform PosicaoDoPlayer;
    
    private AudioSource audioSource;
    
    private SpriteRenderer spriteRenderer;
    
    private Animator animator;
    
    private bool andando = false;

    public float raioDeVisao = 1;
    public CircleCollider2D _visaoCollider2D;

    public void setDano(int dano)
    {
        this.dano = dano;
    }
    public int getDano()
    {
        return this.dano;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        
        if (PosicaoDoPlayer == null)
        {
            PosicaoDoPlayer =  GameObject.Find("Player").transform;
        }
        
        raioDeVisao = _visaoCollider2D.radius;
    }

    void Update()
    {
        andando = false;

        if (getVida() > 0)
        {
            if (PosicaoDoPlayer.position.x - transform.position.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (PosicaoDoPlayer.position.x - transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }


            if (PosicaoDoPlayer != null &&
                Vector3.Distance(PosicaoDoPlayer.position, transform.position) <= raioDeVisao)
            {
                //Debug.Log("Posição do Player" + PosicaoDoPlayer.position);

                transform.position = Vector3.MoveTowards(transform.position,
                    PosicaoDoPlayer.transform.position,
                    getVelocidade() * Time.deltaTime);

                andando = true;
            }

        }

        if (getVida() <= 0)
        {
            animator.SetTrigger("Morte");
        }
        
        animator.SetBool("Andando",andando);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causa dano ao Player
            int novaVida = collision.gameObject.GetComponent<Personagem>().getVida() - getDano();
            collision.gameObject.GetComponent<Personagem>().setVida(novaVida);

            //collision.gameObject.GetComponent<Personagem>().recebeDano(getDano());
            
            setVida(0);
        }
    }

    public void playAudio()
    {
        audioSource.Play();
    }

    public void desative()
    {
        //desativa quando bate no player
         gameObject.SetActive(false);
    }
}