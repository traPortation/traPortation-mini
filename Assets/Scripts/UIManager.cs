using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager manager;
    public Text timeLimitText;
    private float timeLimit = 300f;
    public Text moneyText;
    private int money;
    public Text scoreText;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Utils.NullChecker.Check(this.manager);
    }

    // Update is called once per frame
    void Update()
    {
        money = manager.ManageMoney.money;
        timeLimit -= Time.deltaTime;

        timeLimitText.text = string.Format("{0:#.#} 秒", timeLimit);
        moneyText.text = string.Format("{0} 円", money);
        scoreText.text = string.Format("{0} 点", score);
    }

}
