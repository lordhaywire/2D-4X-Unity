using TMPro;
using UnityEngine;

public class MusterArmy : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject armyListGameObject;

    //private int numberOfArmies = 0; // This is the current number of spawnedArmies spawned by the player.
    public void MusterArmyButton()
    {
        // This is so we can only create the army in our own counties.
        if (WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].faction.factionNameAndColor.name
            == WorldMapLoad.Instance.playerFaction)
        {
            var armyNumber = WorldMapLoad.Instance.spawnedArmies.Count;
            var spawnedArmyToken = new SpawnedArmy(null, null, null, null, null, null,false, false,false, "Player", "Fuck Stick" + armyNumber, Random.Range(1, 1001));

            WorldMapLoad.Instance.spawnedArmies.Add(spawnedArmyToken);

            spawnedArmyToken.gameObject = Instantiate(unitPrefab, WorldMapLoad.Instance.counties[WorldMapLoad.Instance.currentlySelectedCounty].countyCenterGameObject.transform.position,
              Quaternion.identity, armyListGameObject.transform);
            //Debug.Log("New SpawnedArmy Game Object: " + spawnedArmyToken.uIResearchItemPanelGameObject);
            // Change name of GameObject in the inspector
            spawnedArmyToken.gameObject.name = armyNumber.ToString();

            // This just sets it to it's basic starting color32.
            spawnedArmyToken.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

            // Move GameObject to army list in Inspector.
            //spawnedArmyToken.gameObject.transform.parent = armyListGameObject.transform;

            // This is for the text box above the army's uIResearchItemPanelGameObject in the game.
            spawnedArmyToken.timerCanvasGameObject = spawnedArmyToken.gameObject.transform.GetChild(0).gameObject;
            spawnedArmyToken.timerText = spawnedArmyToken.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            // This is the ArmyMovement script attached to the uIResearchItemPanelGameObject as a component.
            spawnedArmyToken.armyMovement = spawnedArmyToken.gameObject.GetComponent<ArmyMovement>();
            Debug.Log("Army Movement Script? " + spawnedArmyToken.armyMovement);
            
            // Store the current location name of the army.
            spawnedArmyToken.location = WorldMapLoad.Instance.currentlySelectedCounty;
            Debug.Log("Current Location: " + spawnedArmyToken.location);

            // Sets the army Destination to its current location.
            spawnedArmyToken.destination = WorldMapLoad.Instance.currentlySelectedCounty;

            // Change the SpawnedArmy Info Panel to have the new army info from list.
            WorldMapLoad.Instance.armyInfoPanel.SetActive(true);

            UIArmyPanel.Instance.armyOwnerText.text = "Owner: " + spawnedArmyToken.owner; 
            UIArmyPanel.Instance.armyNameText.text = "Name: " + spawnedArmyToken.name; 
            UIArmyPanel.Instance.armySizeText.text = "Size: " + spawnedArmyToken.size.ToString();

            WorldMapLoad.Instance.countyInfoPanel.SetActive(false);
        }
        else
        {
            Debug.Log("You don't own this county, so you can't spawn armies here.");
        }

    }
}
