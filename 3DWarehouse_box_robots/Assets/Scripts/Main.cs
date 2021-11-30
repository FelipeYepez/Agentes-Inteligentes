using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject warehouse;
    [SerializeField] GameObject box;
    [SerializeField] GameObject shelf;
    [SerializeField] GameObject robot;
    const int N = 5;
    Vector3 startWarehouse = new Vector3(1, 0, -12);
    GameObject[] robots = new GameObject[N];
    Vector3[] startRobots = new Vector3[N];
    GameObject[] boxes = new GameObject[N];
    Vector3[] startBoxes = new Vector3[N];
    Vector3[] p1 = new Vector3[N];
    float[] startShelfsX = new float[3];
    float[] startShelfsY = new float[4];
    float[] speeds = new float[N];
    int[] state = {0, 0, 0, 0, 0};
    int[] dest = {4, 1, 3, 2, 0};


    // Start is called before the first frame update
    void Start()
    {   
        startRobots[0] = new Vector3(-3, 0, -2.3f);
        startRobots[1] = new Vector3(-10.64f, 0, -2.07f);
        startRobots[2] = new Vector3(-20.49f, 0, -1.82f);
        startRobots[3] = new Vector3(-29.21f, 0, -1.76f);
        startRobots[4] = new Vector3(-20.61f, 0, -6.6f);

        startBoxes[0] = new Vector3(-5, 0, -16.3f);
        startBoxes[1] = new Vector3(-24.32f, 0, -16.3f);
        startBoxes[2] = new Vector3(-14.37f, 0, -22.85f);
        startBoxes[3] = new Vector3(-19.37f, 0, -29f);
        startBoxes[4] = new Vector3(-9.44f, 0, -29f);

        startShelfsX[0] = -1.0f;
        startShelfsX[1] = -10.0f;
        startShelfsX[2] = -20.0f;

        startShelfsY[0] = -25.7f;
        startShelfsY[1] = -19.15f;
        startShelfsY[2] = -12.0f;
        startShelfsY[3] = -5.5f;
        createWarehouse(startRobots, startBoxes, startShelfsX, startShelfsY);

        for (int i = 0; i < N; i++) {
            speeds[i] = Random.Range(1.0f, 4.0f);
        }

        p1[0] = new Vector3(-3, 0, -29f);
        p1[1] = new Vector3(-10.64f, 0, -16.3f);
        p1[2] = new Vector3(-20.49f, 0, -29f);
        p1[3] = new Vector3(-29.21f, 0, -22.85f);
        p1[4] = new Vector3(-20.61f, 0, -16.3f);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move(){
        for(int i = 0; i < N; i++){
            if(state[i] == 0){
                robots[i].transform.position = Vector3.MoveTowards(robots[i].transform.position, p1[i], Time.deltaTime * speeds[i]);
                if(Mathf.Abs(Vector3.Distance(robots[i].transform.position, p1[i])) <= 1f){
                    state[i] = 1;
                }
            }
            else if(state[i] == 1){
                Quaternion rotate = Quaternion.LookRotation(startBoxes[dest[i]] - robots[i].transform.position, Vector3.up);
                rotate = Quaternion.Inverse(rotate);
                robots[i].transform.rotation = Quaternion.Lerp(robots[i].transform.rotation, rotate, 5 * Time.deltaTime);
                robots[i].transform.position = Vector3.MoveTowards(robots[i].transform.position, startBoxes[dest[i]], Time.deltaTime * speeds[i]);
                if(Mathf.Abs(Vector3.Distance(robots[i].transform.position, startBoxes[dest[i]])) <= 1f){
                    state[i] = 2;
                }
            }
        }
    }

    void createWarehouse(Vector3[] startRobots, Vector3[] startBoxes, float[] startShelfsX, float[] startShelfsY){
        Instantiate(warehouse, startWarehouse, Quaternion.identity);
        for(int i = 0; i < N; i++){
            GameObject agent;
            agent = Instantiate(robot, startRobots[i], Quaternion.identity);
            robots[i] = agent;
            agent = Instantiate(box, startBoxes[i], Quaternion.identity);
            boxes[i] = agent;
        }
        Vector3 temp;
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 4; j++){
                temp = new Vector3(startShelfsX[i], 0, startShelfsY[j]);
                Instantiate(shelf, temp, Quaternion.identity);
            }
        }
    }
}