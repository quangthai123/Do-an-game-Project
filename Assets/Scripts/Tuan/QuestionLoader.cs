using UnityEngine;
using System.Collections.Generic;
using System.IO;  // Cần thiết để sử dụng File.Exists()

public class QuestionLoader : MonoBehaviour
{
    [SerializeField] private QuizDataScriptable quizDataScriptable;

    void Start()
    {
        LoadQuestionsForCurrentScene();
    }

    void LoadQuestionsForCurrentScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string jsonFileName = $"{sceneName}.json";  // Ví dụ: Level1.json
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (System.IO.File.Exists(filePath))  // Đảm bảo sử dụng System.IO.File.Exists
        {
            string jsonData = System.IO.File.ReadAllText(filePath);  // Đọc dữ liệu từ file

            // Giải mã JSON thành đối tượng QuestionList
            QuestionList loadedQuestions = JsonUtility.FromJson<QuestionList>(jsonData);

            quizDataScriptable.questions.Clear();  // Xóa câu hỏi cũ trong ScriptableObject

            foreach (var question in loadedQuestions.questions)
            {
                // Dựa vào kiểu câu hỏi để xử lý đúng
                switch (question.questionType)
                {
                    case QuestionType.TEXT:
                        question.qustionImage = null;  // Không cần ảnh, video hoặc âm thanh
                        question.qustionClip = null;
                        question.qustionVideo = null;
                        break;

                    case QuestionType.IMAGE:
                        // Giả sử bạn đã đưa đường dẫn ảnh vào
                        question.qustionClip = null;  // Không cần audio hoặc video
                        question.qustionVideo = null;
                        break;

                    case QuestionType.AUDIO:
                        // Giả sử bạn đã đưa đường dẫn file âm thanh vào
                        question.qustionImage = null;  // Không cần ảnh hoặc video
                        question.qustionVideo = null;
                        break;

                    case QuestionType.VIDEO:
                        // Giả sử bạn đã đưa đường dẫn video vào
                        question.qustionImage = null;  // Không cần ảnh hoặc âm thanh
                        question.qustionClip = null;
                        break;
                }

                quizDataScriptable.questions.Add(question);  // Thêm câu hỏi vào danh sách
            }

            Debug.Log($"Questions loaded for {sceneName}");
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }

}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}
