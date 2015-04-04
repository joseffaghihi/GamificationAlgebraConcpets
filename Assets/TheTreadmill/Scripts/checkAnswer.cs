using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class checkAnswer : MonoBehaviour 
{
    public GameObject correctCoinEffect;
    public GameObject falseCoinEffect;

    public void OnCollisionEnter(Collision collision)
    {
        int coinNum = GetComponentInChildren<RandomNumberGenerator>().GetRandomNumber();
        int equationNum = GameObject.Find("Equation").GetComponent<EquationGenerator>().GetRandomNumber();

        if (collision.gameObject.tag == "Player" && Mathf.Abs(coinNum) == Mathf.Abs(equationNum) && coinNum != equationNum)
        {
            GameControl gameController = new GameControl();
            gameController.GameWon();
            (Instantiate(correctCoinEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject).transform.parent =
                            GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            GameControl gameController = new GameControl();
            gameController.LostLife();
        }
    }
}
