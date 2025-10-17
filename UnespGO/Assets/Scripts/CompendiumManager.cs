using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CompendiumManager : MonoBehaviour
{
    // mantém o array visível no inspector (opcional)
    public Button[] buttons;

    // lista interna usada em tempo de execução
    private List<Button> runtimeButtons = new List<Button>();

    void Awake()
    {
        // inicializa a lista com os botões já ligados no inspector (se houver)
        if (buttons != null && buttons.Length > 0)
        {
            runtimeButtons.AddRange(buttons);
        }
        SyncArrayWithList();
    }

    // registra um botão adicionado dinamicamente
    public void RegisterButton(Button b)
    {
        if (b == null) return;
        if (!runtimeButtons.Contains(b))
        {
            runtimeButtons.Add(b);
            SyncArrayWithList();
        }
    }

    // opcional: limpa todos os registros (se precisar)
    public void ClearButtons()
    {
        runtimeButtons.Clear();
        SyncArrayWithList();
    }

    // mantém o array público sincronizado (útil para depuração/inspector)
    private void SyncArrayWithList()
    {
        buttons = runtimeButtons.ToArray();
    }

    public void EnableEntryButton(int id)
    {
        // id assumido 1-based no seu código original
        Debug.Log("Enabling button for entry ID: " + id);
        if (id - 1 < 0 || id - 1 >= runtimeButtons.Count) return;
        runtimeButtons[id - 1].interactable = true;
    }

    public void EnableEntry(int id)
    {
        // o restante do comportamento original permanece (usa entries)
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].SetActive(false);
        }
        entries[id].SetActive(true);
    }

    public GameObject[] entries; 
}

