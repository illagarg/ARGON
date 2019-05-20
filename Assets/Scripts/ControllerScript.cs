using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    public GameObject heritageBtn;
    public GameObject infoBtn;
    public GameObject backdateBtn;
    private GameObject target;

    public GameObject[] buildings;
    private GameObject currBuilding;
    //public GameObject slider;

    public GameObject[] handMenu;

    public GameObject cam;

    private enum State {Building, Menu, Info, BackDate};
    private State currState;

    private string currFinger;
    private string yearLookedAt;
    private string selectedYear;
    private int selectedIndex;

    public GameObject yearDisplayed;
    public Text showingYear;


    // Start is called before the first frame update
    void Start()
    {
        infoBtn.SetActive(false);
        backdateBtn.SetActive(false);
        currState = State.Building;
        currFinger = "Thumb";
        hideTime();
        cam.GetComponent<GestureScript>().switchMode("Normal");
        yearDisplayed.SetActive(false);
        selectedYear = "2019";
        showingYear.text = selectedYear;
    }

    // Update is called once per frame
    void Update()
    {

        print(selectedIndex);

    }



    public void processEvent(string mode) {
        target = cam.GetComponent<CameraScript>().getGameObject();
        if (mode == "open") {
            //print(target.name);
            if (target != null) {
                switch (target.name)
                {
                    case "Heritage Btn":
                        if (currState == State.Building)
                        {

                            StartCoroutine(heritageBtn.GetComponent<ButtonScript>().openButton());
                            infoBtn.SetActive(true);
                            backdateBtn.SetActive(true);
                            setBuildings();

                            currState = State.Menu;
                        }
                        break;

                    case "Info Btn":
                        if (currState == State.Menu)
                        {
                            StartCoroutine(infoBtn.GetComponent<ButtonScript>().openButton());
                            backdateBtn.SetActive(false);
                            currBuilding.GetComponent<BuildingScript>().displayInfo();

                            currState = State.Info;
                        }
                        break;

                    case "Backdate Btn":

                        if (currState == State.Menu)
                        {
                            StartCoroutine(backdateBtn.GetComponent<ButtonScript>().openButton());
                            infoBtn.SetActive(false);
                            //slider.SetActive(true);
                            cam.GetComponent<GestureScript>().switchMode("BackDate");
                            yearDisplayed.SetActive(true);

                            currState = State.BackDate;

                            // 到时删掉
                            showTime();
                        }

                        break;


                }
            }

        }
        else if (mode == "close") {  // close
            switch (currState)
            {
                case State.Menu:
                    StartCoroutine(heritageBtn.GetComponent<ButtonScript>().comeBackButton());
                    infoBtn.SetActive(false);
                    backdateBtn.SetActive(false);
                    hideBuildings();

                    currState = State.Building;
                    break;

                case State.Info:
                    StartCoroutine(infoBtn.GetComponent<ButtonScript>().comeBackButton());
                    backdateBtn.SetActive(true);
                    StartCoroutine(backdateBtn.GetComponent<ButtonScript>().comeBackButton());
                    currBuilding.GetComponent<BuildingScript>().hideInfo();

                    currState = State.Menu;
                    break;

                case State.BackDate:
                    infoBtn.SetActive(true);
                    StartCoroutine(infoBtn.GetComponent<ButtonScript>().comeBackButton());
                    StartCoroutine(backdateBtn.GetComponent<ButtonScript>().comeBackButton());
                    cam.GetComponent<GestureScript>().switchMode("Normal");
                    cam.GetComponent<GestureScript>().dontclose();
                    hideTime();
                    yearDisplayed.SetActive(false);

                    //slider.SetActive(false);
                    currState = State.Menu;
                    break;
            }
        }
    }

    public void displayTime(Vector3[] pos) {
        int i = 0;
        foreach (GameObject menu in handMenu) {
            menu.SetActive(true);
            menu.GetComponent<FingerMenuScript>().adjustPos(pos[i]);
            i += 1;
        }
    }

    public void hideTime() {
        foreach (GameObject menu in handMenu)
        {
            menu.SetActive(false);
        }
        //currFinger = "None";
    }

    public void showTime() {
        foreach (GameObject menu in handMenu)
        {
            menu.SetActive(true);
        }
    }

    void setBuildings() {
        foreach (GameObject building in buildings)
        {
            building.SetActive(false);
        }
        currBuilding = buildings[selectedIndex];
        currBuilding.SetActive(true);
    }

    void hideBuildings() {
        foreach (GameObject building in buildings)
        {
            building.SetActive(false);
        }
    }


    public void choose(GameObject year) {
        currFinger = year.name;
        yearLookedAt = year.transform.GetChild(0).GetComponent<Text>().text;
        //print(currFinger);
    }

    public void selectFingerMenu() {
        switch (currFinger)
        {
            case "Thumb":
                selectedIndex = 0;
                break;
            case "Index Finger":
                selectedIndex = 1;
                break;
            case "Middle Finger":
                selectedIndex = 2;
                break;
            case "Ring Finger":
                selectedIndex = 3;
                break;
            case "Little Finger":
                selectedIndex = 4;
                break;
            case "Exit":
                processEvent("close");
                break;
            default:
                break;
        }

        selectedYear = yearLookedAt;
        showingYear.text = selectedYear;
        setBuildings();
    }



    // Simulate AR
    public void reposition(Vector3 d)
    {
        this.transform.position += d;
    }

}
