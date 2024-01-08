using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Image slot5;
    public Image slot6;
    public Text txtQtdSlot1;
    public Text txtQtdSlot2;
    public Text txtQtdSlot3;
    public Text txtQtdSlot4;
    public Text txtQtdSlot5;
    public Text txtQtdSlot6;


    private void Start()
    {

        // Nome, tipo, atributo, caminho do sprite, quantidade
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion1", "Cura", 11, "Item/tile000",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion1", "Cura", 11, "Item/tile000",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion2", "Cura", 11, "Item/tile001",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion3", "Cura", 11, "Item/tile002",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion3", "Cura", 10, "Weapon/3",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion4", "Cura", 10, "Weapon/3",1);
        GameManager.Instance.characterAttributes.AddToInventoryFirstEmptySlot("HealthPotion3", "Cura", 10, "Weapon/3",1);
        // Exemplo de uso: atualize as imagens iniciais
        UpdateInventoryDisplay();
    }

    // Função para atualizar as imagens com base nos itens do inventário do personagem
    public void UpdateInventoryDisplay()
    {
        CharacterAttributes characterAttributes = GameManager.Instance.characterAttributes;

        // Atualiza a imagem do slot 1
        if (characterAttributes.GetInventoryItemBySlot(1) != null)
        {
            UpdateImage(slot1, characterAttributes.GetInventoryItemBySlot(1).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(1).Qtd, txtQtdSlot1);
        }
        else
        {
            slot1.enabled = false;
            txtQtdSlot1.text = "0"; // Se o slot estiver vazio, o texto também deve estar vazio
        }

        // Atualiza a imagem do slot 2
        if (characterAttributes.GetInventoryItemBySlot(2) != null)
        {
            UpdateImage(slot2, characterAttributes.GetInventoryItemBySlot(2).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(2).Qtd, txtQtdSlot2);
        }
        else
        {
            slot2.enabled = false;
        }

        // Atualiza a imagem do slot 3
        if (characterAttributes.GetInventoryItemBySlot(3) != null)
        {
            UpdateImage(slot3, characterAttributes.GetInventoryItemBySlot(3).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(3).Qtd, txtQtdSlot3);
        }
        else
        {
            slot3.enabled = false;
        }

        // Atualiza a imagem do slot 4
        if (characterAttributes.GetInventoryItemBySlot(4) != null)
        {
            UpdateImage(slot4, characterAttributes.GetInventoryItemBySlot(4).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(4).Qtd, txtQtdSlot4);
        }
        else
        {
            slot4.enabled = false;
        }

        // Atualiza a imagem do slot 5
        if (characterAttributes.GetInventoryItemBySlot(5) != null)
        {
            UpdateImage(slot5, characterAttributes.GetInventoryItemBySlot(5).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(5).Qtd, txtQtdSlot5);
        }
        else
        {
            slot5.enabled = false;
        }

        // Atualiza a imagem do slot 6
        if (characterAttributes.GetInventoryItemBySlot(6) != null)
        {
            UpdateImage(slot6, characterAttributes.GetInventoryItemBySlot(6).spritePath);
            AtualizarTexto(characterAttributes.GetInventoryItemBySlot(6).Qtd, txtQtdSlot6);
        }
        else
        {
            slot6.enabled = false;
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
        public void AtualizarTexto(int qtd, Text textoComponente)
    {
            // Atualiza o texto com o valor da variável
            textoComponente.text = qtd.ToString();

    }
}
