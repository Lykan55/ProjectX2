using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
    // Human1���擾
    public GameObject player;

    public GameObject WallDestroy;

    public float searchPosition = 40f;  // n�����̃`�����N�̌������l

    // �A�C�e���g�p���̕����𔻒� �{ �^���I�Ƀ��A���^�C���ŊĎ�
    public bool isWPressed = false;
    public bool isAPressed = false;
    public bool isSPressed = false;
    public bool isDPressed = false;

    // ���W+40�̈ʒu�̃I�u�W�F�N�g�e�I�u�W�F�N�g�̃^�O
    public string targetTagPlusX;
    public string targetTagMinusX;

    // Area�I�u�W�F�N�g����
    public GameObject AreaPlusX;
    public GameObject AreaMinusX;

    // Area�I�u�W�F�N�g�̃^�O���i�[
    public string targetAreaTagPlusX;
    public string targetAreaTagMinusX;

    // object���C���[
    public LayerMask Objectlayer;
    // Area���C���[
    public LayerMask Arealayer;

    // �A�C�e���̕����g�p�����p
    public bool ItemCheck = false;

    // ����p�ύX�Ɏg�p����
    public CinemachineVirtualCamera virtualCamera;
    public float newOrthoSize = 10f;
    private float originalOrthoSize;

    // ���A�C�e���Ɋ֌W
    public GoalDirectionItem goalDirectionItem; // �S�[�������A�C�e�����Ǘ�����X�N���v�g�̎Q��

    // �A�C�e���I���̗����Ɏg�p
    public List<Sprite> itemImages; // �A�C�e���摜�̃��X�g��Inspector�Őݒ�
    public Image[] itemSlots; // �A�C�e����\������UI Image�I�u�W�F�N�g�̔z��
    private List<Sprite> selectedItems = new List<Sprite>();

    void Start()
    {
        if (virtualCamera != null)
        {
            // ���݂�Orthographic Size��ۑ����Ă���
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
        // �A�C�e���������_���ɑI�сA�d�����Ȃ��悤�ɕ\������
        SelectItems();
        DisplayItems();
    }

    void Update()
    {
        // J�L�[�AK�L�[�AL�L�[�������ꂽ�Ƃ��ɃA�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseItem(0); // 1�L�[�������ꂽ�ꍇ
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            UseItem(1); // 2�L�[�������ꂽ�ꍇ
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            UseItem(2); // 3�L�[�������ꂽ�ꍇ
        }

        // �L�[�������ꂽ��true�ɐݒ�
        if (Input.GetKeyDown(KeyCode.W)) { isWPressed = true; }
        if (Input.GetKeyDown(KeyCode.A)) { isAPressed = true; }
        if (Input.GetKeyDown(KeyCode.S)) { isSPressed = true; }
        if (Input.GetKeyDown(KeyCode.D)) { isDPressed = true; }

        // �L�[�������ꂽ��false�ɖ߂�
        if (Input.GetKeyUp(KeyCode.W)) { isWPressed = false; }
        if (Input.GetKeyUp(KeyCode.A)) { isAPressed = false; }
        if (Input.GetKeyUp(KeyCode.S)) { isSPressed = false; }
        if (Input.GetKeyUp(KeyCode.D)) { isDPressed = false; }

    }



    void SelectItems()
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        // 3�̃A�C�e����I��
        while (selectedIndices.Count < 3)
        {
            int index = Random.Range(0, itemImages.Count);
            selectedIndices.Add(index);
        }

        // �I�΂ꂽ�A�C�e�������X�g�ɒǉ�
        selectedItems.Clear();
        foreach (int index in selectedIndices)
        {
            selectedItems.Add(itemImages[index]);
        }
        ItemCheck = true; // �����ŃA�C�e���g�p����
    }

    void DisplayItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < selectedItems.Count)
            {
                itemSlots[i].sprite = selectedItems[i];
                itemSlots[i].enabled = true; // �A�C�e��������ꍇ�͕\��
            }
            else
            {
                itemSlots[i].enabled = false; // �A�C�e�����Ȃ��ꍇ�͔�\��
            }
        }
    }

    void UseItem(int slotIndex)
    {
        if (ItemCheck == true)
        {
            ItemCheck = false; // �A�C�e���̎g�p�����b�N
            if (slotIndex >= 0 && slotIndex < selectedItems.Count)
            {
                Sprite item = selectedItems[slotIndex];
                string itemName = item.name; // �A�C�e���̖��O���擾
                Debug.Log($"{slotIndex}: {itemName}���g�p"); // �g�p�����A�C�e�����f�o�b�O���O�ɕ\��

                // �A�C�e���̃��\�b�h�������ɒǉ�

                if (itemName == "Lens")
                {
                    StartCoroutine(Lens());
                }
                else if (itemName == "ArrowItem")
                {
                    StartCoroutine(Arrow());
                }
                else if (itemName == "WallDestroy")
                {
                    //WallDestroy();
                }
            }
            else
            {
                Debug.Log("�A�C�e���X���b�g�ɃA�C�e��������܂���");
            }
        }
        else if (ItemCheck == false)
        {
            Debug.Log("�����ɕ����̃A�C�e�����g�p�ł��܂���");
        }
    }



    //�A�C�e���̏���
    public IEnumerator Lens()// ����p�A�C�e������
    {
        if (virtualCamera != null)
        {
            // Ortho Size��ύX
            virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

            // �T�b�ҋ@
            yield return new WaitForSeconds(5f);

            // Ortho Size�����ɖ߂�
            virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
        }
        ItemCheck = true;
    }



    public IEnumerator Arrow()// ���A�C�e������
    {
        // GoalDirectionItem�X�N���v�g���Q��
        goalDirectionItem.UseGoalDirectionItem(); // �S�[���̕�����\�����郁�\�b�h���Ăяo��
        // �R�b�ҋ@
        yield return new WaitForSeconds(3f);
        ItemCheck = true;
    }

    








    /*
    public void WallDestroy() // �ǔj��A�C�e������
    {
        FindObjectPosition();// ���͂̃I�u�W�F�N�g�̍��W���擾

        //Human1�����ݐڐG���Ă���^�O�����Ƃɔ���
        if (player.GetComponent<Human1>().PlayerCollision.gameObject.tag == "wall")// �܂��͕ǂɐڐG���Ă鎞�����Ɍ���
        {
            if (AreaPlusX != null && targetAreaTagPlusX == "Area")
            {
                if (targetTagPlusX != "wall" && isDPressed)
                {
                    player.GetComponent<Human1>().PlayerCollision.transform.parent.gameObject.SetActive(false);
                    Debug.Log("�E�̕ǂ�j��");
                }
            }
            if (AreaMinusX != null|| targetTagPlusX == null && targetAreaTagMinusX == "Area")
            {
                if (targetTagMinusX != "wall" && isAPressed)
                {
                    player.GetComponent<Human1>().PlayerCollision.transform.parent.gameObject.SetActive(false);
                    Debug.Log("���̕ǂ�j��");
                }
            }
            else 
            {
                Debug.Log("������j�󂷂邱�Ƃ͂ł��܂���");
            }
        }
        ItemCheck = true;
    }
    */


    /*-----------------------------------------------------------------------------------------------------------------------------------------------*/


    // �Q�`�����N��ɂ���I�u�W�F�N�g�̃^�O���擾���郁�\�b�h
    public void FindObjectPosition()
    {
        // ���݂̃`�����N�ʒu����x���W�� +40 �����ꏊ
        Vector2 targetPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y + 9.5f);

        // ���݂̃`�����N�ʒu����x���W�� -40 �����ꏊ
        Vector2 targetPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y + 9.5f);

// �Q�`�����N���Area���C���[�̒��S�̍��W

        // ���݂̃`�����N�ʒu����x���W�� +40 �����ꏊ
        Vector2 targetAreaPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // ���݂̃`�����N�ʒu����x���W�� -40 �����ꏊ
        Vector2 targetAreaPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

// OverlapPoint���g���āA�Q�`�����N��ɂ���I�u�W�F�N�g���擾

        // Object���C���[�̂Q�`�����N��ɂ���I�u�W�F�N�g���擾
        Collider2D targetObjectPlusX = Physics2D.OverlapPoint(targetPositionPlusX, Objectlayer);    // X�� +40
        Collider2D targetObjectMinusX = Physics2D.OverlapPoint(targetPositionMinusX, Objectlayer);  // X�� -40

        // Area���C���[�̂Q�`�����N��ɂ���I�u�W�F�N�g���擾
        Collider2D targetAreaPlusX = Physics2D.OverlapPoint(targetAreaPositionPlusX, Arealayer);    // X�� +40
        Collider2D targetAreaMinusX = Physics2D.OverlapPoint(targetAreaPositionMinusX, Arealayer);  // X�� -40

// �Q�`�����N���Area���C���[�̃I�u�W�F�N�g
        if (targetAreaPlusX != null && isDPressed)
        {
            AreaPlusX = targetAreaPlusX.gameObject;
        }
        else if (targetAreaMinusX != null && isAPressed)
        {
            AreaMinusX = targetAreaMinusX.gameObject;
        }
        

// �Q�`�����N���Area���C���[�̃^�O���擾
        if (targetAreaPlusX != null && isDPressed)
        {
            targetAreaTagPlusX = targetAreaPlusX.gameObject.tag;
        }
        else if (targetAreaMinusX != null && isAPressed)
        {
            targetAreaTagMinusX = targetAreaMinusX.gameObject.tag;
        }

// �Q�`�����N���object���C���[�̏�̃^�O���擾
        if (targetObjectPlusX != null && targetObjectPlusX.transform.parent != null && isDPressed)
        {
            targetTagPlusX = targetObjectPlusX.gameObject.tag;
        }

        else if (targetObjectMinusX != null && targetObjectMinusX.transform.parent != null && isAPressed)
        {
            targetTagMinusX = targetObjectMinusX.gameObject.tag;
        }
    }
}
