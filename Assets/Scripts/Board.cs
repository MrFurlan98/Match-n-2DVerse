using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public int height;
    public int width;
    public int offSet;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    public GameObject[] icons = new GameObject[0];
    public GameObject blockIcon;
    public GameObject[,] allicons;
	public float moves;
	public float score = 0;
	public Text pointText;
	public Text moveText;

    // Use this for initialization
    void Start () {
        allTiles = new BackgroundTile[width, height];
        allicons = new GameObject[width, height];
		pointText.text= " " + score;
		moveText.text= " " + moves;
        SetUp();
	}

    private void SetUp()
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<height;j++)
            {
                Vector2 tempPosition = new Vector2(i, j+offSet);
                /*GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";*/
                int iconToUse = Random.Range(0, icons.Length);


                GameObject icon = null;
                if (j == height/2 || i == width/2)
                {
                    icon = Instantiate(blockIcon, tempPosition, Quaternion.identity);
                }
                else
                {
                  icon =  Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);

                }
                icon.GetComponent<Icon>().colunm = j;
                icon.GetComponent<Icon>().row= i;
                icon.transform.parent = this.transform;
                //icon.name = "( " + i + ", " + j + " )";
                allicons[i, j] = icon;
            }
        }
    }
    public void DestroyMatch()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(allicons[i,j]!=null)
                {
					if (allicons[i, j].GetComponent<Icon>().isMatch && !(allicons[i, j].GetComponent<Icon>().medused) && !(allicons[i, j].GetComponent<Icon>().isIndestructable))
                    {
                        Destroy(allicons[i, j]);
                        allicons[i, j] = null;
                    }
                }
            }
        }
        StartCoroutine(DropColumns());
    }
    
    private IEnumerator DropColumns()
    {
        int nullCount = 0;
        int indestructable = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allicons[i, j] == null)
                {
                    nullCount++;
                }
                else if(nullCount>0)
                {
                    if (allicons[i, j].GetComponent<Icon>().isIndestructable)
                    {
                        indestructable++;
                    }
                    else { 
                     // GAMBIARRA DO KRL
                       if((j - (nullCount + indestructable)) >= 0 && (j - (nullCount + indestructable)) < height && allicons[i, j - (nullCount + indestructable) ] != null && allicons[i, j - (nullCount + indestructable) ].GetComponent<Icon>().isIndestructable)
                        {
                          
                            indestructable = 0;
                        }
                        allicons[i, j].GetComponent<Icon>().colunm -= nullCount + indestructable;
                        allicons[i, j] = null;
                    }
                }
            }
            indestructable = 0;
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoard());
    }
    private void RefillBoard()
    {

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(allicons[i,j]==null)
                {
                    Vector2 tempoPosition = new Vector2(i, j+offSet);
                    int iconToUse = Random.Range(0, icons.Length);
                    GameObject newIcon = Instantiate(icons[iconToUse], tempoPosition, Quaternion.identity);
                    allicons[i, j] = newIcon;
                    newIcon.GetComponent<Icon>().colunm = j;
                    newIcon.GetComponent<Icon>().row = i;
                    newIcon.transform.parent = transform;
                }
            }
        }
    }
    private IEnumerator FillBoard()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);
    }

    public void UpdateMove()
    {
		pointText.text= " " + score;
		moves--;

		if (moves >= 0) {
			moveText.text= " " + moves;
		} 
	}


}
