using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    public List<RecipeData> possibleRecipe;
    public Recipe baseRecipe;
    public Canvas canvas;
    public List<Recipe> recipeList;
    public int maxRecipes = 5;
    public int timeRecipes = 15;

    private float elapsedTime = 240f;
    private int totalPoints = 0;
    public Text points;
    public Text time;

    FoodType food;

    public Animator anim;
    public Text text;
    private float animTime = 0f;

    private Points pointsManager;

    private bool letBegin = false;
    private float begin = 0f;

    public Text ready;
    public Text go;

    public MusicElements musicElements;
    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(generateRecipe());
        pointsManager = GameObject.FindGameObjectWithTag("Points").GetComponent<Points>();
        pointsManager.setToZero();
        points.text = "0";
        ready.enabled = false;
        go.enabled = false;
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().gameMusic.pitch = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (letBegin)
        {
            points.text = totalPoints.ToString();

            if (elapsedTime > 0f)
            {
                elapsedTime -= Time.deltaTime;
                time.text = FormatTime(elapsedTime);

                if(elapsedTime <= 10f)
                {
                    GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().gameMusic.pitch = 1.4f;
                }
                else if(elapsedTime <= 20f)
                {
                    GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().gameMusic.pitch = 1.2f;
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().gameMusic.Stop();
                if (!musicElements.end.isPlaying && !hasPlayed)
                {
                    musicElements.end.Play();
                    hasPlayed = true;
                }
                
                Time.timeScale = 0f;
                if (!musicElements.end.isPlaying)
                {
                    SceneManager.LoadScene(sceneName: "PointScene");
                }
            }


            if (canvas.transform.childCount > 1)
            {
                if (recipeList[0].elapsedTime >= recipeList[0].requiredTime)
                {
                    recipeList.Remove(recipeList[0]);
                    canvas.transform.GetChild(4).transform.SetParent(null);
                    totalPoints = totalPoints - 10;
                    pointsManager.increaseFailed();
                    activatePanels();
                }
            }

            if (anim.GetBool("showUP"))
            {
                if (anim.GetBool("showUP"))
                {
                    if (animTime < 1.5)
                    {
                        animTime += Time.deltaTime;
                    }
                    else
                    {
                        animTime = 0f;
                        anim.SetBool("showUP", false);
                    }
                }
            }
        }
        else
        {
            if(begin <= 5f)
            {

                begin += Time.unscaledDeltaTime;

                if(begin < 3.5f)
                {
                    ready.enabled = true;
                    go.enabled = false;
                }
                else
                {
                    ready.enabled = false;
                    go.enabled = true;
                }
            }
            else
            {
                letBegin = true;
                ready.enabled = false;
                go.enabled = false;
                Time.timeScale = 1f;
            }
        }
        
    }

    public void playAnimation()
    {
        anim.SetBool("showUP", true);
    }

    public void activatePanels()
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            recipeList[i].setInitialPosition(recipeList[i].getFinalPosition());
            recipeList[i].setFinalPosition(new Vector3(i * 150 + 90, -10, 0));
            recipeList[i].setInitialTime();
            recipeList[i].setDistance();
            recipeList[i].moving = true;
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void receivePlate(plateContent content)
    {
        bool found = false;
        for (int i = 0; i < recipeList.Count; i++)
        {
            if (recipeList[i].plateContent == content)
            {
                int point = 2 * Mathf.FloorToInt(6 * (float)(recipeList[i].timePercentage / 1.5));
                if(point > 0)
                {
                    totalPoints += point;
                    pointsManager.increaseTips(point);
                    text.text = "+" + point.ToString();
                    playAnimation();
                }
                recipeList.Remove(recipeList[i]);
                canvas.transform.GetChild(i + 4).transform.SetParent(null);
                totalPoints += 20;
                pointsManager.increaseDelivered();
                found = true;
                activatePanels();
                break;
            }
        }

        if (found)
        {
            musicElements.correct.Play();
        }
        else
        {
            musicElements.wrong.Play();
        }
    }

    IEnumerator generateRecipe()
    {
        while(true)
        {
            if(recipeList.Count < maxRecipes)
            {
                int randomIndex = Random.Range(0, possibleRecipe.Count);
                Recipe recipe = Instantiate(baseRecipe, canvas.transform);
                recipe.setup(possibleRecipe[randomIndex], recipeList.Count);
                recipeList.Add(recipe);
            }
            yield return new WaitForSeconds(timeRecipes);
        }
    }
}
