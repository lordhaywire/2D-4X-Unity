using TMPro;
using UnityEngine;

public class MusterArmy : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject armyListGameObject;

    //private int numberOfArmies = 0; // This is the current number of armies spawned by the player.
    public void MusterArmyButton()
    {
        // This is so we can only create the army in our own counties.
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction)
        {
            var armyNumber = WorldMapLoad.Instance.armies.Count;
            var newArmyList = new Army(null, null, null, null, null, null,false, false,false, "Player", "Fuck Stick" + armyNumber, Random.Range(1, 1001));

            WorldMapLoad.Instance.armies.Add(newArmyList);

            newArmyList.gameObject = Instantiate(unitPrefab, WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].countyCenterGameObject.transform.position,
              Quaternion.identity);
            //Debug.Log("New Army Game Object: " + newArmyList.uIResearchItemPanelGameObject);
            // Change name of GameObject in the inspector
            newArmyList.gameObject.name = armyNumber.ToString();

            // This just sets it to it's basic starting color32.
            newArmyList.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

            // Move GameObject to army list in Inspector.
            newArmyList.gameObject.transform.parent = armyListGameObject.transform;

            // This is for the text box above the army's uIResearchItemPanelGameObject in the game.
            newArmyList.armyTimerCanvasGameObject = newArmyList.gameObject.transform.GetChild(0).gameObject;
            newArmyList.armyTimerText = newArmyList.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            // This is the ArmyMovement script attached to the uIResearchItemPanelGameObject as a component.
            newArmyList.armyMovement = newArmyList.gameObject.GetComponent<ArmyMovement>();
            Debug.Log("Army Movement Script? " + newArmyList.armyMovement);
            
            // Store the current location name of the army.
            newArmyList.location = WorldMapLoad.Instance.currentlySelectedCounty;
            Debug.Log("Current Location: " + newArmyList.location);

            // Sets the army Destination to its current location.
            newArmyList.armyDestination = WorldMapLoad.Instance.currentlySelectedCounty;

            // Change the Army Info Panel to have the new army info from list.
            WorldMapLoad.Instance.armyInfoPanel.SetActive(true);

            UIArmyPanel.Instance.armyOwnerText.text = "Owner: " + newArmyList.owner; 
            UIArmyPanel.Instance.armyNameText.text = "Name: " + newArmyList.name; 
            UIArmyPanel.Instance.armySizeText.text = "Size: " + newArmyList.size.ToString();

            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
        }
        else
        {
            Debug.Log("You don't own this county, so you can't spawn armies here.");
        }

    }
}
