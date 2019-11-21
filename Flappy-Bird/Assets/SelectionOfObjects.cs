using System.Collections.Generic;
using UnityEngine;

public class SelectionOfObjects : MonoBehaviour
{
    [SerializeField] private InitsObject initsObject;
    [SerializeField] private List<object> initsGameObjects;
    private List<object> selectedGameObjects;
    [SerializeField] private List<GameObject> gList;

    private Vector2 startPos;
    private Vector2 endPos;
    private Rect rect;
    private bool cursorSelection;
    private GUISkin skin;
    void Start()
    {
        initsGameObjects=new List<object>();
        selectedGameObjects=new List<object>();
        initsObject.GoOnInits += AddInitsGoList;
        for (int i = 0; i < gList.Count; i++){
            initsGameObjects.Add(gList[i]);
        }
    }

    private void AddInitsGoList(object go){
        initsGameObjects.Add(go);
    }

    private bool CheckInitsGameObject(GameObject initsGameObject){
        bool result = false;
        for (int i = 0; i < selectedGameObjects.Count; i++)
        {
            if (initsGameObject == selectedGameObjects[i])
                result = true;
        }

        return result;
    }

    void OnGUI(){
        GUI.skin = skin;
        if (Input.GetMouseButtonDown(0)){
            startPos = Input.mousePosition;
            cursorSelection = true;
        }

        if (Input.GetMouseButtonUp(0)){
            cursorSelection = false;
            Selected();
        }

        if (cursorSelection){
            selectedGameObjects.Clear();
            endPos = Input.mousePosition;
            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                Screen.height - Mathf.Max(endPos.y, startPos.y),
                Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y));

            GUI.Box(rect, "");

            for (int i = 0; i < initsGameObjects.Count; i++){
                GameObject go=initsGameObjects[i] as GameObject;
                if (go == null){
                    return;
                }

                Vector2 objectPos = new Vector2(
                    Camera.main.WorldToScreenPoint(go.gameObject.transform.position).x,
                    Screen.height - Camera.main.WorldToScreenPoint(go.transform.position).y);
                if (rect.Contains(objectPos)){
                    if (selectedGameObjects.Count == 0){
                        selectedGameObjects.Add(go);
                    }
                    else{
                        if (!CheckInitsGameObject(go)){
                            selectedGameObjects.Add(go);
                        }
                    }
                }
            }
        }
    }

    private void Selected(){
        for (int i = 0; i < selectedGameObjects.Count; i++)
        {
            initsGameObjects.Remove(selectedGameObjects[i]);
            Destroy(selectedGameObjects[i] as GameObject);
        }
    }
}