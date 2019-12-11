using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class BuscadorDeEnderecoDeArquivo
{
    [SerializeField] private Object arquivo;
    [SerializeField] private string endereco;

    public string GetPath { get { return endereco; } }
    public string GetFileName { get { return arquivo.name; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnValidate()
    {
#if UNITY_EDITOR
        if (arquivo != null)
            endereco = AssetDatabase.GetAssetPath(arquivo);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
