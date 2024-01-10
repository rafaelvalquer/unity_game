using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;

    private Rigidbody2D rb;
    private Animator anim;

    // ------VARIAVEIS DE MOVIMENTAÇÃO------------------------------
    private bool isMoving = false;
    private bool allowMoveForward = true;



    // Referência ao script do inimigo
    private Enemy enemyScript;
    // Referência ao script do Menu
    private MenuBattleManager menuBattleManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Obtenha a referência ao script do inimigo
        enemyScript = FindObjectOfType<Enemy>();

        // Obtenha a referência ao MenuBattleManager
        menuBattleManager = FindObjectOfType<MenuBattleManager>();
    }

    private void Update()
    {
        // Inicia o movimento do personagem ao pressionar a tecla de seta direcional direita
        if (Input.GetKeyDown(KeyCode.RightArrow) && allowMoveForward)
        {
            allowMoveForward = true;  // Impede movimento para frente
            isMoving = true;
        }

        // Para o movimento do personagem ao soltar a tecla de seta direcional direita
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMoving = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);  // Define a velocidade horizontal como zero
            anim.SetBool("Run", false);
        }

        if (isMoving)
        {
            // Move o personagem no eixo X
            Move();
        }
    }

    private void Move()
    {
        // Move o personagem no eixo X
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        anim.SetBool("Run", true);
    }

    // Função para chamar quando você não quer mais que o jogador vá para frente
    public void BloquearMovimentoParaFrente()
    {
        allowMoveForward = false;
        isMoving = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);  // Define a velocidade horizontal como zero
        anim.SetBool("Run", false);
    }
    // Função para chamar quando você quer permitar que o jogador vá para frente
    public void DesbloquearMovimentoParaFrente()
    {
    allowMoveForward = true;
    isMoving = false;
    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // Define a velocidade horizontal
    anim.SetBool("Run", false);
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Se colidir com outro personagem, pare o movimento
            rb.velocity = Vector2.zero;
            isMoving = false;
            anim.SetBool("Run", false);
            DesbloquearMovimentoParaFrente();
            menuBattleManager.HabilitarPanelAcao();
        }
    }
        public void ExecuteAttackAnimation()
    {
        // Ative o gatilho para a animação de ataque
        anim.SetBool("Attack", true);

        // Inicie a corrotina para esperar o término da animação
        StartCoroutine(WaitForAttackAnimation());
    }

    // Corrotina para aguardar o término da animação de ataque
        private System.Collections.IEnumerator WaitForAttackAnimation()
    {
        // Aguarda o próximo frame
        yield return null;

        // Aguarda até que a animação de ataque seja concluída
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }
        // Animção de ataque terminou, agora podemos chamar a próxima função
        menuBattleManager.PlayerAttack();
        enemyScript.ExecuteAttackAnimation();
    }

        public void ReturnToIdle()
    {
        anim.SetBool("Attack", false);
    }
}
