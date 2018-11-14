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
    public GameObject[] icons;
    public GameObject[,] allicons;
	public float moves;
	public float score = 0;
	public Text pointText;
	public Text moveText;

    // Use this for initialization
    void Start () {
        allTiles = new BackgroundTile[width, height];
        allicons = new GameObject[width, height];
		pointText.text= "Points: " + score;
		moveText.text= "Moves left: " + moves;
        SetUp();
	}

    private void SetUp()
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<height;j++)
            {
                Vector2 tempPosition = new Vector2(i, j+offSet);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";
                int iconToUse = Random.Range(0, icons.Length);


                GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                icon.GetComponent<Icon>().colunm = j;
                icon.GetComponent<Icon>().row= i;
                icon.transform.parent = this.transform;
                icon.name = "( " + i + ", " + j + " )";
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
					if (allicons[i, j].GetComponent<Icon>().isMatch && !(allicons[i, j].GetComponent<Icon>().medused))
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
                    allicons[i, j].GetComponent<Icon>().colunm -= nullCount;
                    allicons[i, j] = null;
                }
            }
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
		pointText.text= "Points: " + score;
		moves--;

		if (moves >= 0) {
			moveText.text= "Moves left: " + moves;
		} 
	}


}
