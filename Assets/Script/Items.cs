using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;

public class ItemManager : MonoBehaviour
{
    //�A�C�e���̕����g�p�����p
    public bool ItemCheck = true;

    //����p�ύX�Ɏg�p����
    public CinemachineVirtualCamera virtualCamera;
    public float newOrthoSize = 10f;
    private float originalOrthoSize;

    //���A�C�e���Ɋ֌W
    public GoalDirectionItem goalDirectionItem; // �S�[�������A�C�e�����Ǘ�����X�N���v�g�̎Q��


    //�A�C�e���I���̗����Ɏg�p
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
        // 1�L�[�A2�L�[�A3�L�[�������ꂽ�Ƃ��ɃA�C�e�����g�p����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0); // 1�L�[�������ꂽ�ꍇ
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1); // 2�L�[�������ꂽ�ꍇ
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2); // 3�L�[�������ꂽ�ꍇ
        }
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
            if (slotIndex >= 0 && slotIndex < selectedItems.Count)
            {
                Sprite item = selectedItems[slotIndex];
                string itemName = item.name; // �A�C�e���̖��O���擾
                Debug.Log($"Using item in slot {slotIndex}: {itemName}"); // �f�o�b�O���O��ǉ�

                // �A�C�e���̃��\�b�h�������ɒǉ�

                if (itemName == "Lens")
                {
                    Lens();
                }
                else if (itemName == "ArrowItem")
                {
                    Arrow();
                }
            }
            else
            {
                Debug.Log("�A�C�e���X���b�g�ɃA�C�e��������܂���");
            }
        }
        else if (ItemCheck != true)
        {
            Debug.Log("�����ɕ����̃A�C�e�����g�p�ł��܂���");
        }
          
    }


    //�A�C�e���̏���
    public void Lens()//����p�ύX�A�C�e������
    {
        ItemCheck = false;
        if (virtualCamera != null)
        {
            // Ortho Size��ύX
            virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

            // 5�b��Ɍ��ɖ߂�Coroutine���J�n
            StartCoroutine(ResetOrthoSizeAfterDelay(5f));
        }
    }

    //����p�ύX����
    IEnumerator ResetOrthoSizeAfterDelay(float delay)
    {
        // �w�肵�����Ԃ����ҋ@
        yield return new WaitForSeconds(delay);

        // Ortho Size�����ɖ߂�
        virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
        ItemCheck = true;

    }


    public void Arrow()//���A�C�e������
    {
        ItemCheck = false;
        //GoalDirectionItem�X�N���v�g���Q��
        goalDirectionItem.UseGoalDirectionItem(); // �S�[���̕�����\�����郁�\�b�h���Ăяo��
        //ItemCheck = true;
    }
}
