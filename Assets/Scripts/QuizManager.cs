using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class QuizManager : MonoBehaviour
{

    //canvas where the question appears
    public GameObject questionCanvas;

    //question title
    public Text questionTitle;

    //score text
    public Text scoreText;

    //buttons
    public GameObject btnTrue, btnFalse, btnContinue; 

    //flag to indicate whether we are showing questions or not
    bool isShowingQuestions;

    //quiz
    Quiz quiz;

    //time (used to show questions)
    float elapsedTime = 0;

    //next question
    Interaction nextQuestion;

    //next question index
    int nextQuestionIndex;

    //total correct answers
    int totalCorrect = 0;

    //total questions
    int numQuestionsResponded = 0;

    //json
    string path, jsonString;

    // Use this for initialization
    void Start()
    {
        //set initial score
        scoreText.text = "Puntaje: 0 / 0";

        //pause quiz
        PauseQuiz();

        // hide question canvas
        questionCanvas.SetActive(false);

        path = Path.Combine(Application.streamingAssetsPath, "vr-quiz.json");

        jsonString = File.ReadAllText(path);

        // save json in our quiz
        quiz = JsonUtility.FromJson<Quiz>(jsonString);

        //prepare next question
        PrepareNext();
    }

    // Update is called once per frame
    void Update()
    {
        //check that we should be showing questions
        if (!isShowingQuestions) return;

        //increase elapsed time
        elapsedTime += Time.deltaTime;

        // check time, if a question is due, show it!
        if (elapsedTime > nextQuestion.time)
        {
            //show question
            if (nextQuestion.type == 0)
            {
                btnContinue.gameObject.SetActive(false);
                btnTrue.gameObject.SetActive(true);
                btnFalse.gameObject.SetActive(true);
            }
            else
            {
                btnContinue.gameObject.SetActive(true);
                btnTrue.gameObject.SetActive(false);
                btnFalse.gameObject.SetActive(false);
            }

            //1) show question canvas
            questionCanvas.SetActive(true);

            //2) set question title
            questionTitle.text = nextQuestion.title;

            //3) pause the quiz
            PauseQuiz();
        }
    }

    void PauseQuiz()
    {
        // no showing questions
        isShowingQuestions = false;
    }

    void ResumeQuiz()
    {
        // continue measuring elapsed time
        isShowingQuestions = true;
    }

    void PrepareNext()
    {
        //setting the first value
        if (nextQuestion == null)
        {
            //set index to the start of the array
            nextQuestionIndex = 0;

            //save next question
            nextQuestion = quiz.interactions[nextQuestionIndex];
        }
        else
        {
            //increase the question index
            nextQuestionIndex++;

            //check that there are more question left
            if (nextQuestionIndex < quiz.interactions.Length)
            {
                //save next question
                nextQuestion = quiz.interactions[nextQuestionIndex];
            }
            else
            {
                //we've completed the quiz
                questionCanvas.SetActive(false);

                scoreText.text += "\n¡Quiz Completo!";

                return;
            }
        }

        ResumeQuiz();
    }

    public void HandleAnswer(bool response)
    {
        //hide the question canvas
        questionCanvas.SetActive(false);

        if (nextQuestion.type == 0)
        {
            //increase number of responded question
            numQuestionsResponded++;

            //check if the answer was correct
            if (response == nextQuestion.correct)
            {
                totalCorrect++;
                scoreText.text = "¡Correcto!";
            }
            else
            {
                scoreText.text = "¡Respuesta equivocada!";
            }

            scoreText.text += "\nPuntaje: " + totalCorrect + " / " + numQuestionsResponded;
        }

        PrepareNext();
    }
}
