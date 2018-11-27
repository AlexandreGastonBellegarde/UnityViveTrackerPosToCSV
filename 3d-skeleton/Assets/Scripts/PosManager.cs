using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

public class PosManager : MonoBehaviour {

    GameObject redTrackerObj;
    GameObject blueTrackerObj;
    GameObject yellowTrackerObj;
    int delayTime = 34;
    
    bool startChk = false;

    public IEnumerator coroutine;
    List<string> columns;

    string time;
    int idx;
    string posX;
    string posY;
    string posZ;
    string rotX;
    string rotY;
    string rotZ;

    string filePath;

    StreamWriter writer;

    // Use this for initialization
    void Start () {
        redTrackerObj = GameObject.FindGameObjectWithTag("RedTracker");
        blueTrackerObj = GameObject.FindGameObjectWithTag("BlueTracker");
        yellowTrackerObj = GameObject.FindGameObjectWithTag("YellowTracker");
        
        coroutine = GetPos();
    }

    
    public void StartOn() {
        print("save start");
        this.startChk = true;
        string fileTime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        filePath = Application.dataPath + "/CSV/" + "Tracker-"+ fileTime + ".csv";

        print(filePath);

        string sDir = Application.dataPath + "/CSV";
        DirectoryInfo di = new DirectoryInfo(sDir);

        if (di.Exists == false) {
            di.Create();
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        bf.Serialize(file, "");
        file.Close();
   
        writer = new StreamWriter(filePath);
        StartCoroutine("GetPos");
    }

    public void StartOff()
    {        
        print("save end");
        this.startChk = false;
        StopCoroutine("GetPos");
        //writer.Close();
    }


    IEnumerator GetPos() {

        writer.WriteLine("Time,Idx,PosX,PosY,PosZ,RotX,RotY,RotZ");
        writer.Flush();

        while (startChk) {
            
            time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:");
            time += Time.time.ToString("f2");
            //print(time);
            idx = 0;

            posX = redTrackerObj.transform.position.x.ToString();
            posY = redTrackerObj.transform.position.y.ToString(); 
            posZ = redTrackerObj.transform.position.z.ToString();
            rotX = redTrackerObj.transform.eulerAngles.x.ToString();
            rotY = redTrackerObj.transform.eulerAngles.y.ToString();
            rotZ = redTrackerObj.transform.eulerAngles.z.ToString();

            writer.WriteLine(time+","+ idx + "," + posX + "," + posY + "," + posZ + "," + rotX + "," + rotY + "," + rotZ);
            writer.Flush();

            idx = 1;

            posX = blueTrackerObj.transform.position.x.ToString();
            posY = blueTrackerObj.transform.position.y.ToString();
            posZ = blueTrackerObj.transform.position.z.ToString();
            rotX = blueTrackerObj.transform.eulerAngles.x.ToString();
            rotY = blueTrackerObj.transform.eulerAngles.y.ToString();
            rotZ = blueTrackerObj.transform.eulerAngles.z.ToString();

            writer.WriteLine(time + "," + idx + "," + posX + "," + posY + "," + posZ + "," + rotX + "," + rotY + "," + rotZ);
            writer.Flush();

            idx = 2;

            posX = yellowTrackerObj.transform.position.x.ToString();
            posY = yellowTrackerObj.transform.position.y.ToString();
            posZ = yellowTrackerObj.transform.position.z.ToString();
            rotX = yellowTrackerObj.transform.eulerAngles.x.ToString();
            rotY = yellowTrackerObj.transform.eulerAngles.y.ToString();
            rotZ = yellowTrackerObj.transform.eulerAngles.z.ToString();

            writer.WriteLine(time + "," + idx + "," + posX + "," + posY + "," + posZ + "," + rotX + "," + rotY + "," + rotZ);
            writer.Flush();

            //yield return new WaitForSeconds(delayTime);
            Thread.Sleep(delayTime);
            yield return null;
        }//.endWhile
            
        
    }//endGetPos


  

    

}//.class
