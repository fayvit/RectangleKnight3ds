using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveMap
{
    public static void Salvar(Texture2D tex,string nomeDoMapa = "teste")
    {
        var bytes = tex.EncodeToPNG();
//        Debug.Log(Application.dataPath + " : " + Application.persistentDataPath + " : " + Application.consoleLogPath + " : " + Application.streamingAssetsPath + " :" + Application.temporaryCachePath);
        var file = File.Open(Application.dataPath +"/" +nomeDoMapa+".png" , FileMode.Create);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }

    public static void SalvarJsonInTexto(string conteudo,string path)
    {
        StreamWriter sw = File.CreateText(path) ;

        sw.Write(conteudo);

        sw.Close();
    }

    public static string CarregarArquivoParaJson(string path)
    {
        StreamReader x;
        
        x = File.OpenText(path);
        string conteudo = "";
        while (x.EndOfStream != true)
        {
            string linha = x.ReadLine();
            Debug.Log(linha);
            conteudo += linha;
        }
        x.Close();

        return conteudo;
    }

    public static void SalvarPlusMap(ContainerPlusMap plus, string nomeArquivo = "teste")
    {
        var binary = new BinaryFormatter();
        var file = File.Open(Application.dataPath + "/" + nomeArquivo + ".plm", FileMode.Create);
        binary.Serialize(file, plus);
        file.Close();
    }

    public static ContainerPlusMap CarregarPlusMap(string nomeArquivo)
    {
        var binary = new BinaryFormatter();
        var file = File.Open(nomeArquivo, FileMode.Open);
        ContainerPlusMap pMap = (ContainerPlusMap)binary.Deserialize(file);

        return pMap;
    }

}