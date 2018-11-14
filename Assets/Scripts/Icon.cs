using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {

    public int colunm;
    public int row;
    private Board board;
    public int targetX;
    public int targetY;
    public bool isMatch = false;
    private Vector2 tempPosition;
	int nrmTitle = 0;
	int scoreInst = 0;
	public bool medused=false;

	private SpriteRenderer theSpriteRenderer;
	public Sprite medusaTitle;

    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
        /*targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetX;
        colunm = targetY;*/
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
    }
    void Update()
    {
        if(isMatch)
        {
            FindMatch();
        }
        targetY = colunm;
        targetX = row;
        if(Mathf.Abs(targetY-transform.position.y)>.1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if(board.allicons[row,colunm]!=this.gameObject)
            {
                board.allicons[row, colunm] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            
        }
    }
    private void OnMouseDown()
    {
        isMatch = true;
    }
    private void OnMouseUp()
    {
        if (CheckIfCanDestroy())
        {
            Scoring();
            board.DestroyMatch();
        }
        else
        {
            SetIsMatchFalse();
			theSpriteRenderer.sprite = medusaTitle;
			medused = true;

        }
    }
    void FindMatch()
    {
        if (targetX > 0)
        {
            if(board.allicons[targetX-1,targetY]!=null)
            {
                GameObject leftIcon1 = board.allicons[targetX - 1, targetY];


                if (leftIcon1.tag == this.gameObject.tag)
                {
                    leftIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(leftIcon1);

                }
            }
        }
        if (targetX < board.width - 1) 
        {
            if (board.allicons[targetX + 1, targetY] != null)
            {
                GameObject rightIcon1 = board.allicons[targetX + 1, targetY];
                if (rightIcon1.tag == this.gameObject.tag)
                {
                    rightIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(rightIcon1);
                }
            }
        }
        if (targetY < board.height - 1) 
        {
            if (board.allicons[targetX, targetY + 1] != null)
            {
                GameObject upIcon1 = board.allicons[targetX, targetY + 1];

                if (upIcon1.tag == this.gameObject.tag)
                {
                    upIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(upIcon1);

                }

            }
        }
        if (targetY > 0)
        {
            if (board.allicons[targetX, targetY - 1] != null)
            {
                GameObject downIcon1 = board.allicons[targetX, targetY - 1];
                if (downIcon1.tag == this.gameObject.tag)
                {
                    downIcon1.GetComponent<Icon>().isMatch = true;
                    //FindMatch(downIcon1);

                }
            }
        }
    }
    public bool CheckIfCanDestroy()
    {
        nrmTitle = 0;
        for(int i=0; i <board.width;i++)
        {
            for(int j=0; j<board.height;j++)
            {
                if(board.allicons[i,j]!=null)
                {
					if (board.allicons[i, j].GetComponent<Icon>().isMatch && !medused)
                    {
                        nrmTitle++;
                    }
                }
            }
        }
		if(nrmTitle>=3)
        {
            return true;
        }
        return false;
    }
    private void SetIsMatchFalse()
    {
        isMatch = false;
        if (targetX > 0)
        {
            if (board.allicons[targetX - 1, targetY] != null)
            {
                board.allicons[targetX - 1, targetY].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetX < board.width - 1)
        {
            if (board.allicons[targetX + 1, targetY] != null)
            {
                board.allicons[targetX + 1, targetY].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetY < board.height - 1)
        {
            if (board.allicons[targetX, targetY + 1] != null)
            {
                board.allicons[targetX, targetY + 1].GetComponent<Icon>().isMatch = false;
            }
        }
        if (targetY > 0)
        {
            if (board.allicons[targetX, targetY - 1] != null)
            {
                board.allicons[targetX, targetY - 1].GetComponent<Icon>().isMatch = false;
            }
        }
    }
    public void Scoring ()
    {
		if (nrmTitle == 3) {
			scoreInst = 100;
		} else 
		{
			scoreInst = 100 * (nrmTitle - 2);
		}
			
		board.score += scoreInst;
		board.UpdateMove();
    }
}
