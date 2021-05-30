using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

/// <summary>
/// とりあえず仮で (ここ大事) 人の仕様を置いておきます
/// わりと適当なので変えたい場合は気軽に言ってください
/// </summary>
public interface IPerson
{
    /// <summary>
    /// 乗り物が駅に着いたときに、駅で待ってる人について呼ばれる
    /// 次の駅を見て乗るか決めるみたいな
    /// </summary>
    /// <param name="nextStation">次の駅</param>
    /// <returns>乗るかどうか</returns>
    bool DecideToRide(BoardNode nextStation);

    /// <summary>
    /// 乗り物が駅に着いたときに、乗り物に乗ってる人について呼ばれる
    /// 次の駅を見て降りるか決めるみたいな
    /// </summary>
    /// <param name="nextStation">次の駅</param>
    /// <returns>降りるかどうか</returns>
    bool DecideToGetOff(BoardNode nextStation);
}
