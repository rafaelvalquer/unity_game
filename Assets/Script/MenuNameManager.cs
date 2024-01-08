using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuNameManager : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelName;
    [SerializeField] private TMP_InputField inputName; // Ajuste para TMP_InputField
    [SerializeField] private TextMeshProUGUI textNomeSalvo; // Arraste o componente TMP Text para este campo no Unity Editor



    public void Proximo()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirName()
    {
        painelMenuInicial.SetActive(false);
        painelName.SetActive(true);
    }
        public void SalvarName()
    {
         // Obter o texto do InputField e atribuir à variável characterName
        GameManager.Instance.characterAttributes.characterName = inputName.text;
        Debug.Log($"characterName salvo: {GameManager.Instance.characterAttributes.characterName}");
        Debug.Log($"characterName salvo: {GameManager.Instance.characterAttributes.attack}");
        painelName.SetActive(false);
        painelMenuInicial.SetActive(true);
         // Atualiza o TextMeshProUGUI com o nome salvo
        textNomeSalvo.text = "Nome do Personagem: " + GameManager.Instance.characterAttributes.characterName;
    }
}
