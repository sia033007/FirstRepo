using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityEngine.UI;
public class NewScript : MonoBehaviour
{
    public static NewScript instance;
    public InputField username;
    public InputField password;
    public int score;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player player1 = new Player("Iranian","Taremi",27);
        Player player2 = new Player("German","Kroos",30);
        Player player3 = new Player("Austrian","Alaba",30);

        Debug.Log(player1.name);
        player3.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,300))
            {
                agent.destination = hit.point;
            }
        }
        
    }
    IEnumerator CreateAssetBundle()
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle("");
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            var prefab = bundle.LoadAsset("Player");
            GameObject character = Instantiate(prefab,transform.position,Quaternion.identity)as GameObject;
        }
    }
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username",username.text);
        form.AddField("Password",password.text);
        UnityWebRequest request = UnityWebRequest.Post("",form);
        yield return request.SendWebRequest();

        if(request.downloadHandler.text[0]=='0')
        {
            Debug.Log("Successfully logged in!");
            score = int.Parse(request.downloadHandler.text.Split('\t')[1]);
        }
    }
}
public class Player
{
    public string nationality;
    public string name;
    public int age;

    public Player(string mynationality, string myname, int myage)
    {
        nationality = mynationality;
        name = myname;
        age = myage;
    }
    public void Play()
    {
        Debug.Log($"{name} is a {nationality} player with {age} years old");
    }
}
