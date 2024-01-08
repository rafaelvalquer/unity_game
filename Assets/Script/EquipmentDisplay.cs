using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour
{
    public Image weaponImage;
    public Image shieldImage;
    public Image helmetImage;

    private void Start()
    {
        // Exemplo de uso: atualize as imagens iniciais
        UpdateEquipmentImages();
    }

    // Função para atualizar as imagens com base nos equipamentos do personagem
    public void UpdateEquipmentImages()
    {
        CharacterEquipment characterEquipment = GameManager.Instance.characterAttributes.equipment;


        // Atualiza a imagem da arma
        if (characterEquipment.weaponSlot != null)
        {
            UpdateImage(weaponImage, characterEquipment.weaponSlot.spritePath);
        }
        else
        {
            weaponImage.enabled = false;
        }

        // Atualiza a imagem do escudo
        if (characterEquipment.shieldSlot != null)
        {
            UpdateImage(shieldImage, characterEquipment.shieldSlot.spritePath);
        }
        else
        {
            shieldImage.enabled = false;
        }

        // Atualiza a imagem do capacete
        if (characterEquipment.helmetSlot != null)
        {
            UpdateImage(helmetImage, characterEquipment.helmetSlot.spritePath);
        }
        else
        {
            helmetImage.enabled = false;
        }
    }

    // Função para carregar e definir o sprite de uma imagem
    private void UpdateImage(Image image, string spritePath)
    {
        Sprite sprite = Resources.Load<Sprite>(spritePath);

        if (sprite != null)
        {
            image.sprite = sprite;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
            Debug.LogError("Erro ao carregar o sprite: " + spritePath);
        }
    }
}