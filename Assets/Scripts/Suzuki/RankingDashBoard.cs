using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankingDashBoard : MonoBehaviour
{
    private string RankingPrefKey = "50,30,10"; // ローカルに文字列でランキングを保存
    private int RankingNum = 3; // 保持するランキングの順位
    public float[] ranking = new float[3]; // ランキングの値をスコアと参照するための配列
    public Text first_rank; // GUIテキストの一位の欄
    public Text second_rank; // GUIテキストの二位の欄
    public Text third_rank; // GUIテキストの三位の欄

    // ランキングをPlayerPrefsから取得してrankingに格納
    public void getRanking()
    {
        // ローカルに保存した文字列のランキングを取得する
        string _ranking = PlayerPrefs.GetString(RankingPrefKey);

        // 文字列のランキングを配列に分割し格納
        if(_ranking.Length > 0)
        {
            string[] _score = _ranking.Split(","[0]);
            ranking = new float[RankingNum];
            for(int i=0;i<_score.Length && i<RankingNum; i++)
            {
                ranking[i] = float.Parse(_score[i]);
            }
        }
    }

    // 新たにスコアを保持する
    public void saveRanking(float new_score)
    {
        if (!ranking.Equals(string.Empty))
        {
            float _tmp = 0.0f;
            for(int i = 0; i < ranking.Length; i++)
            {
                if(ranking[i] < new_score)
                {
                    _tmp = ranking[i];
                    ranking[i] = new_score;
                    new_score = _tmp;
                }
            }
        }
        else
        {
            ranking[0] = new_score;
        }
        string[] tmpArray = new string[3];
        for (int i = 0; i < ranking.Length; i++)
        {
            tmpArray[i] = ""+ranking[i];
        }
        string ranking_string = string.Join(",",tmpArray);
        PlayerPrefs.SetString(RankingPrefKey, ranking_string);
    }

    public void deleteRanking()
    {
        PlayerPrefs.DeleteKey(RankingPrefKey);
    }

    public void debugmodeRanking()
    {
        PlayerPrefs.SetString(RankingPrefKey, "50,30,10");
    }

    public void screenRanking()
    {
        for(int j = 0; j < ranking.Length; j++)
        {
            string ranking_string = "";
            ranking_string = ranking_string + (j + 1) + "位" + ranking[j];
            if (j == 0) first_rank.text = ranking_string.ToString();
            if (j == 1) second_rank.text = ranking_string.ToString();
            if (j == 2) third_rank.text = ranking_string.ToString();
        }
    }
}
