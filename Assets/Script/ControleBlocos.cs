using UnityEngine;

public class ControleBlocos : MonoBehaviour
{
    public Rigidbody2D bloco1Rigidbody;
    public Rigidbody2D bloco2Rigidbody;
    public Rigidbody2D bloco3Rigidbody;
    public Rigidbody2D bloco4Rigidbody;
    public Rigidbody2D bloco5Rigidbody;
    public Rigidbody2D bloco6Rigidbody;
    public Rigidbody2D bloco7Rigidbody;
    public Rigidbody2D bloco8Rigidbody;
    public Rigidbody2D bloco9Rigidbody;
    public Rigidbody2D bloco10Rigidbody;

    void Start()
    {
        // Exemplo: atribui os Rigidbodies aos blocos
        bloco1Rigidbody = GameObject.Find("Bloco1").GetComponent<Rigidbody2D>();
        bloco2Rigidbody = GameObject.Find("Bloco2").GetComponent<Rigidbody2D>();
        bloco3Rigidbody = GameObject.Find("Bloco3").GetComponent<Rigidbody2D>();
        bloco4Rigidbody = GameObject.Find("Bloco4").GetComponent<Rigidbody2D>();
        bloco5Rigidbody = GameObject.Find("Bloco5").GetComponent<Rigidbody2D>();
        bloco6Rigidbody = GameObject.Find("Bloco6").GetComponent<Rigidbody2D>();
        bloco7Rigidbody = GameObject.Find("Bloco7").GetComponent<Rigidbody2D>();
        bloco8Rigidbody = GameObject.Find("Bloco8").GetComponent<Rigidbody2D>();
        bloco9Rigidbody = GameObject.Find("Bloco9").GetComponent<Rigidbody2D>();
        bloco10Rigidbody = GameObject.Find("Bloco10").GetComponent<Rigidbody2D>();

        // Exemplo: chama a função para alterar o Rigidbody para Dynamic de ambos os blocos
        //AlterarBodyTypeDynamic(bloco4Rigidbody);
        //AlterarBodyTypeDynamic(bloco2Rigidbody);
    }

    void AlterarBodyTypeDynamic(Rigidbody2D rb)
    {
        // Verifica se o Rigidbody2D foi encontrado
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Altera o body type para Dynamic
            rb.gravityScale = 10f; // Altera o gravity scale para 10
        } else 
        {
            Debug.LogError("Erro ao carregar o Tile: ");
        }
    }
}
