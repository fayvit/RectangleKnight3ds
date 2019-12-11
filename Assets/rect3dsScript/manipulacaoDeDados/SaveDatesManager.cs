using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDatesManager
{

    //private static bool estaCarregado = false;

    public static SaveDatesManager s;

    public int IndiceDoJogoAtualSelecionado  = 0;

    public SaveDatesManager()
    {
        s = this;
    }

    public List<PropriedadesDeSave> SaveProps  = new List<PropriedadesDeSave>();

    public List<SaveDates> SavedGames = new List<SaveDates>();

    public idioma ChosenLanguage  = idioma.en_google;

    public SaveDates CurrentSaveDate
    {
        get
        {
            if (SavedGames.Count > IndiceDoJogoAtualSelecionado)
                return SavedGames[IndiceDoJogoAtualSelecionado];
            else
            {
                Debug.Log("nulo");
                return null;
            }
        }
    }

    public static void SalvarAtualizandoDados()
    {
        if (s == null)
            new SaveDatesManager();

        if (s.SavedGames.Count > s.IndiceDoJogoAtualSelecionado)
            s.SavedGames[s.IndiceDoJogoAtualSelecionado] = (new SaveDates());
        else
            s.SavedGames.Add(new SaveDates());

        Save();
        //loadSave.Save(new SaveDates());


    }

    public static void SalvarAtualizandoDados(NomesCenas[] cenasAtivas)
    {
        if (s == null)
            new SaveDatesManager();

        if (s.SavedGames.Count > s.IndiceDoJogoAtualSelecionado)
            s.SavedGames[s.IndiceDoJogoAtualSelecionado] = new SaveDates(cenasAtivas);
        else
            s.SavedGames.Add(new SaveDates(cenasAtivas));
        Save();
        //loadSave.Save(new SaveDates(cenasAtivas));

    }

    public static byte[] SaveDatesForBytes()
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();

        Debug.Log(s.ChosenLanguage + " no momento do save");
        bf.Serialize(ms, s);

        return ms.ToArray();
    }

    public static void CarregaSaveDates(int indice)
    {
        PropriedadesDeSave p = s.SaveProps[indice];

        List<PropriedadesDeSave> lista = s.SaveProps;
        indice = lista.IndexOf(p);

        lista[indice] = new PropriedadesDeSave()
        {
            ultimaJogada = System.DateTime.Now,
            nome = lista[indice].nome,
            indiceDoSave = lista[indice].indiceDoSave
        };

        //salvador.SalvarArquivo("criaturesGames.ori", lista);
        Save();
    }

    public void RemoveSaveDates(int indice)
    {
        List<PropriedadesDeSave> lista;
        PropriedadesDeSave p = SaveProps[indice];

        lista = SaveProps;

        SavedGames[p.indiceDoSave] = null;
        lista.Remove(p);

        Save();

        lista.Sort();
    }

    public static void SetSavesWithBytes(byte[] b)
    {
        MemoryStream ms = new MemoryStream(b);
        BinaryFormatter bf = new BinaryFormatter();
        s = (SaveDatesManager)bf.Deserialize(ms);

        Debug.Log("No Load: " + s.ChosenLanguage);

        Debug.Log("Saves set: " + s.SaveProps.Count + " : " + s.SavedGames.Count);
    }

    public static void Save()
    {

#if UNITY_WEBGL //GameJolt

        if (UrlVerify.DomainsContainString("gamejolt"))
            ForGameJoltDatesManager.Save(s);
#endif
#if UNITY_N3DS
        if (s != null)
        {
            byte[] sb = SaveDatesForBytes();
            preJSON pre = new preJSON() { b = sb };
            UnityEngine.N3DS.FileSystemSave.Mount();

            StreamWriter sw = File.CreateText(Application.persistentDataPath + "/file1");
            sw.WriteLine(JsonUtility.ToJson(pre));
            sw.Close();
            UnityEngine.N3DS.FileSystemSave.Unmount();
        }
#endif
#if !UNITY_N3DS && !UNITY_WEBGL
        if (s != null && !GlobalController.g.EmTeste)
        {
            byte[] sb = SaveDatesForBytes();
            preJSON pre = new preJSON() { b = sb };

            PlayerPrefs.SetString("dates_RK", JsonUtility.ToJson(pre));

            PlayerPrefs.Save();

        }

#endif
    }

    public static void Load()
    {

#if UNITY_WEBGL //GameJolt

        //Colocado posteriormente para corrigir bugs
        new SaveDatesManager();

        if (UrlVerify.DomainsContainString("gamejolt"))
        {
            ForGameJoltDatesManager.Load();
            EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "Url foi sim encontrada"));
        }
        else
        {
            EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "Url não encontrado na segunda chamada"));
        }
#endif
#if UNITY_N3DS
        UnityEngine.N3DS.FileSystemSave.Mount();
        string S2 = string.Empty;
        if (File.Exists(Application.persistentDataPath + "/file1"))
        {
            StreamReader sr = File.OpenText(Application.persistentDataPath + "/file1");
            S2 = sr.ReadLine();
            sr.Close();

        }

        UnityEngine.N3DS.FileSystemSave.Unmount();

        if (!string.IsNullOrEmpty(S2))
        {
            Debug.Log("não é null");
            SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
        }
        else
        {
            Debug.Log("não achou");
            new SaveDatesManager();
        }
        Debug.Log("sou um N3DS");
        //GameObject.FindObjectOfType<LoginJoltManager>().StartCoroutine(Carregado());
#endif

#if !UNITY_N3DS && !UNITY_WEBGL
        string S2 = PlayerPrefs.GetString("dates_RK", string.Empty);

        if (!string.IsNullOrEmpty(S2))
        {

            SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
        }
        else
        {
            Debug.Log("nada encontrado");
            new SaveDatesManager();
        }

        //  GlobalController.g.StartCoroutine(Carregado());
#endif
        //    GlobalController.g.StartCoroutine(Carregado());
    }



    /*
    static IEnumerator Carregado()
    {
        yield return new WaitForEndOfFrame();
        estaCarregado = true;
    }*/
}

[System.Serializable]
public class preJSON
{
    public byte[] b;
}

[System.Serializable]
public struct PropriedadesDeSave : System.IComparable
{
    public string nome;
    public int indiceDoSave;
    public System.DateTime ultimaJogada;

    public int CompareTo(object obj)
    {
        return System.DateTime.Compare(((PropriedadesDeSave)obj).ultimaJogada, ultimaJogada);
    }
}