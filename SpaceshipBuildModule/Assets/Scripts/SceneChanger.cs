using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    
    public void GoToConstructScene() {
        SceneManager.LoadScene("Shipbuilding");
    } 
    
    public void GoToShipChoiceScene() {
        SceneManager.LoadScene("Shipchoice");
    }
    
    

    public void Quit() {
        Debug.Log("we are closing an app");
        Application.Quit();
    }
}
