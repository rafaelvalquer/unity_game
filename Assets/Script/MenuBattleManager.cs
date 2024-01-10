using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuBattleManager : MonoBehaviour
{

    // ------PANEL DE AÇÃO------------------------------
    [Header("Panel Ação")]
    public GameObject panelAcao;
    public Button botaoAtaque;
    public Button botaoDefesa;

    // ------PANEL DE DIALOGO------------------------------
    [Header("Panel Dialogo")]
    public GameObject panelDialogo;
    public Button botaoNextDialogo;
    public Text speechText;
    public Text actorNameText;

    // ------PANEL DE RESPOSTAS------------------------------
    [Header("Panel Respostas")]   
    public GameObject panelRespostas;
    public Button botaoResposta1;
    public Button botaoResposta2;
    public Text speechTextResposta1;
    public Text speechTextResposta2;
    public Text playerNameText;
   

    // ------PANEL DE STATUS GAME------------------------------
    [Header("Panel Status Game")]   
    public GameObject panelStatusGame;
    public Button botaoNextStatus;
    public Text statusText;

    // ------CONTROLA VELOCIDADE DO TEXTO------------------------------
    [Header("Controla Veloc Txt")]   
    public float typingSpeed;

    


    // ------PANEL DE VIDA/MANA/STAMINA PLAYER------------------------------
    [Header("Barra VIDA/MANA/STAMINA Player")]   
    public Slider barraVida;
    private int vida;

    // ------PANEL DE VIDA/MANA/STAMINA INIMIGO------------------------------
    [Header("Barra VIDA/MANA/STAMINA Enemy")]   
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
        botaoNextDialogo.onClick.AddListener(() => HabilitarPanelRespostas(enemyAtributo.Dialogues[1], enemyAtributo.Dialogues[2]));
        botaoResposta1.onClick.AddListener(() => HabilitarPanelDialogo(enemyAtributo.Dialogues[3], enemyAtributo.enemyName));
        botaoResposta2.onClick.AddListener(() => HabilitarPanelDialogo(enemyAtributo.Dialogues[3], enemyAtributo.enemyName));


        // Garante que o painel de ação está desabilitado no início
        DesabilitarPanelAcao();
        // Carrega a lista de inimigos
        InitializeEnemies();
    // ------CARREGA ATRIBUTOS DO INIMIGO DA CENA (enemyAtributo.maxHP)------------------------------
        PegaAtributoInimigo(GameManager.Instance.characterAttributes.nextEnemyName);
    // ------CARREGA BARRAS DE ENERGIA (PLAYER/INIMIGO)------------------------------
        CarregaBarraDeEnergia();

        Debug.Log("LOGA TUDO = " + playerAtributos.characterName);



    }
    // Função pública para ser chamada externamente e habilitar o painel de AÇÃO/DIALOGO/RESPOSTAS/STATUSGAME
    public void HabilitarPanelAcao()
    {
        panelAcao.SetActive(true);
    }
    public void HabilitarPanelDialogo(string txt, string actorName)
    {
        DesabilitarPanelResposta();
        playerScript.BloquearMovimentoParaFrente();
        panelDialogo.SetActive(true);
        speechText.text = txt;
        actorNameText.text = actorName;

        if(txt == enemyAtributo.Dialogues[3])
        {
            botaoNextDialogo.onClick.RemoveListener(() => HabilitarPanelRespostas(enemyAtributo.Dialogues[1], enemyAtributo.Dialogues[2]));
            botaoNextDialogo.onClick.AddListener(() =>HabilitarPanelStatusGame("COMEÇAR BATALHA!"));

        }
    }
    public void HabilitarPanelStatusGame(string txt)
    {
        DesabilitarPanelDialogo();
        panelStatusGame.SetActive(true);
        statusText.text = txt;
        playerScript.DesbloquearMovimentoParaFrente();

    }
    public void HabilitarPanelRespostas(string resposta1, string resposta2)
    {
        DesabilitarPanelDialogo();
        panelRespostas.SetActive(true);
        speechTextResposta1.text = resposta1;
        speechTextResposta2.text = resposta2;
        playerNameText.text = playerAtributos.characterName;

    }


    // Função pública para ser chamada pelos botões e desabilitar o painel de AÇÃO/DIALOGO/STATUSGAME
    public void DesabilitarPanelAcao()
    {
        panelAcao.SetActive(false);
    }
    public void DesabilitarPanelResposta()
    {
        panelRespostas.SetActive(false);
    }
    public void DesabilitarPanelDialogo()
    {
        panelDialogo.SetActive(false);
    }
    public void DesabilitarStatusGame()
    {
        panelStatusGame.SetActive(false);
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

        // Calcular a precisão do jogador
        int playerAccuracy = playerAtributos.accuracy;

        // Calcular a evasão do inimigo
        int enemyEvasion = enemyAtributo.evasion;

        // Defina a chance crítica do jogador
        int playerCriticalChance = playerAtributos.critical;

        // Rolar um "D20" e adicionar ao ataque do jogador
        int playerAttackRoll = RollD20();
        playerAttack += playerAttackRoll;

        // Rolar um "D20" e adicionar à defesa do inimigo
        int enemyDefenseRoll = RollD20();
        enemyDefense += enemyDefenseRoll;

        // Comparar ataque e defesa
        if (playerAttack > enemyDefense)
        {
            int damage = playerAttack - enemyDefense;
             // Comparar precisão e evasão
                if (playerAccuracy > enemyEvasion)
                {
                    if(CheckCritical(playerCriticalChance))
                    {
                        // Se for um ataque crítico, aumente o dano (por exemplo, em 50%)
                        float criticalMultiplier = 1.5f; // Ajuste conforme necessário
                        int criticalDamage = Mathf.RoundToInt(damage * criticalMultiplier);
                        Debug.Log("Ataque crítico! Dano causado: " + criticalDamage);
                        // Aplique o dano crítico ao inimigo
                        TakeDamage(criticalDamage);
                    }
                    else
                    {
                        // O ataque foi bem-sucedido
                        Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);
                        // Aplique o dano ao inimigo
                        TakeDamage(damage);
                    }
                }
                else 
                {
                    playerAccuracy = playerAccuracy + RollD20();
                    if (playerAccuracy > enemyEvasion)
                    {
                        // O ataque foi bem-sucedido
                        Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);
                        // Aplique o dano ao inimigo
                        TakeDamage(damage);
                    }
                    else 
                    {
                        Debug.Log("Inimigo conseguiu desviar do Ataque!");
                    }

                }
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
        // Calcular o ataque do inimigo
        int enemyAttack =  enemyAtributo.attack + enemyAtributo.equipment.weaponSlot.itemAtk;
    
        // Calcular a defesa do jogador
        int playerDefense = playerAtributos.defense + playerAtributos.equipment.weaponSlot.defenseBonus;

        // Calcular a precisão do inimigo
        int enemyAccuracy = enemyAtributo.accuracy;

        // Calcular a evasão do jogador
        int playerEvasion = playerAtributos.evasion;

        // Defina a chance crítica do inimigo
        int enemyCriticalChance = enemyAtributo.critical;
    
        // Rolar um "D20" e adicionar ao ataque do inimigo
        int enemyAttackRoll = RollD20();
        enemyAttack += enemyAttackRoll;

        // Rolar um "D20" e adicionar à defesa do jogador
        int playerDefenseRoll = RollD20();
        playerDefense += playerDefenseRoll;

        // Comparar ataque e defesa
        if (enemyAttack > playerDefense)
        {
            int damage = enemyAttack - playerDefense;
                // Comparar precisão e evasão
                if (enemyAccuracy > playerEvasion)
                {
                    if(CheckCritical(enemyCriticalChance))
                    {
                        // Se for um ataque crítico, aumente o dano (por exemplo, em 50%)
                        float criticalMultiplier = 1.5f; // Ajuste conforme necessário
                        int criticalDamage = Mathf.RoundToInt(damage * criticalMultiplier);
                        Debug.Log("Ataque crítico! Dano causado: " + criticalDamage);
                        // Aplique o dano crítico ao inimigo
                        TakeDamageEnemy(criticalDamage);
                    }
                    else
                    {
                        // O ataque foi bem-sucedido
                        Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);
                        // Aplique o dano ao inimigo
                        TakeDamageEnemy(damage);
                    }
                }
                else
                {
                    enemyAccuracy = enemyAccuracy + RollD20();
                    if (enemyAccuracy > playerEvasion)
                    {
                        // O ataque foi bem-sucedido
                        Debug.Log("Ataque bem-sucedido! Dano causado: " + damage);
                        // Aplique o dano ao inimigo
                        TakeDamageEnemy(damage);
                    }
                    else
                    {
                        Debug.Log("Jogador conseguiu desviar do Ataque!");
                    }

                }
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
    
    // Função para calcular o efeito crítico
    private bool CheckCritical(int criticalChance)
    {
        // Rolar um "D100" para determinar se ocorre um ataque crítico
        int roll = Random.Range(1, 101);
        return roll <= criticalChance;
    }
















































    // Adicione inimigos à lista
    private void InitializeEnemies()
    {
        AddEnemy("Orc", 50, 50, 3, 2, 2, 2, 2, 5,
                "Weapon", "BasicSword", 2, 0, "Weapon/1",
                "Shield", "WoodenShield", 0, 2, "Weapon/1",
                "Helmet", "LeatherHelmet", 0, 1, "Helmet/Hat-Helmet",new string[] { "Você não passará!", "Prepare-se para a batalha!", "Vou te Matar!", "Pode vir!" });

        AddEnemy("Elf", 80, 40, 7, 3, 7, 7, 8, 3,
                "Weapon", "ElvenSword", 3, 1, "Weapon/1",
                "Shield", "ElvenShield", 1, 3, "Weapon/1",
                "Helmet", "ElvenHelmet", 1, 2, "Helmet/Hat-Helmet",new string[] { "Você não passará!", "Prepare-se para a batalha!" });

    }
        private void AddEnemy(string name, int maxHealth, int maxMana, int atk, int def, int spd, int acc, int eva, int crit,
        string weaponType, string weaponName, int weaponAtk, int weaponDef, string weaponSpritePath,
        string shieldType, string shieldName, int shieldAtk, int shieldDef, string shieldSpritePath,
        string helmetType, string helmetName, int helmetAtk, int helmetDef, string helmetSpritePath, string[] dialogues)
    {
        EnemyAttributes enemy = new EnemyAttributes(name, maxHealth, maxMana, atk, def, spd, acc, eva, crit,
            weaponType, weaponName, weaponAtk, weaponDef, weaponSpritePath,
            shieldType, shieldName, shieldAtk, shieldDef, shieldSpritePath,
            helmetType, helmetName, helmetAtk, helmetDef, helmetSpritePath, dialogues);

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
