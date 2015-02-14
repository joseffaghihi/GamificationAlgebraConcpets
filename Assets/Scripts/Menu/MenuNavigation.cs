using UnityEngine;
using System.Collections;

public class MenuNavigation : MonoBehaviour
{
    public Canvas Menu;		//reference for the whole Menu

    //***RectTransform references to each Panel***
    public RectTransform mainMenuPanel;
    public RectTransform shopPanel;
    public RectTransform levelSelectPanel;
    public RectTransform trophyPanel;
    public RectTransform statsPanel;

    //***Animator references***
    public Animator mainMenuAnimator;
    public Animator shopAnimator;
    public Animator levelSelectAnimator;
    public Animator trophyAnimator;
    public Animator statsAnimator;

    //***All Types of possible Animations***
    private const string moveRight = "MoveRight";
    private const string moveLeft = "MoveLeft";
    private const string fadeIn = "MainMenuFadeIn";
    private const string fadeOut = "MainMenuFadeOut";

    //***All Types of possible Scenes***
    private const string BalancingGame = "Balance";

    //Keep track of which panel the user is in
    /*
     * 0 = main menu panel
     * 1 = level select panel
     * 2 = shop panel
     * 3 = trophies panel
     * 4 = statistics panel
     */
    Stats stats = new Stats();

    void Start()
    {
        mainMenuAnimator.Play(moveRight);
        stats.SetCurrentPanel(0);
    }

    //Load Play Selection
    public void PlayButton()
    {
        mainMenuAnimator.Play(moveLeft);
        levelSelectAnimator.Play(moveRight);

        stats.SetCurrentPanel(1);
    }

    //Load Shop/Inventory
    public void ShopButton()
    {
        mainMenuAnimator.Play(moveLeft);
        shopAnimator.Play(moveRight);

        stats.SetCurrentPanel(2);
    }

    //Load Trophy Panel
    public void TrophiesButton()
    {
        mainMenuAnimator.Play(moveLeft);
        trophyAnimator.Play(moveRight);

        stats.SetCurrentPanel(3);
    }

    //Load Statistics Panel
    public void StatsButton()
    {
        mainMenuAnimator.Play(moveLeft);
        statsAnimator.Play(moveRight);

        stats.SetCurrentPanel(4);
    }

    //Load Main Panel
    public void GoToMainPanel()
    {
        mainMenuAnimator.Play(moveRight);

        switch(stats.GetCurrentPanel())
        {
            case 0:
            break;

            case 1:
                levelSelectAnimator.Play(moveLeft);
            break;

            case 2:
                shopAnimator.Play(moveLeft);
            break;

            case 3:
                trophyAnimator.Play(moveLeft);
            break;

            case 4:
                statsAnimator.Play(moveLeft);
            break;

            default:
                Debug.LogError("Current Menu Location not found.");
            break;
        }
    }

    //Load Balancing Game
    public void PlayBalancingGame()
    {
        Application.LoadLevel(BalancingGame); //start the balancing game
    }

    //Save Game
    public void SaveButton()
    {
        Stats stats = new Stats();

        stats.Save();
    }

    //Load Game
    public void LoadButton()
    {
        Stats stats = new Stats();

        stats.Load();
    }

    //Quit the Game
    public void QuitGame()
    {
        Application.Quit();
    }
}
