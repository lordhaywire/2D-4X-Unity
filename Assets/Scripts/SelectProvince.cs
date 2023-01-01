using UnityEngine;
using UnityEngine.EventSystems;

public class SelectProvince : MonoBehaviour, IPointerClickHandler
{
    public static bool isArmySelected;
    public float speed;

    private bool rightClick = false;
    private Vector2 velocity = Vector2.zero;
    public float smoothTime = 0.3f;

    private void FixedUpdate()
    {
        if (rightClick == true)
        {
            /*var armyStep = speed * Time.deltaTime; // This is probably going to need fixed Delta time.
            WorldMapLoad.armies[0].gameObject.transform.position = Vector2.MoveTowards(WorldMapLoad.armies[0].gameObject.transform.position, WorldMapLoad.provinces[name][0].provinceCenterGameObject.transform.position,
                armyStep);*/

            WorldMapLoad.armies[0].gameObject.transform.position = Vector2.SmoothDamp(
                 WorldMapLoad.armies[0].gameObject.transform.position, WorldMapLoad.provinces[name][0].provinceCenterGameObject.transform.position, ref velocity, smoothTime);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            WorldMapLoad.provincePanel.SetActive(true);
            WorldMapLoad.armyPanel.SetActive(false);

            UIProvincePanel.provinceOwnerText.text = "Owner: " + WorldMapLoad.provinces[name][0].ownerName;
            UIProvincePanel.provinceNameText.text = "Province: " + name;

            if (WorldMapLoad.playerName == WorldMapLoad.provinces[name][0].ownerName)
            {
                UIProvincePanel.provincePopulationText.text = "Population: " + WorldMapLoad.provinces[name][0].population.ToString();
            }
            else
            {
                UIProvincePanel.provincePopulationText.text = "Population: Unknown";
            }
            Debug.Log("Name of Province: " + name);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (isArmySelected == true)
            {
                if (rightClick == true)
                {
                    rightClick = false;
                    Debug.Log("Right Click = " + rightClick);
                }
                else
                {
                    rightClick = true;
                }


                // Notes for SmoothDamp and MoveTowards
                //WorldMapLoad.armies[0].gameObject.transform.position = WorldMapLoad.provinces[name][0].provinceCenterGameObject.transform.position;
                //WorldMapLoad.armies[0].gameObject.transform.position = Vector2.SmoothDamp(
                //    WorldMapLoad.armies[0].gameObject.transform.position, WorldMapLoad.provinces[name][0].provinceCenterGameObject.transform.position, ref velocity, smoothTime);

                /*
                var armyStep = speed * Time.deltaTime; // This is probably going to need fixed Delta time.
                WorldMapLoad.armies[0].gameObject.transform.position = Vector2.MoveTowards(WorldMapLoad.armies[0].gameObject.transform.position, WorldMapLoad.provinces[name][0].provinceCenterGameObject.transform.position,
                    armyStep);
                Debug.Log("Province Name from Select Province: " + name);
                Debug.Log("Select Army - This is a right click");
                */
            }
            else
            {
                Debug.Log("No army is selected.");

            }

            /*
            SelectArmy.provinceName = name;
            Debug.Log("Province Name: " + SelectArmy.provinceName);
            Debug.Log("Select Province - This is a right click");
            */
        }
    }
}
