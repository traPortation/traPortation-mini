using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchState;

public class CameraMove : MonoBehaviour
{
    // 参考：https://qiita.com/shell/items/b8d6bd2c4bd42913189e

    TouchManager touchManager;

    private bool scrollStartFlg = false; // スクロールが始まったかのフラグ
    private Vector2 scrollStartPos = new Vector2(); // スクロールの起点となるタッチポジション

    private static float SCROLL_DISTANCE_CORRECTION = 0.8f; // スクロール距離の調整

    private Vector2 touchPosition_cam = new Vector2(); // タッチポジション初期化
    private Collider2D collide2dObj = null; // タッチ位置にあるオブジェクトの判定の初期化

    // Use this for initialization
    void Start()
    {
        this.touchManager = new TouchManager();
    }

    // Update is called once per frame
    void Update()
    {
        this.touchManager.update();
        TouchManager touchState = this.touchManager.getTouch();

        if (touchState.touchFrag)
        {

            touchPosition_cam = Camera.main.ScreenToWorldPoint(touchState.touchPosition);
            collide2dObj = Physics2D.OverlapPoint(touchPosition_cam);

            if (scrollStartFlg == false && collide2dObj)
            {
                /// <summary>
                /// タッチ位置にオブジェクトがあったらそのオブジェクトを取得する
                /// スクロール移動とオブジェクトタッチの処理を区別するために記載
                /// </summary>
                GameObject obj = collide2dObj.transform.gameObject;
                Debug.Log(obj.name);
            }
            else
            {
                /// <summary>
                /// タッチした場所に何もない場合、スクロールフラグをtrueに
                /// </summary>
                scrollStartFlg = true;
                if (scrollStartPos.x == 0.0f)
                {
                    /// <summary>
                    /// スクロール開始位置を取得
                    /// </summary>
                    scrollStartPos = Camera.main.ScreenToWorldPoint(touchState.touchPosition);
                }
                else
                {
                    Vector2 touchMovePos = touchPosition_cam;
                    if (scrollStartPos.x != touchMovePos.x)
                    {
                        /// <summary>
                        /// 直前のタッチ位置との差を取得する
                        /// </summary>
                        float diffPos_x = SCROLL_DISTANCE_CORRECTION * (touchMovePos.x - scrollStartPos.x);
                        float diffPos_y = SCROLL_DISTANCE_CORRECTION * (touchMovePos.y - scrollStartPos.y);

                        Vector3 pos = this.transform.position;
                        pos.x -= diffPos_x;
                        pos.y -= diffPos_y;

                        this.transform.position = pos;
                        scrollStartPos = touchMovePos;
                    }
                }
            }
        }
        else
        {

            /// <summary>
            /// タッチを離したらフラグを落とし、スクロール開始位置も初期化する 
            /// </summary>
            scrollStartFlg = false;
            scrollStartPos = new Vector2();
        }
    }
}