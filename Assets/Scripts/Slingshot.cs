using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour {
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;
    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public float birdPositionOffset;

    public float baseForce;

    [SerializeField] GameObject psSlingshotEffectFirst;
    [SerializeField] GameObject psSlingshotEffectSecond;
    [SerializeField] AudioSource superSlingshotAudio;
    [SerializeField] AudioSource boxerAppearance;
    [SerializeField] AudioSource bombermanAppearance;


    [SerializeField] List<StickmanController> stickmansList;
    int indexOfNextStickman = 0;
    GameObject placedStickman;
    GameObject currentStickman;
    Rigidbody2D placedStickmanBodyRigidBody;
    Collider2D placedStickmanBodyCollider;
    StickmanType placedStickmanType;
    [SerializeField] GameObject boxerStickmanPrefab;
    GameObject boxerClone;
    [SerializeField] GameObject bombermanStickmanPrefab;
    GameObject bombermanClone;
    
    bool isPowerfulSlingshot;
     

    public enum StickmanType {
        Default,
        Breaking,
        Hulk,
        Boxer,
        StoneBreaker,
        Bomb
    }

    bool isOnBreakingActions;
    bool isOnBombActions; // переделать нахуй
    public int countOfLeftStickmans;
    public bool isLeftZeroStickmans;

    [SerializeField] FingerTouch fingerTouch;

    void Start() {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        PutNextStickmanToSlingshot();

        countOfLeftStickmans = stickmansList.Count;
    }

    void PutNextStickmanToSlingshot() {
        if (indexOfNextStickman < stickmansList.Count) {
            placedStickman = stickmansList[indexOfNextStickman].gameObject;
            indexOfNextStickman++;

            GameObject stickmanBody = GetStickmanBody(placedStickman);
            placedStickmanBodyRigidBody = stickmanBody.GetComponent<Rigidbody2D>();
            placedStickmanBodyCollider = placedStickmanBodyRigidBody.GetComponent<Collider2D>();
            placedStickmanType = placedStickman.GetComponent<StickmanController>().stickmanType;

            ActivatePutStickmanFeatures(placedStickmanBodyCollider, placedStickmanBodyRigidBody);

            ResetStrips();
        } else {
            // конец уровня если не победа
        } 
    }

    void ActivatePutStickmanFeatures(Collider2D collider2D, Rigidbody2D rigidbody2D) {
        collider2D.enabled = false;
        rigidbody2D.gravityScale = 1; // СДЕЛАТЬ ЧТО БЫ ГОЛОВА А НЕ ТЕЛО
        rigidbody2D.isKinematic = true;
        DeactivateAngleLimitsForBodyParts(placedStickman);
    }

    void DeactivatePutStickmanFeatures() {
        placedStickmanBodyCollider.enabled = true;
        placedStickmanBodyRigidBody.gravityScale = -7;
        placedStickmanBodyRigidBody.isKinematic = false;
    }
    

    GameObject GetStickmanBody(GameObject stickman) {
        return stickman.transform.GetChild(0).gameObject;
    }

    void ActivateAngleLimitsForBodyParts(GameObject stickman) {
        for (int i = 1; i < 11; i++) {
            HingeJoint2D hingeJointComponent = stickman.transform.GetChild(i).GetComponent<HingeJoint2D>();
            hingeJointComponent.useLimits = true;
        }
    }

    void DeactivateAngleLimitsForBodyParts(GameObject stickman) {
        for (int i = 1; i < 11; i++) {
            HingeJoint2D hingeJointComponent = stickman.transform.GetChild(i).GetComponent<HingeJoint2D>();
            hingeJointComponent.useLimits = false;
        }
    }

    void Update() {
        if (isMouseDown) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (placedStickmanBodyCollider) {
                placedStickmanBodyCollider.enabled = true;
            }
        }
        else {
            ResetStrips();
        }

        if (Input.GetMouseButtonDown(0)) {
            if (isOnBreakingActions) {
                BreakBodyParts();
                isOnBreakingActions = false;
            }

            if (isOnBombActions) {
                currentStickman.transform.GetChild(1).GetComponent<Bomb>().Explode();
                isOnBombActions = false;
            }
        }

        if (countOfLeftStickmans == 0) {
            isLeftZeroStickmans = true;
        }
            
    }

    void OnMouseDown() {
        isMouseDown = true;
    }
    

    void BreakBodyParts() {
        for (int i = 1; i < 11; i++) {
            Destroy(currentStickman.transform.GetChild(i).GetComponent<HingeJoint2D>());
        }
    }

    void OnMouseUp() {
        isMouseDown = false;
        if (placedStickman != null) {
            Shoot();
        }
        currentPosition = idlePosition.position;
    }

    void Shoot() {
        var changeableForce = baseForce;
        if (isPowerfulSlingshot) {
            changeableForce = baseForce * 1.5f;
            isPowerfulSlingshot = false;
            psSlingshotEffectFirst.SetActive(false);
            psSlingshotEffectSecond.SetActive(false);
            superSlingshotAudio.Stop();
        }

        placedStickmanBodyRigidBody.isKinematic = false;
        Vector3 stickmanForce = (currentPosition - center.position) * changeableForce * -1;
        placedStickmanBodyRigidBody.velocity = stickmanForce;

        placedStickmanBodyRigidBody.GetComponent<Bird>().Release();

        if (placedStickmanType == StickmanType.Breaking) {
            isOnBreakingActions = true;
            currentStickman = placedStickman;
        }

        if (placedStickmanType == StickmanType.Bomb) {
            isOnBombActions = true;
            currentStickman = placedStickman;
        }

        if (fingerTouch != null && placedStickmanType != StickmanType.Default) {
            fingerTouch.GetComponent<FingerTouch>().StartFingerTouchAnimation();
        }

        placedStickman.GetComponent<StickmanController>().isShooted = true;

        
        

        ActivateAngleLimitsForBodyParts(placedStickman);
        

        // Обнуляем переменные вылетевшего из рогатки Стикмена
        placedStickman = null;
        placedStickmanBodyRigidBody = null;
        placedStickmanBodyCollider = null;
        countOfLeftStickmans--;

        Invoke("PutNextStickmanToSlingshot", 2);
    }

    public bool ActivateSlingshotPowerUp() {
        if (!isLeftZeroStickmans & !isPowerfulSlingshot) {
            isPowerfulSlingshot = true;
            psSlingshotEffectFirst.SetActive(true);
            psSlingshotEffectSecond.SetActive(true);
            superSlingshotAudio.Play();
            return true;
        } else {
            return false;
        }
    }

    public void DeactivateSlingshot() {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void ActivateSlingshot() {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public bool ActivateHulkPowerUp() {
        if (placedStickman != null) {
            int j = 12;
            StickmanController stickmanController = placedStickman.GetComponent<StickmanController>();
            if (stickmanController.stickmanType == StickmanType.Boxer) {
                j = 14;
            }
            for (int i = 0; i < j; i++) {
                // делаем части тела халками
                StickmanPartController stickmanPartController = placedStickman.transform.GetChild(i).GetComponent<StickmanPartController>();
                stickmanPartController.stickmanType = StickmanType.Hulk;
                // И зелёными
                stickmanPartController.BecomeGreen();
                stickmanController.PlayHulkAudio();

            }
            placedStickman.transform.localScale += new Vector3(0.8f, 0.8f, 0);
            return true;
        } else {
            return false;
        }
        
    }

    // Зарефакторить!!!

    public bool InstantiateAndPlaceBombermanInSlingshot() {
        if (placedStickman != null) { // сделать так что бы боксёра можно было активировать даже когда не осталось стикменов
            bombermanClone = Instantiate(bombermanStickmanPrefab, new Vector3(gameObject.transform.position.x + 6,
                gameObject.transform.position.y + 1, gameObject.transform.position.z), Quaternion.identity);

        bombermanAppearance.Play();

        PutBombermanInSlingshot(); 

        return true;
        }
        return false;
    }

    void PutBombermanInSlingshot() {
        var magicNumber = stickmansList.Count - countOfLeftStickmans;
        
        stickmansList.Add(bombermanClone.GetComponent<StickmanController>());

        for (int i = stickmansList.Count - 2; i > magicNumber - 1; i--) {
            stickmansList[i + 1] = stickmansList[i];
        }

        stickmansList[magicNumber] = bombermanClone.GetComponent<StickmanController>();

        indexOfNextStickman--; // мб переделать
        countOfLeftStickmans++;
        DeactivatePutStickmanFeatures();
        SwapStickmanPositionsForBomberman();
        PutNextStickmanToSlingshot();
    }

    void SwapStickmanPositionsForBomberman() {
        bombermanClone.transform.position = placedStickman.transform.position;
        placedStickman.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y + 1, gameObject.transform.position.z);
    }


    public bool InstantiateAndPlaceBoxerInSlingshot() {
        if (placedStickman != null) { // сделать так что бы боксёра можно было активировать даже когда не осталось стикменов
            boxerClone = Instantiate(boxerStickmanPrefab, new Vector3(gameObject.transform.position.x + 6,
                gameObject.transform.position.y + 1, gameObject.transform.position.z), Quaternion.identity);

        boxerAppearance.Play();

        PutBoxerInSlingshot(); 

        return true;
        }
        return false;
    }

    void PutBoxerInSlingshot() {
        var magicNumber = stickmansList.Count - countOfLeftStickmans;
        
        stickmansList.Add(boxerClone.GetComponent<StickmanController>());

        for (int i = stickmansList.Count - 2; i > magicNumber - 1; i--) {
            stickmansList[i + 1] = stickmansList[i];
        }

        stickmansList[magicNumber] = boxerClone.GetComponent<StickmanController>();

        indexOfNextStickman--; // мб переделать
        countOfLeftStickmans++;
        DeactivatePutStickmanFeatures();
        SwapStickmanPositionsForBoxer();
        PutNextStickmanToSlingshot();
    }

    void SwapStickmanPositionsForBoxer() {
        boxerClone.transform.position = placedStickman.transform.position;
        placedStickman.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y + 1, gameObject.transform.position.z);
    }

    void ResetStrips() {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position) {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (placedStickmanBodyRigidBody) {
            Vector3 dir = position - center.position;
            placedStickmanBodyRigidBody.transform.position = position + dir.normalized * birdPositionOffset;
            placedStickmanBodyRigidBody.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector) {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
