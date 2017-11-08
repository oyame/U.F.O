using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

    public GUIText scoreGUI;
    public GUIText highscoreGUI;
    private int score;
    private int highScore;

    void Start()
    {
        score = 0;
        // キーを使って値を取得
        // キーがない場合は第二引数の値を取得
        highScore = PlayerPrefs.GetInt("highScoreKey", 0);
    }

    // スコアの加算
    void AddScore(int s)
    {
        score = score + s;
    }

    void Update()
    {
        // Scoreが現在のハイスコアを上回ったらhighScoreを更新
        if (highScore < score) highScore = score;

        scoreGUI.text = "" + score;
        highscoreGUI.text = "" + highScore;
    }

    public void Save()
    {
        // メソッドが呼ばれたときのキーと値をセットする
        PlayerPrefs.SetInt("highScoreKey", highScore);
        // キーと値を保存
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        // キーを全て消す
        PlayerPrefs.DeleteAll();
    }
}
