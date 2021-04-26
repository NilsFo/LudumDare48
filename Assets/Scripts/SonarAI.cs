using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarAI : MonoBehaviour
{

    public Image myBlep;
    public float rangeY = 30;
    public float rangeX = 120;

    public FishManagerAI fishManager;
    public RectTransform rect;
    public GameState gameState;
    public Camera playerCamera;
    public List<Image> allBleps;

    // Start is called before the first frame update
    void Start()
    {
        allBleps = new List<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Deleting old bleps
        foreach(Image i in allBleps)
        {
            Destroy(i.gameObject);
        }
        allBleps.Clear();

        // Looking for nearby fish
        float depth = gameState.CurrentDepth;
        List<GameObject> fish = GetNearbyFish(depth);
        //print("Nearby Fish: " + fish.Count);

        foreach (GameObject currentFish in fish)
        {
            Vector3 localFish = playerCamera.transform.worldToLocalMatrix.MultiplyVector(currentFish.transform.position);
            float fishX = currentFish.transform.position.x - playerCamera.transform.position.x;
            float fishZ = currentFish.transform.position.z - playerCamera.transform.position.z;

            float sonarX = localFish.x / rangeX * (rect.rect.width*0.75f);
            float sonarZ = localFish.z / rangeX * (rect.rect.height*0.75f);

            float distY = Mathf.Abs(playerCamera.transform.position.y - localFish.y);
            float sonarY = distY / rangeY;
            float a = 1 - sonarY;

            Image newBlep = Instantiate(myBlep, this.transform);
            newBlep.transform.localPosition = new Vector3(sonarX, sonarZ, this.transform.position.z);

            var tempColor = newBlep.color;
            tempColor.a = a;
            newBlep.color = tempColor;
            allBleps.Add(newBlep);
        }

    }

    public List<GameObject> GetNearbyFish(float depth)
    {
        List<GameObject> nearbyFish = new List<GameObject>();

        List<GameObject> fish = fishManager.getFishAtDepth(depth);

        foreach (GameObject f in fish)
        {
            FishAI fai = f.GetComponent<FishAI>();
            float disty = Mathf.Abs(depth - f.transform.position.y);
            if (disty <= rangeY)
            {
                Vector2 myPos = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.z);
                Vector2 fishPos = new Vector2(f.transform.position.x, f.transform.position.z);
                if (Vector2.Distance(myPos,fishPos) <= rangeX)
                { 
                    nearbyFish.Add(f);
                }
            }
        }

        return nearbyFish;
    }


}