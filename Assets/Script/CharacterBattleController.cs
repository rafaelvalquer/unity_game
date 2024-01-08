using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippedItem
{
    public enum ItemType { Weapon, Shield, Helmet }

    public ItemType itemType;
}

public class CharacterBattleController : MonoBehaviour
{
    public List<EquippedItem> equippedItems = new List<EquippedItem>();

    // Adicione as referências aos seus SpriteRenderers no Inspector
    public SpriteRenderer weaponSpriteRenderer;
    public SpriteRenderer shieldSpriteRenderer;
    public SpriteRenderer helmetSpriteRenderer;

    // Método para carregar os sprites dos itens equipados
    public void LoadEquippedItems()
    {
        CharacterEquipment characterEquipment = GameManager.Instance.characterAttributes.equipment;

        if (characterEquipment.weaponSlot != null)
        {
            LoadSprite(characterEquipment.weaponSlot.spritePath, weaponSpriteRenderer);
        }
        else
        {
            weaponSpriteRenderer.sprite = null;
        }

        if (characterEquipment.shieldSlot != null)
        {
            LoadSprite(characterEquipment.shieldSlot.spritePath, shieldSpriteRenderer);
        }
        else
        {
            shieldSpriteRenderer.sprite = null;
        }

        if (characterEquipment.helmetSlot != null)
        {
            LoadSprite(characterEquipment.helmetSlot.spritePath, helmetSpriteRenderer);
        }
        else
        {
            helmetSpriteRenderer.sprite = null;
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

    void Update()
    {
        // Chame LoadEquippedItems() no Update após as alterações
        LoadEquippedItems();
    }
}
