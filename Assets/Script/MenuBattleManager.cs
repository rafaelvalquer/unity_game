using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuBattleManager : MonoBehaviour
{

    // ------PANEL DE AÇÃO------------------------------
    public GameObject panelAcao;
    public Button botaoAtaque;
    public Button botaoDefesa;


    // ------PANEL DE VIDA/MANA/STAMINA PLAYER------------------------------
    public Slider barraVida;
    private int vida;
    // ------PANEL DE VIDA/MANA/STAMINA INIMIGO------------------------------
    public Slider barraVidaEnemy;
    private int vidaEnemy;


    // ------LISTA DE INIMIGOS------------------------------
    public List<EnemyAttributes> enemiesList = new List<EnemyAttributes>();

    // ------LISTA DE SCRIPTS------------------------------
    // Referência ao script do inimigo
    private Enemy enemyScript;
    // Referência ao script do Player
    private Player playerScript;

    private EnemyAttributes enemyAtributo;
    private CharacterAttributes playerAtributos;

    private void Start()
    {
        // Obtenha a Atributos do Player
        playerAtributos = GameManager.Instance.characterAttributes;
        
        // Obtenha a referência ao script do inimigo
        enemyScript = FindObjectOfType<Enemy>();

        // Obtenha a referência ao Player
        playerScript = FindObjectOfType<Player>();


        // Configura os métodos chamados pelos botões
        botaoAtaque.onClick.AddListener(ExecutarAcaoAtaque);
        botaoDefesa.onClick.AddListener(ExecutarAcaoDefesa);

        // Garante que o painel de ação está desabilitado no início
        DesabilitarPanelAcao();
        // Carrega a lista de inimigos
        InitializeEnemies();
    // ------CARREGA ATRIBUTOS DO INIMIGO DA CENA (enemyAtributo.maxHP)------------------------------
        PegaAtributoInimigo(GameManager.Instance.characterAttributes.nextEnemyName);
    // ------CARREGA BARRAS DE ENERGIA (PLAYER/INIMIGO)------------------------------
        CarregaBarraDeEnergia();

        Debug.Log("LOGA TUDO = " + enemyAtributo.equipment.weaponSlot.itemAtk);

    }
    // Função pública para ser chamada externamente e habilitar o painel de ação
    public void HabilitarPanelAcao()
    {
        panelAcao.SetActive(true);
    }

    // Função pública para ser chamada pelos botões e desabilitar o painel de ação
    public void DesabilitarPanelAcao()
    {
        panelAcao.SetActive(false);
    }

    // Função chamada pelo botão de ataque
    private void ExecutarAcaoAtaque()
    {
        // Lógica do ataque
        playerScript.ExecuteAttackAnimation();


        // Desabilita o painel após a ação
        DesabilitarPanelAcao();
    }

    // Função chamada pelo botão de defesa
    private void ExecutarAcaoDefesa()
    {
        // Lógica da defesa

        // Desabilita o painel após a ação
        DesabilitarPanelAcao();
    }

        public void TakeDamage(int damage)
    {
        // Reduz a vida do jogador
        barraVidaEnemy.value -= damage;
        Debug.Log("Inimigo sofreu " + damage + " de dano!");
        // Adicione qualquer lógica adicional aqui, como verificar se o jogador morreu
    }
        public void TakeDamageEnemy(int damage)
    {
        // Reduz a vida do jogador
        barraVida.value -= damage;
        Debug.Log("Player sofreu " + damage + " de dano!");
        // Adicione qualquer lógica adicional aqui, como verificar se o jogador morreu
    }

    // Método para o jogador atacar o inimigo
    public void PlayerAttack()
    {
        // Calcular o ataque do jogador
        int playerAttack = playerAtributos.attack + playerAtributos.equipment.weaponSlot.attackBonus;

        // Calcular a defesa do inimigo
        int enemyDefense = enemyAtributo.defense + enemyAtributo.equipment.weaponSlot.itemDef;

        // Rolar um "D20" e adicionar ao ataque do jogador
        int playerAttackRoll = RollD20();
        playerAttack += playerAttackRoll;

        // Rolar um "D20" e adicionar à defesa do inimigo
        int enemyDefenseRoll = RollD20();
        enemyDefense += enemyDefenseRoll;

        // Comparar ataque e defesa
        if (playerAttack > enemyDefense)
        {
            // O ataque foi bem-sucedido
            int damage = playerAttack - enemyDefense;
            Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);

            // Aplique o dano ao inimigo
            TakeDamage(damage);
        }
        else
        {
            // O ataque foi bloqueado ou não causou dano
            Debug.Log("Ataque bloqueado pelo inimigo!");
        }
    }

    // Método para o Inimigo atacar o Jogardo
    public void EnemyAttack()
    {
        // Calcular o ataque do jogador
        int enemyAttack =  enemyAtributo.attack + enemyAtributo.equipment.weaponSlot.itemAtk;
    
        // Calcular a defesa do inimigo
        int playerDefense = playerAtributos.defense + playerAtributos.equipment.weaponSlot.defenseBonus;
     
        // Rolar um "D20" e adicionar ao ataque do jogador
        int enemyAttackRoll = RollD20();
        enemyAttack += enemyAttackRoll;

        // Rolar um "D20" e adicionar à defesa do inimigo
        int playerDefenseRoll = RollD20();
        playerDefense += playerDefenseRoll;

        // Comparar ataque e defesa
        if (enemyAttack > playerDefense)
        {
            // O ataque foi bem-sucedido
            int damage = enemyAttack - playerDefense;
            Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);

            // Aplique o dano ao inimigo
            TakeDamageEnemy(damage);
        }
        else
        {
            // O ataque foi bloqueado ou não causou dano
            Debug.Log("Ataque bloqueado pelo inimigo!");
        }
    }


    //Rolagem de dado
        private int RollD20()
    {
        // Simula o lançamento de um dado D20
        return Random.Range(1, 21);
    }
















































    // Adicione inimigos à lista
    private void InitializeEnemies()
    {
        AddEnemy("Orc", 150, 50, 5, 5, 5, 5, 5, 5,
                "Weapon", "BasicSword", 2, 0, "Weapon/1",
                "Shield", "WoodenShield", 0, 2, "Weapon/1",
                "Helmet", "LeatherHelmet", 0, 1, "Helmet/Hat-Helmet");

        AddEnemy("Elf", 80, 40, 7, 3, 7, 7, 8, 3,
                "Weapon", "ElvenSword", 3, 1, "Weapon/1",
                "Shield", "ElvenShield", 1, 3, "Weapon/1",
                "Helmet", "ElvenHelmet", 1, 2, "Helmet/Hat-Helmet");

    }
        private void AddEnemy(string name, int maxHealth, int maxMana, int atk, int def, int spd, int acc, int eva, int crit,
        string weaponType, string weaponName, int weaponAtk, int weaponDef, string weaponSpritePath,
        string shieldType, string shieldName, int shieldAtk, int shieldDef, string shieldSpritePath,
        string helmetType, string helmetName, int helmetAtk, int helmetDef, string helmetSpritePath)
    {
        EnemyAttributes enemy = new EnemyAttributes(name, maxHealth, maxMana, atk, def, spd, acc, eva, crit,
            weaponType, weaponName, weaponAtk, weaponDef, weaponSpritePath,
            shieldType, shieldName, shieldAtk, shieldDef, shieldSpritePath,
            helmetType, helmetName, helmetAtk, helmetDef, helmetSpritePath);

        enemiesList.Add(enemy);
    }
    private void PegaAtributoInimigo(string enemyName){
        enemyAtributo = enemiesList.Find(enemy => enemy.enemyName == enemyName);
        Debug.Log($"Atributo do Inimigo - Max HP: {enemyAtributo.maxHP}");
    }
    private void CarregaBarraDeEnergia(){

        // ------PLAYER------------------------------
        vida = GameManager.Instance.characterAttributes.maxHP;
        Debug.Log(vida);
        barraVida.maxValue = vida;
        barraVida.value = vida;   

        // ------INIMIGO------------------------------
        vidaEnemy = enemyAtributo.maxHP;
        Debug.Log(vidaEnemy);
        barraVidaEnemy.maxValue = vidaEnemy;
        barraVidaEnemy.value = vidaEnemy;
    }


}
