using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// GameManager 클래스는 게임의 전반적인 상태(목표, 게임오버, 재시작 등)를 관리합니다.
public class GameManager : MonoBehaviour
{
    // 싱글턴 인스턴스 (다른 스크립트에서 GameManager.instance로 접근)
    public static GameManager instance = null;

    public bool isGameOver = false; // 게임 오버 여부
    public GameObject retryButton; // 재시도 버튼 오브젝트

    [SerializeField]
    private TextMeshProUGUI textGoal; // 목표 개수 표시용 텍스트

    [SerializeField]
    private int goal; // 남은 목표 핀 개수

    // 게임 오브젝트가 생성될 때 호출 (싱글턴 패턴 구현)
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 게임 시작 시 목표 개수 텍스트 초기화
    void Start()
    {
        textGoal.SetText(goal.ToString());
    }

    public void DecreaseGoal()
    {
        goal = goal - 1;
        if (goal <= 0)
        {
            SetGameOver(true); // 성공 시 게임 오버 처리
        }
        textGoal.text = goal.ToString(); // 목표 개수 갱신
    }

    // 게임 오버 처리 (성공/실패 여부에 따라 배경색 변경)
    public void SetGameOver(bool success)
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Camera.main.backgroundColor = success ? Color.green : Color.red;
        }
        Invoke("ShowRetryButton", 1f); // 1초 후 재시도 버튼 표시
    }

    // 재시도 버튼 활성화
    void ShowRetryButton()
    {
        retryButton.SetActive(true);
    }

    // 게임 재시작 (씬을 다시 로드)
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
