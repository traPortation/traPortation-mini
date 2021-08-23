using BoardElements;

/// <summary>
/// Personに (将来的に) 実装する機能
/// </summary>
public interface IPerson
{
    /// <summary>
    /// 駅で待ってる人について呼ばれる
    /// 次の駅を見て乗るか決める
    /// </summary>
    /// <param name="nextStation">次の駅</param>
    /// <returns>乗るかどうか</returns>
    bool DecideToRide(Vehicle vehicle);

    /// <summary>
    /// 乗り物に乗る処理
    /// </summary>
    /// <param name="vheicle"></param>
    void Ride(Vehicle vheicle);

    /// <summary>
    /// 乗り物に乗ってる人について呼ばれる
    /// 降りるか決める
    /// </summary>
    /// <param name="nextStation">次の駅</param>
    /// <returns>降りるかどうか</returns>
    bool DecideToGetOff(BoardNode station);
}
