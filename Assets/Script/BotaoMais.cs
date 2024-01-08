using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
//
public class BotaoMais : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    public string Atributo;
    public PontosDisponiveis pontosDisponiveis;
    public PontosIniciais pontosIniciais;
    private Text txtPontos; // Adicione um campo para o componente de texto

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnBotaoMaisClick);
        
    }

    private void OnBotaoMaisClick()
    {
        if (TemPontosIniciaisDisponiveis())
        {
            AdicionarValorAoAtributo(1);
            SubtrairPontoInicial();
            pontosDisponiveis.AtualizarTexto();
            pontosIniciais.AtualizarPontosIniciais();
        }
        else 
        {
            Debug.Log("Não há mais pontos iniciais disponíveis. Você atingiu o limite.");
            // Adicione aqui qualquer lógica adicional que deseja realizar quando não houver mais pontos iniciais.
        }
    }

    private bool TemPontosIniciaisDisponiveis()
    {
        return GameManager.Instance.characterAttributes.pontosIniciais > 0;
    }

    private void AdicionarValorAoAtributo(int valor)
    {
        int valorAtual = ObterValorDoAtributo(Atributo);
        SetValorDoAtributo(Atributo, valorAtual + valor);
    }

    private void SubtrairPontoInicial()
    {
        int pontosIniciais = ObterValorDoAtributo("pontosIniciais");

        // Verifica se o valor de pontosIniciais é maior que 0 antes de subtrair
        if (pontosIniciais > 0)
        {
            SetValorDoAtributo("pontosIniciais", pontosIniciais - 1);
        }
        else
        {
            Debug.Log("PontosIniciais já estão no valor mínimo permitido.");
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
