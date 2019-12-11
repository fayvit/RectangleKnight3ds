using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct JsonMapContainer
{
    public sArray[] a;
    public VetorDoDicionarioDeCores[] V;

    public JsonMapContainer(
        Dictionary<string, Dictionary<string, MyColor>> D1,
        Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2)
    {
        a = D2ToStringArray(D2);
        V = D1ToStringArray(D1);
    }

    public static string GetJSonD2(Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2)
    {
        return JsonUtility.ToJson(D2ToStringArray(D2));
    }

    public static string GetJSonD1(Dictionary<string, Dictionary<string, MyColor>> D1)
    {
        return JsonUtility.ToJson(D1ToStringArray(D1));
    }

    public static string GetJsonObject(
        Dictionary<string, Dictionary<string, MyColor>> D1,
        Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2)
    {
        return new JsonMapContainer(D1, D2).GetJsonObject();
    }

    public string GetJsonObject()
    {
        return JsonUtility.ToJson(this);
    }

    public static VetorDoDicionarioDeCores[] D1ToStringArray(Dictionary<string, Dictionary<string, MyColor>> D1)
    {
        VetorDoDicionarioDeCores[] sa = new VetorDoDicionarioDeCores[D1.Count];
        int cont = 0;
        foreach (string s in D1.Keys)
        {
            sa[cont] = new VetorDoDicionarioDeCores(s, D1[s].Count);

            int cont2 = 0;

            foreach (string ss in D1[s].Keys)
            {
                Color C = D1[s][ss].cor;
                sa[cont].coresCorrespondentes[cont2].nomeDoTile = ss;
                sa[cont].coresCorrespondentes[cont2].cor = new float[] { C.r, C.g, C.b, C.a };

                cont2++;
            }

            cont++;
        }

        return sa;
    }
    public Dictionary<string, Dictionary<string, MyColor>> StringArrayToD1()
    {
        Dictionary<string, Dictionary<string, MyColor>> D1 = new Dictionary<string, Dictionary<string, MyColor>>();
        for (int i = 0; i < V.Length; i++)
        {
            string name = V[i].name;
            D1[name] = new Dictionary<string, MyColor>();
            CorCorrespondenteAoTile[] Vb = V[i].coresCorrespondentes;
            for (int j = 0; j < Vb.Length; j++)
            {
                string vj = Vb[j].nomeDoTile;
                float[] cj = Vb[j].cor;
                D1[name][vj]
                    = new MyColor(new Color(cj[0], cj[1], cj[2], cj[3]));
            }
        }

        return D1;
    }


    public static Dictionary<string, Dictionary<string, MyColor>> StringArrayToD1(string s)
    {
        JsonMapContainer ps = JsonUtility.FromJson<JsonMapContainer>(s);
        return ps.StringArrayToD1();
    }

    public Dictionary<string, Dictionary<MyVector2Int, MyColor>> StringArrayToD2()
    {
        
        Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2 = new Dictionary<string, Dictionary<MyVector2Int, MyColor>>();
        for (int i = 0; i < a.Length; i++)
        {
            string name = a[i].name;
            D2[name] = new Dictionary<MyVector2Int, MyColor>();
            vetArray[] V = a[i].V;
            for (int j = 0; j < V.Length; j++)
            {
                int[] vj = V[j].XY;
                float[] cj = V[j].colors;
                D2[name][new MyVector2Int(vj[0], vj[1])]
                    = new MyColor(new Color(cj[0], cj[1], cj[2], cj[3]));
            }
        }

        return D2;
    }

    public static Dictionary<string, Dictionary<MyVector2Int, MyColor>> StringArrayToD2(string s)
    {
        JsonMapContainer ps = JsonUtility.FromJson<JsonMapContainer>(s);
        return ps.StringArrayToD2();
    }

    public static sArray[] D2ToStringArray(Dictionary<string, Dictionary<MyVector2Int, MyColor>> D2)
    {


        sArray[] sa = new sArray[D2.Count];
        int cont = 0;
        foreach (string s in D2.Keys)
        {
            sa[cont] = new sArray(s, D2[s].Count);

            int cont2 = 0;

            foreach (MyVector2Int ss in D2[s].Keys)
            {
                Color C = D2[s][ss].cor;
                sa[cont].V[cont2].XY = new int[] { ss.x, ss.y };
                sa[cont].V[cont2].colors = new float[] { C.r, C.g, C.b, C.a };

                cont2++;
            }

            cont++;
        }

        return sa;
    }
}

[System.Serializable]
public struct VetorDoDicionarioDeCores
{
    public string name;
    public CorCorrespondenteAoTile[] coresCorrespondentes;

    public VetorDoDicionarioDeCores(string name, int dictCount)
    {
        this.name = name;
        coresCorrespondentes = new CorCorrespondenteAoTile[dictCount];
    }
}

[System.Serializable]
public struct CorCorrespondenteAoTile
{
    public string nomeDoTile;
    public float[] cor;
}

[System.Serializable]
public struct sArray
{
    public string name;
    public vetArray[] V;

    public sArray(string name, int dictCount)
    {
        this.name = name;
        V = new vetArray[dictCount];
    }


}

[System.Serializable]
public struct vetArray
{
    public int[] XY;
    public float[] colors;
}
