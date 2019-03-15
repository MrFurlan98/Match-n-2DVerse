using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    STANDBY,
    RUNNING,
    onMatch,
    finishMatch
}

public enum E_Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    STAY
}

public class Board : MonoBehaviour {

    

    [SerializeField]
    private GamePlayUI m_pGamePlayUI;

    public GameState m_CurrentState;
    public int level = 1;
    public int height;
    public int width;
    public int offSet = 0;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    public GameObject[] icons = new GameObject[0];
    public GameObject blockIcon;
    public GameObject[,] allicons;
    public Icon[,] m_Icons;
    public int currentX;
    public int currentY;
    public int currentDestroyed;
    public float swipeAngle = 0;
    private GameObject swapingIcon;


    public float moves;
    public float score = 0;

    int scoreInst = 0;

    public System.Action TriggerMatch;
    // create a queue to do actions before the next match 
    [SerializeField]
    public List<System.Action> m_pActions = new List<System.Action>();


    public Vector2 m_InitPosition;
    


    // Use this for initialization
    void Start() {
        allTiles = new BackgroundTile[width, height];
        allicons = new GameObject[width, height];
        m_CurrentState = GameState.RUNNING;
        UpdateMoveScore();
        SetUp();
    }

    private void Update()
    {
        if (OnMouseDown())
        {
            m_InitPosition = MousePosition();
        }
        if (OnMouseUp())
        {
            Vector2 tEndPosi = MousePosition();

            Vector2Int tIconPosition = FindIcon(m_InitPosition);

            if(tIconPosition.x > -1)
            {
                // swipe 
                E_Direction tDirection = GetDirection(m_InitPosition, tEndPosi);

                if(tDirection == E_Direction.STAY)
                {

                }
            }
          
        }

    }

    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                /*GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";*/
                int iconToUse = Random.Range(0, 4);

                GameObject icon = null;


                icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);


                icon.GetComponent<Icon>().colunm = j;
                icon.GetComponent<Icon>().row = i;
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
            if (currentDestroyed >= 8)
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
        if (allicons[targetX, targetY] != null)
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

    public IEnumerator FinishMatch()
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

        foreach (System.Action tAction in m_pActions)
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
                else if (nullCount > 0)
                {
                    if (allicons[i, j].GetComponent<Icon>().isIndestructable)
                    {
                        indestructable++;
                    }
                    else {
                        // GAMBIARRA DO KRL
                        if ((j - (nullCount + indestructable)) >= 0 && (j - (nullCount + indestructable)) < height && allicons[i, j - (nullCount + indestructable)] != null && allicons[i, j - (nullCount + indestructable)].GetComponent<Icon>().isIndestructable)
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
                if (allicons[i, j] == null)
                {
                    Vector2 tempoPosition = new Vector2(i, j);
                    int iconToUse = Random.Range(0, 4);

                    GameObject newIcon = Instantiate(icons[iconToUse], tempoPosition, Quaternion.identity);
                    allicons[i, j] = newIcon;
                    newIcon.GetComponent<Icon>().colunm = j;
                    newIcon.GetComponent<Icon>().row = i;
                    newIcon.transform.parent = transform;
                }
                else
                {
                    allicons[i, j].GetComponent<Icon>().previousColunm = j;
                    allicons[i, j].GetComponent<Icon>().previousRow = i;
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
        m_pGamePlayUI.TScore.text = " " + score;
        moves--;

        if (moves >= 0) {
            m_pGamePlayUI.TMoves.text = " " + moves;
        }
    }

    public void FindMatch(int targetX, int targetY)
    {
        GameObject tCurrentIcon = allicons[targetX, targetY];

        // check if  has a match in left of current icon
        if (!allicons[targetX, targetY].GetComponent<Icon>().isSpecial)
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
        if (allicons[targetX, targetY].GetComponent<Icon>().isSpecial)
        {
            SpecialEffect(targetX, targetY);
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

    public void DestroyRow(int pRow, int targetY)
    {
        allicons[pRow, targetY].GetComponent<Icon>().isSpecial = false;
        for (int j = 0; j < height; j++)
        {
            if (allicons[pRow, j] != null)
            {
                if (allicons[pRow, j] == allicons[pRow, targetY])
                {
                    Destroy(allicons[pRow, j]);
                }
                else if (allicons[pRow, j].GetComponent<Icon>().isSpecial)
                {
                    SpecialEffect(pRow, j);
                }
                else
                {
                    Destroy(allicons[pRow, j]);
                }
            }
        }
    }

    public void DestroyCollum(int targetX, int pCollum)
    {
        allicons[targetX, pCollum].GetComponent<Icon>().isSpecial = false;
        for (int i = 0; i < width; i++)
        {
            if (allicons[i, pCollum] != null)
            {
                if (allicons[i, pCollum] == allicons[targetX, pCollum])
                {
                    Destroy(allicons[i, pCollum]);
                }
                else if (allicons[i, pCollum].GetComponent<Icon>().isSpecial)
                {
                    SpecialEffect(i, pCollum);
                }
                else
                {
                    Destroy(allicons[i, pCollum]);
                }
            }
        }
    }

    // destroy a area by a position and um integer pArea ex: pArea = 3 then 3x3 in originx, originy was been destroyed 
    public void DestroyArea(int pOriginX, int pOriginY, int pArea)
    {
        allicons[pOriginX, pOriginY].GetComponent<Icon>().isSpecial = false;
        for (int i = pOriginX - pArea; i <= pOriginX + pArea; i++)
        {
            for (int j = pOriginY - pArea; j <= pOriginY + pArea; j++)
            {
                if (IsOutOfBoardRange(i, j))
                {
                    continue; //go to next interation 
                }

                if (allicons[i, j] != null)
                {
                    if (allicons[i, j] == allicons[pOriginX, pOriginY])
                    {
                        Destroy(allicons[i, j]);
                    }
                    else if (allicons[i, j].GetComponent<Icon>().isSpecial)
                    {
                        SpecialEffect(i, j);
                    }
                    else
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
        }
    }

    public bool IsOutOfBoardRange(int pPositionX, int pPositionY)
    {
        return !(pPositionX >= 0 && pPositionX < width && pPositionY >= 0 && pPositionY < height);
    }


    public void DoCombo(int targetX, int targetY, int originX, int originY)
    {
        GameObject origin = allicons[originX, originY];
        GameObject second = allicons[targetX, targetY];
        origin.GetComponent<Icon>().isSpecial = false;
        second.GetComponent<Icon>().isSpecial = false;
        if (origin.GetComponent<Icon>().STag == "6Bomb")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                DestroyRow(originX, originY);
                DestroyCollum(originX, originY);
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                DestroyRow(originX + 1, originY);
                DestroyRow(originX, originY);
                DestroyRow(originX - 1, originY);
                DestroyCollum(originX, originY + 1);
                DestroyCollum(originX, originY);
                DestroyCollum(originX, originY - 1);
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombGreen")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombRed")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
        }
        if (origin.GetComponent<Icon>().STag == "7Bomb")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                DestroyRow(originX + 1, originY);
                DestroyRow(originX, originY);
                DestroyRow(originX - 1, originY);
                DestroyCollum(originX, originY + 1);
                DestroyCollum(originX, originY);
                DestroyCollum(originX, originY - 1);
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                DestroyArea(originX, originY, 3);
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombGreen")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombRed")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
        }
        if (origin.GetComponent<Icon>().STag == "8BombBlue")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
        }
        if (origin.GetComponent<Icon>().STag == "8BombGreen")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
        }
        if (origin.GetComponent<Icon>().STag == "8BombRed")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
        }
        if (origin.GetComponent<Icon>().STag == "8BombYellow")
        {
            if (second.GetComponent<Icon>().STag == "6Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
                        {
                            int direction = Random.Range(0, 2);
                            if (direction == 1)
                            {
                                DestroyRow(i, j);
                            }
                            else if (direction == 0)
                            {
                                DestroyCollum(i, j);
                            }
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "7Bomb")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
                        {
                            DestroyArea(i, j, 1);
                        }
                    }
                }
            }
            else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Destroy(allicons[i, j]);
                    }
                }
            }
        }
        if (origin != null)
        {
            Destroy(allicons[originX, originY]);
        }
        if (second != null)
        {
            Destroy(allicons[targetX, targetY]);
        }
        StartCoroutine(FinishMatch());
    }

    public void SpecialEffect(int targetX, int targetY)
    {
        GameObject tCurrentIcon = allicons[targetX, targetY];
        if (tCurrentIcon != null)
        {
            if (tCurrentIcon.GetComponent<Icon>().STag == "6Bomb")
            {
                //Destroy(allicons[targetX,targetY]);
                int direction = Random.Range(0, 2);
                if (direction == 1)
                {
                    DestroyRow(targetX, targetY);
                }
                else if (direction == 0)
                {
                    DestroyCollum(targetX, targetY);
                }
            }
            else if (tCurrentIcon.GetComponent<Icon>().STag == "7Bomb")
            {
                //Destroy(allicons[targetX, targetY]);
                DestroyArea(targetX, targetY, 1);
            }
            else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombBlue")
            {
                Destroy(allicons[targetX, targetY]);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
                        {
                            Destroy(allicons[i, j]);
                        }
                    }
                }
            }
            else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombGreen")
            {
                Destroy(allicons[targetX, targetY]);
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
            }
            else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombRed")
            {
                Destroy(allicons[targetX, targetY]);
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
            }
            else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombYellow")
            {
                Destroy(allicons[targetX, targetY]);
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
            }
        }
    }

    bool OnMouseDown()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    bool OnMouseUp()
    {
        return Input.GetKeyUp(KeyCode.Mouse0);
    }

    Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2Int FindIcon(Vector2 pPosi)
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                Vector2 tCurrentPosition = Vector2.one;
                tCurrentPosition.x *= i;
                tCurrentPosition.y *= j;
                float tDistance = Vector2.Distance(tCurrentPosition, pPosi);
                if (tDistance < 0.5)
                    return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(-1, -1);
    }

    public E_Direction GetDirection(Vector2 pInitPosi, Vector2 pEndPosi)
    {
        if (Vector2.Distance(pInitPosi, pEndPosi) < 0.5)
            return E_Direction.STAY;

        float tAngle = Mathf.Atan2(pEndPosi.y - pInitPosi.y, pEndPosi.x - pInitPosi.x) * 180 / Mathf.PI;

        if (tAngle > -45 && tAngle <= 45)//right swap
        {
            return E_Direction.RIGHT;
        }
        else if (tAngle > 45 && tAngle <= 135)//up swap
        {
            return E_Direction.UP;
        }
        else if ((tAngle > 135 || tAngle <= -135))//left swap
        {
            return E_Direction.LEFT;
        }
        else //if (swipeAngle < -45 && swipeAngle >= -135 && colunm > 0)//down swap
        {
            return E_Direction.DOWN;
        }
        
    }

    public IEnumerator BoardRunning(Vector2Int pInitPosi, E_Direction pDirection)
    {
        m_CurrentState = GameState.RUNNING;


        yield return 0;

        m_CurrentState = GameState.STANDBY;
    }
}
