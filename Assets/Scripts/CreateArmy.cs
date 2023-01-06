using UnityEngine;

public class CreateArmy : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GameObject armyListGameObject;

    //private int numberOfArmies = 0; // This is the current number of armies spawned by the player.
    public void CreateArmyButton()
    {
        if (WorldMapLoad.counties[SelectCounty.currentlySelectedProvince].ownerName == WorldMapLoad.playerName)
        {
            var armyNumber = WorldMapLoad.armies.Count;
            var newArmy = new Army(null, null, false, "Player", "Fuck Stick" + armyNumber, Random.Range(1, 1001));

            // Armies added to Armies list
            //WorldMapLoad.armies.Add(new Army(null, null, false, "Player", "Fuck Stick" + numberOfArmies, Random.Range(1, 1001)));

            WorldMapLoad.armies.Add(newArmy);

            // Instantiate and assign to Army list.
            //WorldMapLoad.armies[numberOfArmies].gameObject = Instantiate(unitPrefab, WorldMapLoad.counties[SelectCounty.currentlySelectedProvince].countyCenterGameObject.transform.position,
            //   Quaternion.identity); // Spawn the token, put it in a location and stop is rotation

            newArmy.gameObject = Instantiate(unitPrefab, WorldMapLoad.counties[SelectCounty.currentlySelectedProvince].countyCenterGameObject.transform.position,
              Quaternion.identity);
            // Change name of GameObject in the inspector
            //WorldMapLoad.armies[numberOfArmies].gameObject.name = numberOfArmies.ToString();
            newArmy.gameObject.name = armyNumber.ToString();



            // Move GameObject to army list in Inspector.
            //WorldMapLoad.armies[numberOfArmies].gameObject.transform.parent = armyListGameObject.transform;
            newArmy.gameObject.transform.parent = armyListGameObject.transform;

            // Change the Army Info Panel to have the new army info from list.
            UIArmyPanel.armyOwnerText.text = newArmy.owner; //WorldMapLoad.armies[numberOfArmies].owner;
            UIArmyPanel.armyNameText.text = newArmy.name; //WorldMapLoad.armies[numberOfArmies].name;
            UIArmyPanel.armySizeText.text = newArmy.size.ToString(); //WorldMapLoad.armies[numberOfArmies].size.ToString();

            WorldMapLoad.armyInfoPanel.SetActive(true);
            WorldMapLoad.countyInfoPanel.SetActive(false);

            //numberOfArmies++;
        }
        else
        {
            Debug.Log("You don't own this county, so you can't spawn armies here.");


        }

    }
}
