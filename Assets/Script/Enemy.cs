using UnityEngine;

[System.Serializable]

public class EnemyEquipmentSlot
{
    public string itemType;
    public string itemName;
    public int itemAtk;
    public int itemDef;
    public string spritePath;

    public EnemyEquipmentSlot(string name, string item, int attack, int defense, string sprite)
    {
        itemType = name;
        itemName = item;
        itemAtk = attack;
        itemDef = defense;
        spritePath = sprite;

    }
}

public class EnemyEquipment
{
    public EnemyEquipmentSlot weaponSlot;
    public EnemyEquipmentSlot shieldSlot;
    public EnemyEquipmentSlot helmetSlot;
    // Adicione mais slots conforme necessário
}

[System.Serializable]
public class EnemyAttributes
{
    public string enemyName;
    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    public int attack;
    public int defense;
    public int speed;
    public int accuracy;
    public int evasion;
    public int critical;
    // Nova propriedade para armazenar as linhas de diálogo
    public string[] Dialogues { get; private set; }

    public EnemyEquipment equipment;

    public EnemyAttributes(string name, int maxHealth, int maxMana, int atk, int def, int spd, int acc, int eva, int crit,
                           string weaponType, string weaponName, int weaponAtk, int weaponDef, string weaponSpritePath,
                           string shieldType, string shieldName, int shieldAtk, int shieldDef, string shieldSpritePath,
                           string helmetType, string helmetName, int helmetAtk, int helmetDef, string helmetSpritePath,  string[] dialogues)
    {
        enemyName = name;
        maxHP = maxHealth;
        currentHP = maxHP;

        this.maxMana = maxMana;
        currentMana = this.maxMana;

        attack = atk;
        defense = def;
        speed = spd;
        accuracy = acc;
        evasion = eva;
        critical = crit;

        Dialogues = dialogues;

        equipment = new EnemyEquipment();
        equipment.weaponSlot = new EnemyEquipmentSlot(weaponType, weaponName, weaponAtk, weaponDef, weaponSpritePath);
        equipment.shieldSlot = new EnemyEquipmentSlot(shieldType, shieldName, shieldAtk, shieldDef, shieldSpritePath);
        equipment.helmetSlot = new EnemyEquipmentSlot(helmetType, helmetName, helmetAtk, helmetDef, helmetSpritePath);
    }
}


public class Enemy : MonoBehaviour
{
    // Animator do inimigo
    public Animator enemyAnim;


    // Referência ao script do Player
    private Player playerScript;
    // Referência ao script do Menu
    private MenuBattleManager menuBattleManager;


    public EnemyAttributes enemyAttributes;

    // Adicione as referências aos seus SpriteRenderers no Inspector
    public SpriteRenderer weaponSpriteRenderer;
    public SpriteRenderer shieldSpriteRenderer;
    public SpriteRenderer helmetSpriteRenderer;


    // ------REFERENCIAS PARA OS DIALOGOS------------------------------
    private string speechTxt; // PRIMEIRA FRASE DO DIALOGO
    private string resosta1Txt; // RESPOSTA 1
    private string resosta2Txt; // RESPOSTA 2
    private string speechFinalTxt; // Texto Final
    private string actorNameEnemy;
    public LayerMask playerLayer; // Layer do player
    public float radious; // 

    // ------VARIAVEL PARA DESABILITAR A FUNÇÃO Interact------------------------------
    private bool interactionEnabled = true;




    private void Start()
    {        
        // Obtenha a referência ao MenuBattleManager
        menuBattleManager = FindObjectOfType<MenuBattleManager>();
        // Obtenha a referência ao Player
        playerScript = FindObjectOfType<Player>();


        // Carrega os Itebs
        LoadEquippedItems(GameManager.Instance.characterAttributes.nextEnemyName);

        
        //Debug.Log("Status itemType = " + enemyAttributes.equipment.weaponSlot.itemType);
        //Debug.Log("Status itemName = " + enemyAttributes.equipment.weaponSlot.itemName);
        //Debug.Log("Status itemAtk = " + enemyAttributes.equipment.weaponSlot.itemAtk);
        //Debug.Log("Status itemDef = " + enemyAttributes.equipment.weaponSlot.itemDef);
        //Debug.Log("Enemy Name in Start: @@@@@@@@ " + enemyAttributes.enemyName);
        actorNameEnemy = enemyAttributes.enemyName;

        // Atribuindo os dialogos
        speechTxt =  enemyAttributes.Dialogues[0];
        resosta1Txt = enemyAttributes.Dialogues[1];
        resosta2Txt = enemyAttributes.Dialogues[2];
        speechFinalTxt = enemyAttributes.Dialogues[3];

     
    }
    
    // ------FUNÇÃO UPDATE QUE TRABALHAR COM FISICA------------------------------
    private void FixedUpdate()
    {
        if (interactionEnabled)
        {
        Interact();
        }
    }
    
    
    
    
    // ------FUNÇÃO PARA INICIAR INTERAÇÃO COM O INIMIGO AO CHEGAR NO COLISOR------------------------------
    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if(hit != null)
        {
            menuBattleManager.HabilitarPanelDialogo(speechTxt, actorNameEnemy);
            interactionEnabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,radious);
    }    

    public void ExecuteAttackAnimation()
    {
        // Ative o gatilho para a animação de ataque
        enemyAnim.SetBool("Attack", true);

        // Inicia uma coroutine para aguardar o término da animação antes de reativar o painel
        StartCoroutine(WaitForAttackAnimation());
    }

    private System.Collections.IEnumerator WaitForAttackAnimation()
    {
        // Aguarda o próximo frame
        yield return null;

        // Aguarda até que a animação de ataque seja concluída
        while (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }

        // Animacao de ataque concluída, agora habilitamos o painel eretira o HP
        menuBattleManager.EnemyAttack();
        menuBattleManager.HabilitarPanelAcao();
    }

    public void ExecuteJumpAnimation()
    {
        // Ative o gatilho para a animação de pulo
        enemyAnim.SetBool("Jump", true);

        // Inicie uma coroutine para aguardar o término da animação antes de reativar o painel
        StartCoroutine(WaitForJumpAnimation());
    }

    private System.Collections.IEnumerator WaitForJumpAnimation()
    {
        // Aguarde até o próximo quadro para garantir que a animação seja acionada
        yield return null;

        // Aguarde o término da animação de pulo
    while (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
    {
        // Ajuste a velocidade e altura do pulo conforme necessário
        float moveSpeed = 100f; // Velocidade de movimento lateral
        float jumpHeight = 100f; // Altura do pulo

        // Mova o inimigo para a direita enquanto está no ar
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // Aplique uma força para cima para simular um pulo
        transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);

        yield return null;
    }

        // Desative o gatilho da animação de pulo
        enemyAnim.SetBool("Jump", false);
    }
    public void ReturnToIdle()
    {
        enemyAnim.SetBool("Attack", false);
        enemyAnim.SetBool("Jump", false);
    }
    // Método para carregar os sprites dos itens equipados
    public void LoadEquippedItems(string enemyName)
    {

        // Encontre o inimigo na lista com base no nome
        enemyAttributes = menuBattleManager.enemiesList.Find(enemy => enemy.enemyName == enemyName);

        if (enemyAttributes != null)
        {
            if (enemyAttributes.equipment.weaponSlot != null)
            {
                LoadSprite(enemyAttributes.equipment.weaponSlot.spritePath, weaponSpriteRenderer);
            }
            else
            {
                weaponSpriteRenderer.sprite = null;
            }

            if (enemyAttributes.equipment.shieldSlot != null)
            {
                LoadSprite(enemyAttributes.equipment.shieldSlot.spritePath, shieldSpriteRenderer);
            }
            else
            {
                shieldSpriteRenderer.sprite = null;
            }

            if (enemyAttributes.equipment.helmetSlot != null)
            {
                LoadSprite(enemyAttributes.equipment.helmetSlot.spritePath, helmetSpriteRenderer);
            }
            else
            {
                helmetSpriteRenderer.sprite = null;
            }
        }
        else
        {
            Debug.LogError("Inimigo não encontrado: " + enemyName);
        }
    }

    // Método para carregar um sprite e atribuí-lo a um SpriteRenderer
    private void LoadSprite(string spritePath, SpriteRenderer spriteRenderer)
    {
        Sprite sprite = Resources.Load<Sprite>(spritePath);

        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogError("Erro ao carregar o sprite: " + spritePath);
        }
    }
}
