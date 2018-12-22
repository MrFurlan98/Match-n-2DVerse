using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    [SerializeField]
    private GamePlayUI m_pGamePlayUI;

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

    int scoreInst = 0;

    public System.Action TriggerMatch;

    // Use this for initialization
    void Start () {
        allTiles = new BackgroundTile[width, height];
        allicons = new GameObject[width, height];
        UpdateMoveScore();
        SetUp();
	}

    private void SetUp()
    {
        for(int i=0;i<width;i++)
        {
            for(int j=0;j<height;j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                /*GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";*/
                int iconToUse = Random.Range(0, icons.Length);


                GameObject icon = null;
          
            
                 icon =  Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);

                
                icon.GetComponent<Icon>().colunm = j;
                icon.GetComponent<Icon>().row= i;
                icon.transform.parent = this.transform;
                //icon.name = "( " + i + ", " + j + " )";
                allicons[i, j] = icon;
            }
        }
    }

    public bool DestroyMatch()
    {
        if (CheckIfCanDestroy())
        {
            TriggerMatch.Invoke();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allicons[i, j] != null)
                    {
                        if (allicons[i, j].GetComponent<Icon>().isMatch && !(allicons[i, j].GetComponent<Icon>().isIndestructable))
                        {
                            Destroy(allicons[i, j]);
                            allicons[i, j] = null;
                        }
                    }
                }
            }
            StartCoroutine(DropColumns());
            return true;
        }
        return false;
    }

    public void SetIsMatchFalse(int targetX, int targetY)
    {
        allicons[targetX, targetY].GetComponent<Icon>().isMatch = false;

        if (targetX > 0)
        {
            if (allicons[targetX - 1, targetY] != null)
            {
                allicons[targetX - 1, targetY].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetX < width - 1)
        {
            if (allicons[targetX + 1, targetY] != null)
            {
                allicons[targetX + 1, targetY].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetY < height - 1)
        {
            if (allicons[targetX, targetY + 1] != null)
            {
                allicons[targetX, targetY + 1].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetY > 0)
        {
            if (allicons[targetX, targetY - 1] != null)
            {
                allicons[targetX, targetY - 1].GetComponent<Icon>().isMatch = false;
            }
        }

    }

    public bool CheckIfCanDestroy()
    {
        int nrmTitle = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allicons[i, j] != null)
                {
                    if (allicons[i, j].GetComponent<Icon>().isMatch)
                    {
                        nrmTitle++;
                    }
                }
            }
        }
        if (nrmTitle >= 3)
        {
            Scoring(nrmTitle);
            return true;
        }
        return false;
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
                    Vector2 tempoPosition = new Vector2(i, j);
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

    public void UpdateMoveScore()
    {
		m_pGamePlayUI.TScore.text= " " + score;
		moves--;

		if (moves >= 0) {
            m_pGamePlayUI.TMoves.text= " " + moves;
		} 
	}

    public void FindMatch(int targetX, int targetY)
    {
        GameObject tCurrentIcon = allicons[targetX, targetY];
        if (targetX > 0)
        {
            if (allicons[targetX - 1, targetY] != null)
            {
                GameObject leftIcon1 = allicons[targetX - 1, targetY];


                if (leftIcon1.tag == tCurrentIcon.tag)
                {
                    leftIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(leftIcon1);

                }
            }
        }
        if (targetX < width - 1)
        {
            if (allicons[targetX + 1, targetY] != null)
            {
                GameObject rightIcon1 = allicons[targetX + 1, targetY];
                if (rightIcon1.tag == tCurrentIcon.tag)
                {
                    rightIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(rightIcon1);
                }
            }
        }
        if (targetY < height - 1)
        {
            if (allicons[targetX, targetY + 1] != null)
            {
                GameObject upIcon1 = allicons[targetX, targetY + 1];

                if (upIcon1.tag == tCurrentIcon.tag)
                {
                    upIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(upIcon1);

                }

            }
        }
        if (targetY > 0)
        {
            if (allicons[targetX, targetY - 1] != null)
            {
                GameObject downIcon1 = allicons[targetX, targetY - 1];
                if (downIcon1.tag == tCurrentIcon.tag)
                {
                    downIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(downIcon1);

                }
            }
        }
    }

    public void Scoring(int pTitles)
    {
        if (pTitles == 3)
        {
            scoreInst = 100;
        }
        else
        {
            scoreInst = 100 * (pTitles - 2);
        }

        score += scoreInst;
        UpdateMoveScore();
    }


   
}
