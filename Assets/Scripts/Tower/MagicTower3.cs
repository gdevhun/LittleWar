using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower3 : MagicTowerBase
{
    // 스탯 조정
    private void Awake()
    {
        InitTower(400, 1.3f, 200);
    }
}