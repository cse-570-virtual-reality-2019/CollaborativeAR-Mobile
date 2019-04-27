using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour
{
    // Start is called before the first frame update

    private bool _rotateFlag;
    private bool _scaleFlag;
    private bool _translateFlag;
    private bool _holdFlag;

    private Movement _mover;
    private AuthorityController _aController;

    public Button scaleButton;
    public Button rotateButton;
    public Button translateButton;
    public Button holdButton;
    private List<GameObject> groups;
    private int _numSelected;

    void Start()
    {
        groups = new List<GameObject>();
        _numSelected = 0;
        
    }

    public void SpawnObject()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Authority");
        Debug.Log("Number of objects " + objs.Length);
        for (int i = 0; i < objs.Length; ++i)
        {
            GameObject obj = objs[i];
            if (obj != null)
            {
                _aController = obj.GetComponent<AuthorityController>();
            }
            else
            {
                Debug.Log("Couldn't find prefab");
            }

            if (_aController.IsLocalPlayer())
            {
                Debug.Log("Found Local Player");
                _aController.Spawn();
            }
        }
    }
    

    
    public void SetSelected(int id)
    {
        if (id == 1)
        {
            
            _holdFlag = false;
            _rotateFlag = false;
            _translateFlag = false;
            translateButton.GetComponent<Image>().color = Color.white;
            holdButton.GetComponent<Image>().color = Color.white;
            rotateButton.GetComponent<Image>().color = Color.white;

            if (_scaleFlag == true)
            {
                _scaleFlag = false;
                scaleButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _scaleFlag = true;
                scaleButton.GetComponent<Image>().color = Color.cyan;    
            }
            
        }
        else if (id == 2)
        {
            
            _scaleFlag = false;
            _holdFlag = false;
            _translateFlag = false;
            translateButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            holdButton.GetComponent<Image>().color = Color.white;

            if (_rotateFlag)
            {
                _rotateFlag = false;
                rotateButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _rotateFlag = true;
                rotateButton.GetComponent<Image>().color = Color.cyan;
            }
        }
        else if (id == 3)
        {
            _scaleFlag = false;
            _rotateFlag = false;
            _holdFlag = false;
            holdButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            rotateButton.GetComponent<Image>().color = Color.white;

            if (_translateFlag)
            {
                _translateFlag = false;
                translateButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _translateFlag = true;
                translateButton.GetComponent<Image>().color = Color.cyan;
            }
        }
        else if (id == 4)
        {
            _scaleFlag = false;
            _rotateFlag = false;
            _translateFlag = false;
            translateButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            rotateButton.GetComponent<Image>().color = Color.white;

            if (_holdFlag)
            {
                _holdFlag = false;
                holdButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _holdFlag = true;
                holdButton.GetComponent<Image>().color = Color.cyan;
            }
            
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null)
                Debug.Log("Casting Ray");
            Ray ray = Camera.main.ScreenPointToRay((Input.GetTouch(0).position));
            //Ray ray = Camera.main.ScreenPointToRay((Input.mousePosition));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Cube");
                for (int i = 0; i < objs.Length; ++i)
                {
                    GameObject obj = objs[i];
                    if (obj != null)
                    {
                        _mover = obj.GetComponent<Movement>();
                    }
                    else
                    {
                        Debug.Log("Couldn't find prefab");
                    }
                    
                    Debug.Log("Hit Object " + _mover.IsLocalPlayer(hit.collider.gameObject).ToString());
                    if (_mover.IsLocalPlayer(hit.collider.gameObject))
                    {
                        if (!groups.Contains(hit.collider.gameObject))
                        {

                            groups.Add(hit.collider.gameObject);
                            _numSelected++;
                            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                        }
                        else
                        {
                            groups.Remove(hit.collider.gameObject);
                            _numSelected--;
                            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.black;
                        }    
                    }
                }   
                
            }

        }

        if (_numSelected > 0)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && _rotateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                for (int i = 0; i < groups.Count; ++i)
                    groups[i].transform.Rotate(-touchDeltaPosition.x * 0.1f, 0, -touchDeltaPosition.y * 0.1f);
            }
            else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && _scaleFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                for (int i = 0; i < groups.Count; ++i)
                {
                    var localScale = groups[i].transform.localScale;
                    localScale = new Vector3(
                        localScale.x + touchDeltaPosition.y * 0.001f,
                        localScale.y + touchDeltaPosition.y * 0.001f,
                        localScale.z + touchDeltaPosition.y * 0.001f);
                    groups[i].transform.localScale = localScale;
                }
                
            }
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && _translateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                for (int i = 0; i < groups.Count; ++i)
                    groups[i].transform.Translate(-touchDeltaPosition.x * 0.01f, 0,
                    0);
            }
            else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && _translateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                for (int i = 0; i < groups.Count; ++i)
                    groups[i].transform.Translate(0, 0, -touchDeltaPosition.y * 0.01f);
            }
            else if (_holdFlag)
            {
                const float distance = 10.0f;
                for (int i = 0; i < groups.Count; ++i)
                    groups[i].transform.position = Camera.main.transform.forward * distance;
            }
        }
    }
}
