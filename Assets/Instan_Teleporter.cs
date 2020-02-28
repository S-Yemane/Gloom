//TODO: Proper assignment of script references for Instantiated GameObject
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Instan_Teleporter : MonoBehaviour
{
    public GameObject manager;
    public GameObject managerNext;
    public GameObject teleporter;
    private GameObject _player;
    private Transform _teleportTarget;
    public GameObject teleportLocation;

    //public objects used to track the Start() function
    public GameObject tarObject;
    public Transform tarTeleport;
    public GameObject tarLocation;


    // Start is called before the first frame update
    void Start()
    {
        // Math Methods
        Vector3 MinimumSet(Vector3 myVector)    // Sets minimum bounds of position in maze
        {
            Vector3 newVec = myVector;
            newVec.x -= 25f;
            newVec.z -= 25f;
            return newVec;
        }
        Vector3 MaximumSet(Vector3 myVector)    // Sets maximum bounds of position in maze
        {
            Vector3 newVec = myVector;
            newVec.x += 25f;
            newVec.z += 25f;
            return newVec;
        }
        Vector3 Random(Vector3 myVector, Vector3 min, Vector3 max)  //Randomizes the location of the teleporter within the bounds of the maze
        {
            Vector3 newVec = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
            return newVec;
        }
        // Variable Initialization
        Vector3 manageMin, manageMax, manageNextMin, manageNextMax, manageRand = new Vector3(0,0,0), manageNextRand = new Vector3(0,0,0);
        // Variable Assignment
        manageMin = MinimumSet(manager.transform.position);
        manageMax = MaximumSet(manager.transform.position);
        manageNextMin = MinimumSet(managerNext.transform.position);
        manageNextMax = MaximumSet(managerNext.transform.position);
        manageRand = Random(manageRand, manageMin, manageMax);
        manageNextRand = Random(manageNextRand, manageNextMin, manageNextMax);
        // Instantiation of a new Teleporter GameObject
        var newTeleport = Instantiate(teleporter, manageRand, Quaternion.identity);
        // Assignment of References for newTeleport's "Teleport" script **UNFINISHED**
        var component = newTeleport.GetComponent<Teleport>();
        tarObject = newTeleport;
        component.teleportTarget = _teleportTarget;
        component.player = _player;
        tarTeleport = component.teleportTarget;
        tarLocation = component.player;  
    }
}
