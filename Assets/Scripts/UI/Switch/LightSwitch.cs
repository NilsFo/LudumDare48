using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
  public class LightSwitch : MonoBehaviour, IPointerClickHandler 
  {
    public ShipUiManager manager;
  
    public GameObject off;
    public GameObject mid;
    public GameObject on;

    public static bool isOn = true;
  
    public void OnPointerClick(PointerEventData eventData)
    {
      isOn = !isOn;
    }
  
    void FixedUpdate()
    {
      if (isOn)
      {
        off.SetActive(false);
        on.SetActive(true);
        mid.SetActive(false);
        manager.GameState.LightState = GameState.MaschienState.On;
      }
      else
      {
        off.SetActive(true);
        on.SetActive(false);
        mid.SetActive(false);
        manager.GameState.LightState = GameState.MaschienState.Off;
      }
    }
  }
}
