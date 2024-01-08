using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuBattleManager : MonoBehaviour
{
    public GameObject panelAcao;
    public Button botaoAtaque;
    public Button botaoDefesa;
    public Slider barraVida;
    private int vida;
    public List<EnemyAttributes> enemiesList = new List<EnemyAttributes>();
    // Referência ao script do inimigo
    private Enemy enemyScript;
    // Referência ao script do Player
    private Player playerScript;

    private void Start()
    {
        
        vida = GameManager.Instance.characterAttributes.maxHP;
        Debug.Log(vida);
        barraVida.maxValue = vida;
        barraVida.value = vida;

        // Obtenha a referência ao script do inimigo
        enemyScript = FindObjectOfType<Enemy>();

        // Obtenha a referência ao Player
        playerScript = FindObjectOfType<Player>();


        // Configura os métodos chamados pelos botões
        botaoAtaque.onClick.AddListener(ExecutarAcaoAtaque);
        botaoDefesa.onClick.AddListener(ExecutarAcaoDefesa);

        // Garante que o painel de ação está desabilitado no início
        DesabilitarPanelAcao();
        InitializeEnemies();


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
        barraVida.value -= damage;

        // Adicione qualquer lógica adicional aqui, como verificar se o jogador morreu
    }

    // Adicione inimigos à lista
    private void InitializeEnemies()
    {
        AddEnemy("Orc", 100, 50, 5, 5, 5, 5, 5, 5,
                "Weapon", "BasicSword", 55, 0, "Weapon/1",
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

    
}
