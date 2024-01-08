using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuItensManager : MonoBehaviour
{
    public CharacterAttributes characterAttributes;
    [SerializeField] private string nomeDoLevelDeJogo;

    public void Proximo()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

}
