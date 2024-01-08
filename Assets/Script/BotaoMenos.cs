using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class BotaoMenos : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    public string Atributo;
    public PontosDisponiveis pontosDisponiveis;
    public PontosIniciais pontosIniciais;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnBotaoMenosClick);
    }

    private void OnBotaoMenosClick()
    {
        SubtrairValorDoAtributo(1);
        pontosDisponiveis.AtualizarTexto();
        pontosIniciais.AtualizarPontosIniciais();
    }
    private void AdicionarPontoInicial()
    {
        int pontosIniciais = GameManager.Instance.characterAttributes.pontosIniciais;
        SetValorDoAtributo("pontosIniciais", pontosIniciais + 1);
    }

    private void SubtrairValorDoAtributo(int valor)
    {
        int valorAtual = ObterValorDoAtributo(Atributo);

        // Garante que o atributo não caia abaixo de 5
        if (valorAtual > 5)
        {
            SetValorDoAtributo(Atributo, valorAtual - valor);
        }
        else
        {
            Debug.Log($"O atributo {Atributo} já atingiu o valor mínimo permitido.");
        }
        
        if (valorAtual > 5)
        {
            AdicionarPontoInicial();
        }
    }

    private int ObterValorDoAtributo(string nomeDoAtributo)
    {
        FieldInfo field = GameManager.Instance.characterAttributes.GetType().GetField(nomeDoAtributo);
        if (field != null)
        {
            return (int)field.GetValue(GameManager.Instance.characterAttributes);
        }
        else
        {
            Debug.LogError($"Atributo {nomeDoAtributo} não encontrado em CharacterAttributes.");
            return 0;
        }
    }

    private void SetValorDoAtributo(string nomeDoAtributo, int valor)
    {
        FieldInfo field = GameManager.Instance.characterAttributes.GetType().GetField(nomeDoAtributo);
        if (field != null)
        {
            field.SetValue(GameManager.Instance.characterAttributes, valor);
        }
        else
        {
            Debug.LogError($"Atributo {nomeDoAtributo} não encontrado em CharacterAttributes.");
        }
    }
}
