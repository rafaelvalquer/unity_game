using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class PontosDisponiveis : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    private Text textoComponente;
    public string Atributo;

    void Start()
    {
        textoComponente = GetComponent<Text>();
        AtualizarTexto();
    }

    private int ObterValorDoAtributo(string nomeDoAtributo)
    {
        // Usa Reflection para obter o valor do atributo pelo nome
        FieldInfo field = GameManager.Instance.characterAttributes.GetType().GetField(nomeDoAtributo);

        if (field != null)
        {
            return (int)field.GetValue(GameManager.Instance.characterAttributes);
        }
        else
        {
            Debug.LogError($"Atributo {nomeDoAtributo} não encontrado em CharacterAttributes.");
            return 0; // Ou algum valor padrão, dependendo do contexto.
        }
    }

    public void AtualizarTexto()
    {
        if (Atributo == "characterName")
        {
            Debug.Log($"characterName salvo: {GameManager.Instance.characterAttributes.characterName}");
            Debug.Log($"characterName salvo: {GameManager.Instance.characterAttributes.attack}");
            // Se o atributo for "characterName", exibe diretamente o valor da variável
            textoComponente.text = GameManager.Instance.characterAttributes.characterName;
        } 
        else
        {
            int pontos = ObterValorDoAtributo(Atributo);

            // Atualiza o texto com o valor da variável
            textoComponente.text = pontos.ToString();
        }

    }
}
