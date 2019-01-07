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
    public int currentX;
    public int currentY;
    public int currentDestroyed;


    public float moves;
	public float score = 0;

    int scoreInst = 0;

    public System.Action TriggerMatch;
    // create a queue to do actions before the next match 
    [SerializeField]
    public List<System.Action> m_pActions = new List<System.Action>();

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
                int iconToUse = Random.Range(0, 4);

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
            int tempIconToUse = 6;
            if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "BluePotion")
            {
                tempIconToUse = 6;
            }
            if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "GreenPotion")
            {
                tempIconToUse = 7;
            }
            if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "RedPotion")
            {
                tempIconToUse = 8;
            }
            if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "YellowPotion")
            {
                tempIconToUse = 9;
            }
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
            if (currentDestroyed == 6)
            {
                Vector2 tempPosition = new Vector2(currentX, currentY);
                GameObject newIcon = Instantiate(icons[4], tempPosition, Quaternion.identity);
                allicons[currentX, currentY] = newIcon;
                newIcon.GetComponent<Icon>().colunm = currentY;
                newIcon.GetComponent<Icon>().row = currentX;
                newIcon.transform.parent = transform;
            }
            if (currentDestroyed == 7)
            {
                Vector2 tempPosition = new Vector2(currentX, currentY);
                GameObject newIcon = Instantiate(icons[5], tempPosition, Quaternion.identity);
                allicons[currentX, currentY] = newIcon;
                newIcon.GetComponent<Icon>().colunm = currentY;
                newIcon.GetComponent<Icon>().row = currentX;
                newIcon.transform.parent = transform;
            }
            if (currentDestroyed >=8)
            {
                Vector2 tempPosition = new Vector2(currentX, currentY);
                GameObject newIcon = Instantiate(icons[tempIconToUse], tempPosition, Quaternion.identity);
                allicons[currentX, currentY] = newIcon;
                newIcon.GetComponent<Icon>().colunm = currentY;
                newIcon.GetComponent<Icon>().row = currentX;
                newIcon.transform.parent = transform;
            }
            
            StartCoroutine(FinishMatch());
            return true;
        }
        StartCoroutine(FinishMatch());
        return false;
    }

    public void SetIsMatchFalse(int targetX, int targetY)
    {
        if(allicons[targetX, targetY] != null)
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
        currentDestroyed = nrmTitle;
        if (nrmTitle >= 3)
        {
            Scoring(nrmTitle);
            return true;
        }
        return false;
    }

    private IEnumerator FinishMatch()
    {
        yield return new WaitForEndOfFrame();
        yield return RunActions();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SetIsMatchFalse(i, j);
            }
        }
        yield return DropColumns();
    }

    private IEnumerator RunActions()
    {
        if (m_pActions == null)
            yield break;

        foreach(System.Action tAction in m_pActions)
        {
            tAction.Invoke();
        }
        m_pActions = new List<System.Action>();
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
                    int iconToUse = Random.Range(0, 4);
    
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
        
        // check if  has a match in left of current icon
        if(!CheckSpecialTiles(targetX,targetY))
        {
            if (targetX > 0)
            {
                if (allicons[targetX - 1, targetY] != null)
                {
                    GameObject leftIcon1 = allicons[targetX - 1, targetY];

                    if (leftIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
                    {
                        leftIcon1.GetComponent<Icon>().isMatch = true;
                        //FindMatch(leftIcon1);

                    }
                }
            }
            // check if  has a match in right of current icon
            if (targetX < width - 1)
            {
                if (allicons[targetX + 1, targetY] != null)
                {
                    GameObject rightIcon1 = allicons[targetX + 1, targetY];
                    if (rightIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
                    {
                        rightIcon1.GetComponent<Icon>().isMatch = true;
                        //FindMatch(rightIcon1);
                    }
                }
            }
            // check if  has a match in upward of current icon
            if (targetY < height - 1)
            {
                if (allicons[targetX, targetY + 1] != null)
                {
                    GameObject upIcon1 = allicons[targetX, targetY + 1];

                    if (upIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
                    {
                        upIcon1.GetComponent<Icon>().isMatch = true;
                        //FindMatch(upIcon1);

                    }

                }
            }
            // check if  has a match in downward of current icon
            if (targetY > 0)
            {
                if (allicons[targetX, targetY - 1] != null)
                {
                    GameObject downIcon1 = allicons[targetX, targetY - 1];
                    if (downIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
                    {
                        downIcon1.GetComponent<Icon>().isMatch = true;
                        //FindMatch(downIcon1);

                    }
                }
            }
        }
        if(CheckSpecialTiles(targetX,targetY))
        {
            Destroy(allicons[currentX, currentY]);
            StartCoroutine(FinishMatch());
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

    public void DestroyRow(int pRow)
    {
        for(int j = 0; j < height; j++)
        {
            if(allicons[pRow, j] != null)
            {
                Destroy(allicons[pRow, j]);
            }
        }
    }

    public void DestroyCollum(int pCollum)
    {
        for (int i = 0; i < width; i++)
        {
            if (allicons[i, pCollum] != null)
            {
                Destroy(allicons[i, pCollum]);
            }
        }
    }

    // destroy a area by a position and um integer pArea ex: pArea = 3 then 3x3 in originx, originy was been destroyed 
    public void DestroyArea(int pOriginX, int pOriginY, int pArea)
    {
        for(int i = pOriginX - pArea; i < pOriginX + pArea; i++)
        {
            for(int j = pOriginY - pArea; j < pOriginY + pArea; j++)
            {
                if (IsOutOfBoardRange(i, j))
                    continue; //go to next interation 

                if(allicons[i, j] != null)
                {
                    Destroy(allicons[i, j]);
                }
            }
        }
    }

    public bool IsOutOfBoardRange(int pPositionX, int pPositionY)
    {
       return !(pPositionX >= 0 && pPositionX < width && pPositionY >= 0 && pPositionY < height);
    }

    public bool CheckSpecialTiles(int targetX, int targetY)
    {
        GameObject tCurrentIcon = allicons[targetX, targetY];
        if(tCurrentIcon.GetComponent<Icon>().STag=="6Bomb")
        {
            DestroyRow(targetX);
            DestroyCollum(targetY);
            return true;
        }
        if (tCurrentIcon.GetComponent<Icon>().STag == "7Bomb")
        {
            DestroyArea(targetX,targetY,3);
            return true;
        }
        if (tCurrentIcon.GetComponent<Icon>().STag == "8BombBlue")
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(allicons[i,j].GetComponent<Icon>().STag == "BluePotion")
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
            return true;
        }
        if (tCurrentIcon.GetComponent<Icon>().STag == "8BombGreen")
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
            return true;
        }
        if (tCurrentIcon.GetComponent<Icon>().STag == "8BombRed")
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
            return true;
        }
        if (tCurrentIcon.GetComponent<Icon>().STag == "8BombYellow")
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
            return true;
        }
        return false;
    }
}
