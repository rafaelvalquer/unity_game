using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("_GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    // Informações do personagem
    public string playerName;
    public CharacterAttributes characterAttributes;
    public CharacterEquipment characterEquipment;
    public EnemyAttributes enemyAttributes;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicialize os atributos do personagem
        characterAttributes = new CharacterAttributes();
        characterEquipment = new CharacterEquipment();
    }
    // Função para equipar um item no CharacterEquipment
    public void EquipItem(string slotName, EquipmentSlot newEquipment)
    {
        switch (slotName)
        {
            case "Weapon":
                characterEquipment.weaponSlot = newEquipment;
                break;
            case "Shield":
                characterEquipment.shieldSlot = newEquipment;
                break;
            case "Helmet":
                characterEquipment.helmetSlot = newEquipment;
                break;
            // Adicione mais casos conforme necessário
        }
    }
}


/*
// Acessar atributos
int playerHP = GameManager.Instance.characterAttributes.currentHP;

// Modificar atributos
GameManager.Instance.characterAttributes.attack = 15;

*/