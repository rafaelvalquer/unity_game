using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class PontosIniciais : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    private Text textoComponente;

    void Start()
    {
        textoComponente = GetComponent<Text>();
        AtualizarPontosIniciais();
    }

    public void AtualizarPontosIniciais()
    {
        int pontos = GameManager.Instance.characterAttributes.pontosIniciais;

        // Atualiza o texto com o valor da variável
        textoComponente.text = pontos.ToString();
    }
}
