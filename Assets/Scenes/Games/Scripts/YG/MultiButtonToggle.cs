using UnityEngine;
using UnityEngine.UI;

public class MultiButtonToggle : MonoBehaviour
{
    public Button[] buttonss; // 버튼들의 배열
    public Color pressedColor;
    
    private bool[] isPressed; // 각 버튼의 누른 상태를 저장하는 배열
    private Color[] normalColors;

    private void Start()
    {
        normalColors = new Color[buttonss.Length];
        isPressed = new bool[buttonss.Length]; // 버튼 배열과 같은 크기의 상태 배열 생성 및 초기화

        // 초기 버튼 색상 저장
        for (int i = 0; i < buttonss.Length; i++)
        {
            normalColors[i] = buttonss[i].colors.normalColor;
        }
    }

    public void ToggleButton(int buttonIndex)
    {
        isPressed[buttonIndex] = !isPressed[buttonIndex]; // 해당 버튼의 상태를 반전
        
        if (isPressed[buttonIndex])
        {
            // 버튼이 눌린 상태일 때의 처리
            buttonss[buttonIndex].image.color = pressedColor;
            Debug.Log("Button " + buttonIndex + " is pressed.");
        }
        else
        {
            // 버튼이 눌리지 않은 상태일 때의 처리
            buttonss[buttonIndex].image.color = normalColors[buttonIndex];
            Debug.Log("Button " + buttonIndex + " is released.");
        }
    }

    public void ResetButtonStates()
    {
        for (int i = 0; i < isPressed.Length; i++)
        {
            isPressed[i] = false;
            buttonss[i].interactable = true;
            buttonss[i].image.color = normalColors[i];
        }
        
        Debug.Log("All buttons are released.");
    }

}