using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScriptTestador : MonoBehaviour
{
    [SerializeField] private InputTextDoCriandoNovoJogo input;

    private void Start()
    {
        SaveDatesManager.s = new SaveDatesManager();
        SaveDatesManager.s.SavedGames = new List<SaveDates>();
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            input.Iniciar();
        }
    }
}
