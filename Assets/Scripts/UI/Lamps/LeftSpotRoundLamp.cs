using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Lamps
{
  public class LeftSpotRoundLamp : MonoBehaviour
  {
    public ShipUiManager manager;

    public GameObject off;
    public GameObject on;

    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    private float offset = 0;

    void Start()
    {
      offset = Random.value;
    }

    void FixedUpdate()
    {
      _timer += Time.deltaTime;
      if (_timer > _maxTime)
      {
        _timer = 0;
      }

      if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
      {
        float diff = Mathf.Sin((_timer + offset)  * speed);
        if (diff > 0f)
        {
          off.SetActive(true);
          on.SetActive(false);
        }
        else
        {
          off.SetActive(false);
          on.SetActive(true);
        }
      }
      else
      {
        if (manager.GameState.LeftSpotState == GameState.MaschienState.On)
        {
          off.SetActive(false);
          on.SetActive(true);
        }
        else
        {
          off.SetActive(true);
          on.SetActive(false);
        }
      }
    }
  }
}