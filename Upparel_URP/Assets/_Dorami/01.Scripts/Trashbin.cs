using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trashbin : MonoBehaviour, IPointerClickHandler
{
    public Material newMaterial;
    public GameObject UIPanel;

    // Start is called before the first frame update
    void Start()
    {
        UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 터치 또는 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray;
            if (Input.touchCount > 0)
            {
                // 터치 입력
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                // 마우스 클릭 입력
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 객체가 Trashbin인지 확인
                Trashbin trashbin = hit.collider.GetComponent<Trashbin>();
                if (trashbin != null)
                {
                    if(!UIPanel.activeSelf){
                        // UI Panel 활성화
                        UIPanel.SetActive(true);
                        PlayerMove.Instance.animator.SetTrigger("IdleToPickup");
                        PlayerMove.Instance.animator.SetTrigger("PickupToThink");
                    }else{
                        UIPanel.SetActive(false);
                        PlayerMove.Instance.animator.SetTrigger("ThinkToIdle");
                    }
                    
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            gameObject.GetComponent<MeshRenderer>().material = newMaterial;
            Debug.Log("부딪힘");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 기본 클릭 이벤트 처리
    }
}
