using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool _rotateFlag;
    private bool _scaleFlag;
    private bool _translateFlag;
    private bool _holdFlag;
    private bool _objectSelected;
    private GameObject _gameObjectSelected;
    public Button scaleButton;
    public Button rotateButton;
    public Button translateButton;
    public Button holdButton;
    
    void Start()
    {
        _gameObjectSelected = null;
        _objectSelected = false;
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
            
            _scaleFlag = true;
            scaleButton.GetComponent<Image>().color = Color.cyan;

        }
        else if (id == 2)
        {
            
            _scaleFlag = false;
            _holdFlag = false;
            _translateFlag = false;
            translateButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            holdButton.GetComponent<Image>().color = Color.white;
            
            _rotateFlag = true;
            rotateButton.GetComponent<Image>().color = Color.cyan;
        }
        else if (id == 3)
        {
            _scaleFlag = false;
            _rotateFlag = false;
            _holdFlag = false;
            holdButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            rotateButton.GetComponent<Image>().color = Color.white;
            
            _translateFlag = true;
            translateButton.GetComponent<Image>().color = Color.cyan;
        }
        else if (id == 4)
        {
            _scaleFlag = false;
            _rotateFlag = false;
            _translateFlag = false;
            translateButton.GetComponent<Image>().color = Color.white;
            scaleButton.GetComponent<Image>().color = Color.white;
            rotateButton.GetComponent<Image>().color = Color.white;
            
            _holdFlag = true;
            holdButton.GetComponent<Image>().color = Color.cyan;
            
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
                Debug.Log("Hit object");
                _objectSelected = !_objectSelected;
                if (_objectSelected)
                {
                    _gameObjectSelected = hit.collider.gameObject;

                }
                else
                {
                    _gameObjectSelected = null;
                }
            }

        }

        if (_gameObjectSelected != null)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && _rotateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                _gameObjectSelected.transform.Rotate(-touchDeltaPosition.x * 0.1f, 0, -touchDeltaPosition.y * 0.1f);
            }
            else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && _scaleFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                var localScale = _gameObjectSelected.transform.localScale;
                localScale = new Vector3(
                    localScale.x + touchDeltaPosition.y * 0.001f,
                    localScale.y + touchDeltaPosition.y * 0.001f,
                    localScale.z + touchDeltaPosition.y * 0.001f);
                _gameObjectSelected.transform.localScale = localScale;
            }
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && _translateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                _gameObjectSelected.transform.Translate(-touchDeltaPosition.x * 0.01f, 0,
                    0);
            }
            else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && _translateFlag)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                _gameObjectSelected.transform.Translate(0, -touchDeltaPosition.y * 0.01f, 0);
            }
            else if (_holdFlag)
            {
                const float distance = 10.0f;
                _gameObjectSelected.transform.position = Camera.main.transform.forward * distance;
            }
        }
    }
}
