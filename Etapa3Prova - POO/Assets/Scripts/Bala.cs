using System;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField]
    private int dano = 1;
    [SerializeField]
    private float velocidade = 5f;
    
    private Renderer m_Renderer;

    public void setDano(int dano)
    {
        this.dano = dano;
    }

    public int getDano()
    {
        return this.dano;
    }

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        transform.Translate(velocidade * Time.deltaTime, 0, 0);

        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            int novaVida = collision.gameObject.GetComponent<Personagem>().getVida() - getDano();
            collision.gameObject.GetComponent<Personagem>().setVida(novaVida);
        }
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}