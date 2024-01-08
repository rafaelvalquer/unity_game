using UnityEngine;

[System.Serializable]
public class EquipmentSlot
{
    public string slotName;
    public string itemName;
    public int attackBonus;
    public int defenseBonus;
    public string spritePath;  // Armazena o caminho do sprite como uma string

    public EquipmentSlot(string name, string item, int attack, int defense, string sprite)
    {
        slotName = name;
        itemName = item;
        attackBonus = attack;
        defenseBonus = defense;
        spritePath = sprite;
    }
}

[System.Serializable]
public class CharacterEquipment
{
    public EquipmentSlot weaponSlot;
    public EquipmentSlot shieldSlot;
    public EquipmentSlot helmetSlot;

    // Adicione mais slots conforme necessário
}
