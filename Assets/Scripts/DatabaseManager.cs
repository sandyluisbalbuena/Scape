using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    private string userId;
    private DatabaseReference dbReference;
    private Firebase.FirebaseApp app;

    void Start()
    {
        // Initialize Firebase
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                app = Firebase.FirebaseApp.DefaultInstance;
            } else {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });


        userId = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        CreateUser();
    }

    private void CreateUser()
    {
        // User newUser = new User(Name.text);
        User newUser = new User("Luis");
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

}
