using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardAction {
    void Action(int pOriginX, int pOriginY, Icon[,] pIcons);
}
