using TMPro;
using UnityEngine;

public class CreateArmy : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject armyListGameObject;

    //private int numberOfArmies = 0; // This is the current number of armies spawned by the player.
    public void CreateArmyButton()
    {
        // This is so we can only create the army in our own counties.
        if (WorldMapLoad.counties[SelectCounty.currentlySelectedCounty].ownerName == WorldMapLoad.playerName)
        {
            var armyNumber = WorldMapLoad.armies.Count;
            var newArmyList = new Army(null, null, null, null, null,false, false,false, "Player", "Fuck Stick" + armyNumber, Random.Range(1, 1001));

            WorldMapLoad.armies.Add(newArmyList);

            newArmyList.gameObject = Instantiate(unitPrefab, WorldMapLoad.counties[SelectCounty.currentlySelectedCounty].countyCenterGameObject.transform.position,
              Quaternion.identity);
            //Debug.Log("New Army Game Object: " + newArmyList.gameObject);
            // Change name of GameObject in the inspector
            newArmyList.gameObject.name = armyNumber.ToString();

            // Move GameObject to army list in Inspector.
            newArmyList.gameObject.transform.parent = armyListGameObject.transform;

            newArmyList.armyTimerCanvasGameObject = newArmyList.gameObject.transform.GetChild(0).gameObject;
            newArmyList.armyTimerText = newArmyList.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            // Store the current location name of the army.
            newArmyList.currentLocation = SelectCounty.currentlySelectedCounty;
            Debug.Log("Current Location: " + newArmyList.currentLocation);

            // Sets the army Destination to its current location.
            newArmyList.armyDestination = SelectCounty.currentlySelectedCounty;

            // Change the Army Info Panel to have the new army info from list.
            UIArmyPanel.armyOwnerText.text = "Owner: " + newArmyList.owner; //WorldMapLoad.armies[numberOfArmies].owner;
            UIArmyPanel.armyNameText.text = "Name: " + newArmyList.name; //WorldMapLoad.armies[numberOfArmies].name;
            UIArmyPanel.armySizeText.text = "Size: " + newArmyList.size.ToString(); //WorldMapLoad.armies[numberOfArmies].size.ToString();

            WorldMapLoad.armyInfoPanel.SetActive(true);
            WorldMapLoad.countyInfoPanel.SetActive(false);
        }
        else
        {
            Debug.Log("You don't own this county, so you can't spawn armies here.");


        }

    }
}
