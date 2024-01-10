using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class InventorySlot
{
    public int slotNumber;
    public string itemName;
    public string itemType;
    public int attributeValue;
    public string spritePath;
    public int Qtd;
}

public class CharacterAttributes
{
    public string characterName;
    public int pontosIniciais;
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
    public string nextEnemyName;

    public CharacterEquipment equipment;

    
    public List<InventorySlot> inventorySlots = new List<InventorySlot>
    {
        new InventorySlot(),
        new InventorySlot(),
        new InventorySlot(),
        new InventorySlot(),
        new InventorySlot(),
        new InventorySlot()
    };

        // Adiciona um item ao inventário
    public void AddToInventory(string itemName, string itemType, int attributeValue, string spritePath, int slotNumber)
    {
        InventorySlot inventorySlot = inventorySlots.Find(slot => slot.slotNumber == slotNumber);

        if (inventorySlot != null)
        {
            // Atualiza o slot se ele já existir
            inventorySlot.itemName = itemName;
            inventorySlot.itemType = itemType;
            inventorySlot.attributeValue = attributeValue;
            inventorySlot.spritePath = spritePath;
        }
        else
        {
            // Se não existir, cria um novo
            inventorySlot = new InventorySlot
            {
                itemName = itemName,
                itemType = itemType,
                attributeValue = attributeValue,
                spritePath = spritePath,
                slotNumber = slotNumber
            };

            inventorySlots.Add(inventorySlot);
        }
    }


    // Adiciona um item ao inventário no primeiro slot vazio ou acumula a quantidade se o item já existe
    public void AddToInventoryFirstEmptySlot(string itemName, string itemType, int attributeValue, string spritePath, int qtd)
    {
        // Tenta encontrar um slot existente com o mesmo item
        InventorySlot existingSlot = inventorySlots.Find(slot => slot.itemName == itemName);

        if (existingSlot != null)
        {
            // Se o item já existe, acumula a quantidade
            existingSlot.Qtd += qtd;
        }
        else
        {
            // Se não existe, encontra o primeiro slot vazio
            InventorySlot emptySlot = inventorySlots.Find(slot => string.IsNullOrEmpty(slot.itemName));

            // Se encontrou um slot vazio, adiciona o item
            if (emptySlot != null)
            {
                emptySlot.itemName = itemName;
                emptySlot.itemType = itemType;
                emptySlot.attributeValue = attributeValue;
                emptySlot.spritePath = spritePath;
                emptySlot.Qtd = qtd;
                // Define o slotNumber com base no índice do slot vazio + 1
                emptySlot.slotNumber = inventorySlots.IndexOf(emptySlot) + 1;
            }
            else
            {
                Debug.LogWarning("Inventário cheio. Não foi possível adicionar o item.");
            }
        }
    }

    // Remove um item do inventário
    public void RemoveFromInventory(string itemName)
    {
        InventorySlot inventorySlot = inventorySlots.Find(slot => slot.itemName == itemName);

        if (inventorySlot != null)
        {
            inventorySlots.Remove(inventorySlot);
        }
    }

        // Obtém um item do inventário pelo nome
    public InventorySlot GetInventoryItem(string itemName)
    {
        return inventorySlots.Find(slot => slot.itemName == itemName);
    }

    // Método para obter um item do inventário pelo número do slot
public InventorySlot GetInventoryItemBySlot(int slotNumber)
{
    return inventorySlots.Find(slot => slot.slotNumber == slotNumber);
}

    // Adicione mais atributos conforme necessário

    public CharacterAttributes()
    {
        // Inicialize os atributos com valores padrão ou desejados
        characterName = "Rafael";

        pontosIniciais = 10;
        maxHP = 100;
        currentHP = maxHP;

        maxMana = 50;
        currentMana = maxMana;

        attack = 8;
        defense = 5;
        speed = 5;
        accuracy = 5;
        evasion = 5;
        critical = 5;
        nextEnemyName = "Orc";

        // Inicialize os equipamentos
        equipment = new CharacterEquipment();
        equipment.weaponSlot = new EquipmentSlot("Weapon", "BasicSword", 2, 0, "Weapon/1");
        equipment.shieldSlot = new EquipmentSlot("Shield", "WoodenShield", 0, 2, "Shield/7");
        equipment.helmetSlot = new EquipmentSlot("Helmet", "LeatherHelmet", 0, 1, "Helmet/Hat-Helmet");
        
        // Adicione mais equipamentos conforme necessário
    }
}







/*
// Adiciona um item ao inventário
GameManager.Instance.characterAttributes.AddToInventory("HealthPotion", "Cura", 10, "Sprites/HealthPotion");

// Remove um item do inventário
GameManager.Instance.characterAttributes.RemoveFromInventory("HealthPotion");

// Obtém um item do inventário pelo nome
InventorySlot item = GameManager.Instance.characterAttributes.GetInventoryItem("HealthPotion");

// Agora, você pode acessar as propriedades do item
string itemName = item.itemName;
string itemType = item.itemType;
int attributeValue = item.attributeValue;
string spritePath = item.spritePath;

*/