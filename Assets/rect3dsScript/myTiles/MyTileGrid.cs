using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MyTileGrid : MonoBehaviour {

    [System.Serializable]
    private struct DictColorTile
    {
        public Color cor;
        public Sprite thisTile;
        public TypeLayer tLayer;
        public float sizeFactorX;
        public float sizeFactorY;
    }

    private enum TypeLayer
    {
        collider,
        noCollider,
        background,
        foreground,
        destructible
    }

    [SerializeField] private Texture2D mapForTile;
    [SerializeField] private List<DictColorTile> dict = new List<DictColorTile>();
    [SerializeField] private Vector2 basePos;
    [SerializeField] private int basePixel=32;
    [SerializeField] private bool listarCores = false;
    [SerializeField] private bool selectbasePixel = false;
    [SerializeField] private bool criarCenario = false;

	// Use this for initialization
	void Start () {
        if (selectbasePixel &&dict.Count>0)
        {
            if (dict[0].thisTile != null)
                basePixel = dict[0].thisTile.texture.width;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (listarCores)
        {
            ListarCores();
            listarCores = false;
        }

        if (criarCenario)
        {
            criarCenario = false;
            CriarCenario();
        }
	}

    void CriarCenario()
    {
        GameObject P = new GameObject();
        P.name = "container";

        for (int i = 0; i < mapForTile.width; i++)
            for (int j = 0; j < mapForTile.height; j++)
                for(int k=0;k<dict.Count;k++)
            {
                    if (mapForTile.GetPixel(i, j) == dict[k].cor && dict[k].thisTile != null)
                    {
                        
                        GameObject G = new GameObject();
                        SpriteRenderer S= G.AddComponent<SpriteRenderer>();

                        G.transform.parent = P.transform;

                        S.sprite = dict[k].thisTile;

                        G.transform.position = new Vector3(i * basePixel, j * basePixel, 0);

                        if (dict[k].tLayer == TypeLayer.collider)
                        {
                            BoxCollider2D b= G.AddComponent<BoxCollider2D>();
                            b.size = new Vector2(1+dict[k].sizeFactorX, 1+dict[k].sizeFactorY);
                            
                        }
                        //Instantiate(dict[k].thisTile, new Vector3(i * basePixel, j * basePixel, 0), Quaternion.identity);
                    }
            }
    }


    void ListarCores()
    {
        List<Color> myColors=new List<Color>();
        dict = new List<DictColorTile>();

        for (int i = 0; i < mapForTile.width; i++)
            for (int j = 0; j < mapForTile.height; j++)
            {
                Color C = mapForTile.GetPixel(i, j);

                if (!myColors.Contains(C))
                {
                    myColors.Add(C);
                    dict.Add(new DictColorTile() { cor = C });
                }
            }
    }
}
