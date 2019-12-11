using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContainerPlusMap
{

    private Dictionary<string, Dictionary<string, MyColor>> D1
        = new Dictionary<string, Dictionary<string, MyColor>>();

    //string json;

    
    private Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2
        = new Dictionary<string, Dictionary<MyVector2Int, MyColor>>();


    public ContainerPlusMap(JsonMapContainer j)
    {
        D1 = j.StringArrayToD1();
        D2 = j.StringArrayToD2();
    }


    /*
    public ContainerPlusMap(
        Dictionary<string, Dictionary<string, MyColor>>  D1,
        Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2)
    {
        
        dicionarioDeCores = D1;

        string json = JsonMapContainer.GetJsonObject(D1,D2);

        SaveMap.SalvarJsonInTexto(json, Application.dataPath + "/"+nomeParaArquivo+".txt");

        Debug.Log(json); 

        // dicionarioParaImagens = D2;
    }*/

   



    /*
    void CriarCenario(string s, string titleName)
    {
        string path = Application.dataPath+"/"+ titleName + "_" + s + ".png";

        Texture2D t = GetTexturebyName(s,titleName);

        Debug.Log("Essa é a textura: "+t.width);

        GameObject P = new GameObject();

        P.name = "container" + s;

        for (int i = 0; i < t.width; i++)
            for (int j = 0; j < t.height; j++)
            {
                string tileName = GetTileNameWithColor(s, t.GetPixel(i, j));
                
                if (tileName != string.Empty)
                {

                    Debug.Log("Essse é um tileName: "+tileName);

                    GameObject G = new GameObject();
                    SpriteRenderer S = G.AddComponent<SpriteRenderer>();

                    G.transform.parent = P.transform;
                    string[] name = tileName.Split('_');

                    if (name.Length == 2)
                    {
                        Sprite[] ss = Resources.LoadAll<Sprite>(name[0]);
                        int index = int.Parse(name[1]);
                        if (ss.Length > index)
                            S.sprite = ss[index];
                        else
                            Debug.Log(tileName);
                    }

                    G.transform.position = new Vector3(i, j, 0);
                }

            }
                
    }*/

    public void CriarCenario(string s,int xvar,int yvar)
    {
        
        GameObject P = new GameObject();

        P.name = "container" + s;


        foreach (MyVector2Int V in D2[s].Keys)
        {
            GameObject G = new GameObject();
            SpriteRenderer S = G.AddComponent<SpriteRenderer>();

            string tileName = GetTileNameWithColor(s, D2[s][V].cor);
            G.transform.parent = P.transform;
            string[] name = tileName.Split('_');

            if (name.Length == 2)
            {
                Sprite[] ss = Resources.LoadAll<Sprite>(name[0]);
                int index = int.Parse(name[1]);
                if (ss.Length > index)
                    S.sprite = ss[index];
                else
                    Debug.Log(tileName);
            }

            G.transform.position = new Vector3(V.x*xvar, V.y*xvar, 0);
        }
        
    }

    public void CreateScenario(string titleName,int xvar,int yvar)
    {
        foreach (string s in D1.Keys)
        {
            //  CriarCenario(s,titleName);
            CriarCenario(s,xvar,yvar);
        }
    }

    string GetTileNameWithColor(string tileName, Color color)
    {
        foreach (string s in D1[tileName].Keys)
        {
            if (D1[tileName][s].cor == color)
                return s;
        }

        if(color!=Color.white)
            Debug.LogWarning("Nome não encontrado no dicionario de cores: "+color);
        return string.Empty;
    }

    public void MostrarCoresDoDicionario()
    {
        foreach (string s in D1.Keys)
            foreach (string ss in D1[s].Keys)
                Debug.Log(s + " : " + ss + " : " + D1[s][ss].cor);
    }

}



