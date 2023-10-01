import React, { useState, useEffect } from "react";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import GameButton from "./GameButton";
import { Slider } from "@mui/material";
import CustomizedSwitch from "./GameSwitch";

const defaultTheme = createTheme();

export default function Quiz() {
  interface JsonData {
    twoDigitElements: {
      number: string;
      text: string;
    }[];
  }
  interface Answer {
    text: string;
    isCorrect: boolean;
  }
  const centerContentStyles = {
    margin: "5vh",
  };
  const [jsonData, setJsonData] = useState<JsonData | null>(null);
  // State to manage game logic
  const [gameStarted, setGameStarted] = useState(false);
  const [time, setTime] = useState(30);
  const [score, setScore] = useState(0);
  const [numericalQuestionsMode, setNumericalQuestionsMode] = useState(true);
  const [selectedAnswerState, setSelectedAnswerState] = useState<boolean>();
  const [sliderValues, setSliderValue] = React.useState<number[]>([0, 10]);
  const [questionData, setQuestionData] = useState({
    question: "00",
    answers: [
      { text: "THING", isCorrect: true },
      { text: "THING", isCorrect: false },
      { text: "THING", isCorrect: false },
      { text: "THING", isCorrect: false },
    ],
  });
  //GameButton data
  const borderWidths = [
    "3px 4px 3px 5px",
    "4px 3px 5px 3px",
    "5px 4px 2px 4px",
    "2px 5px 4px 3px",
  ];
  const borderRadiuses = [
    "95% 4% 92% 5%/20% 40% 6%",
    "95% 60% 14% 5%/4% 20% 6%",
    "97% 21% 30% 22%/2% 40% 6%",
    "95% 4% 92% 5%/4% 40% 12%",
  ];
  const buttonsData = questionData.answers.map((answer, index) => ({
    buttonText: `${answer.text}`,
    svgPath: answer.isCorrect
      ? "M12,2C6.5,2,2,6.5,2,12s4.5,10,10,10s10-4.5,10-10S17.5,2,12,2z M10.8,16.8l-3.7-3.7l1.4-1.4l2.2,2.2l5.8-6.1L18,9.3 L10.8,16.8z"
      : "M12,2A10,10,0,1,0,22,12,10,10,0,0,0,12,2Zm3.71,12.29a1,1,0,0,1,0,1.42,1,1,0,0,1-1.42,0L12,13.42,9.71,15.71a1,1,0,0,1-1.42,0,1,1,0,0,1,0-1.42L10.58,12,8.29,9.71A1,1,0,0,1,9.71,8.29L12,10.58l2.29-2.29a1,1,0,0,1,1.42,1.42L13.42,12Z",
    svgClassName: answer.isCorrect ? "fill-green-600" : "fill-red-600",
    onClick: () => handleAnswer(answer.isCorrect),
    borderWidth: borderWidths[index],
    borderRadius: borderRadiuses[index],
  }));
  useEffect(() => {
    fetch("src/pages/games/TwoDigitSystem.json")
      .then((response) => response.json())
      .then((data) => {
        setJsonData(data);
      })

      .catch((error) => {
        console.error("Error fetching JSON data:", error);
      });
  }, []);
  useEffect(() => {
    const timer = setInterval(() => {
      if (time > 0 && gameStarted) {
        setTime(time - 1);
      } else {
        clearInterval(timer);
      }
    }, 1000);
    if (time <= 0) {
      endGame();
    }
    return () => {
      clearInterval(timer);
    };
  }, [time]);
  function handleNumericalQuestionsModeChange() {
    if (!numericalQuestionsMode) {
      const newQuestionData = {
        question: "00",
        answers: [
          { text: "THING", isCorrect: true },
          { text: "THING", isCorrect: false },
          { text: "THING", isCorrect: false },
          { text: "THING", isCorrect: false },
        ],
      };
      setQuestionData(newQuestionData);
    } else if (numericalQuestionsMode) {
      const newQuestionData = {
        question: "THING",
        answers: [
          { text: "00", isCorrect: true },
          { text: "01", isCorrect: false },
          { text: "02", isCorrect: false },
          { text: "03", isCorrect: false },
        ],
      };
      setQuestionData(newQuestionData);
    }
    setNumericalQuestionsMode(!numericalQuestionsMode);
  }
  function setGameButtonBackgroundColor() {
    if (gameStarted && numericalQuestionsMode) {
      return "#51D6FF";
    } else if (!gameStarted && numericalQuestionsMode) {
      return "#C2ECF8";
    } else if (gameStarted && !numericalQuestionsMode) {
      return "#FDE74C";
    } else if (!gameStarted && !numericalQuestionsMode) {
      return "#FCF2A6";
    }
  }
  function initializeGame() {
    setScore(0);
    setTime(30);
    setRandomQuestion();
    setGameStarted(true);
  }
  function resetGame() {
    setGameStarted(false);
  }
  function endGame() {
    setGameStarted(false);
  }
  const shuffleArray = (array: Answer[]) => {
    for (let i = array.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  };
  const handleAnswer = (isCorrect: boolean) => {
    setSelectedAnswerState(isCorrect);
    if (isCorrect) {
      setScore(score + 1);
      setTime(time + 3);
    } else {
      setTime(time - 3);
    }
    setRandomQuestion();
  };
  function setRandomQuestion() {
    console.log("setRandomQuestion");
    if (jsonData) {
      const generatedValues = new Set<string>();
      let randomNumbers: string[] = [];

      while (randomNumbers.length < 4) {
        const randomValue = generateTwoDigitString(
          sliderValues[0],
          sliderValues[1]
        );

        if (!generatedValues.has(randomValue)) {
          randomNumbers.push(randomValue);
          generatedValues.add(randomValue);
        }
      }

      const textArray = randomNumbers.map(
        (randomNumber) =>
          (jsonData.twoDigitElements.find(
            (item) => item.number === randomNumber
          )?.text || "") as string
      );
      if (numericalQuestionsMode) {
        const newQuestionData = {
          question: randomNumbers[0],
          answers: shuffleArray([
            { text: textArray[0], isCorrect: true },
            { text: textArray[1], isCorrect: false },
            { text: textArray[2], isCorrect: false },
            { text: textArray[3], isCorrect: false },
          ]),
        };
        setQuestionData(newQuestionData);
      } else if (!numericalQuestionsMode) {
        const newQuestionData = {
          question: textArray[0],
          answers: shuffleArray([
            { text: randomNumbers[0], isCorrect: true },
            { text: randomNumbers[1], isCorrect: false },
            { text: randomNumbers[2], isCorrect: false },
            { text: randomNumbers[3], isCorrect: false },
          ]),
        };
        setQuestionData(newQuestionData);
      }
    }
  }
  function generateTwoDigitString(min: number, max: number) {
    const randomNumber = Math.floor(Math.random() * (max - min + 1)) + min;
    const twoDigitString =
      randomNumber < 10 ? `0${randomNumber}` : `${randomNumber}`;
    return twoDigitString;
  }
  function handleSliderChange(
    event: Event,
    newValue: number | number[],
    activeThumb: number
  ) {
    if (!Array.isArray(newValue)) {
      return;
    }
    const minDistance = 3;
    if (newValue[1] - newValue[0] < minDistance) {
      if (activeThumb === 0) {
        const clamped = Math.min(newValue[0], 99 - minDistance);
        setSliderValue([clamped, clamped + minDistance]);
      } else {
        const clamped = Math.max(newValue[1], minDistance);
        setSliderValue([clamped - minDistance, clamped]);
      }
    } else {
      setSliderValue(newValue as number[]);
    }
  }
  function formatSliderValue(value: number) {
    return value.toLocaleString("en-US", {
      minimumIntegerDigits: 2,
      useGrouping: false,
    });
  }

  return (
    <div style={centerContentStyles}>
      <ThemeProvider theme={defaultTheme}>
        <Grid container spacing={3}>
          <Grid
            item
            xs={6}
            container
            alignItems="center"
            justifyContent="flex-end"
          >
            <div
              style={{
                backgroundColor: gameStarted ? "#1F6DBB" : "#759ABF",
                color: "white",
                padding: "10px 15px",
                borderRadius: "5px",
                width: "238px",
                WebkitBorderRadius:
                  "255px 15px 225px 15px/15px 225px 15px 255px",
              }}
            >
              <Typography variant="h6" align="center" fontWeight={"bold"}>
                {time}s
              </Typography>
            </div>
          </Grid>
          <Grid
            item
            xs={6}
            container
            alignItems="center"
            justifyContent="flex-start"
          >
            <div
              style={{
                backgroundColor: gameStarted ? "#1F6DBB" : "#759ABF",
                color: "white",
                padding: "10px 15px",
                borderRadius: "5px",
                width: "238px",
                WebkitBorderRadius:
                  "255px 15px 225px 15px/15px 225px 15px 255px",
              }}
            >
              <Typography variant="h6" align="center" fontWeight={"bold"}>
                {score}
              </Typography>
            </div>
          </Grid>
        </Grid>
        <div
          style={{
            width: "100%",
            display: "flex",
            justifyContent: "center",
            marginTop: "10px",
          }}
        >
          <div
            style={{
              backgroundColor: gameStarted ? "#9c27b0" : "#AC8FB1",
              fontWeight: "bold",
              color: "white",
              padding: "10px 15px",
              borderRadius: "5px",
              width: "500px",
              marginBottom: "10px",
              position: "relative",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              alignSelf: "center",
              WebkitBorderRadius: "255px 15px 225px 15px/15px 225px 15px 255px",
            }}
          >
            <Typography variant="h6" align="center" fontWeight={"bold"}>
              {questionData.question}
            </Typography>
          </div>
        </div>
        <Grid container spacing={2}>
          {buttonsData.map((buttonData, index) => (
            <Grid
              item
              xs={6}
              key={index}
              container
              alignItems="center"
              justifyContent={index % 2 === 0 ? "flex-end" : "flex-start"}
            >
              <GameButton
                disabled={!gameStarted}
                backgroundColor={setGameButtonBackgroundColor()}
                {...buttonData} // Unpack the object with data as props
              />
            </Grid>
          ))}
        </Grid>

        <div
          style={{
            marginTop: "10px",
            marginBottom: "10px",
            marginLeft: "10%",
            marginRight: "10%",
          }}
        >
          <Slider
            getAriaLabel={() => "Minimum distance shift"}
            value={sliderValues}
            onChange={handleSliderChange}
            valueLabelDisplay="auto"
            disableSwap
            valueLabelFormat={formatSliderValue}
            min={0}
            max={99}
            disabled={gameStarted}
          />
        </div>

        <Grid container spacing={3}>
          <Grid
            item
            xs={4}
            container
            alignItems="center"
            justifyContent="flex-end"
          >
            <Button
              variant="contained"
              size="large"
              disabled={!gameStarted}
              onClick={() => resetGame()}
              style={{
                fontSize: "16px",
                width: "200px",
                height: "50px",
                color: "white",
                backgroundColor: gameStarted ? "#9c27b0" : "#AC8FB1",
                fontWeight: "bold",
              }}
            >
              RESET
            </Button>
          </Grid>

          <Grid
            item
            xs={4}
            container
            alignItems="center"
            justifyContent="center"
          >
            <Button
              variant="contained"
              size="large"
              disabled={gameStarted}
              onClick={() => initializeGame()}
              style={{
                fontSize: "16px",
                width: "200px",
                height: "50px",
                color: "white",
                backgroundColor: !gameStarted ? "#9c27b0" : "#AC8FB1",
                fontWeight: "bold",
              }}
            >
              PLAY
            </Button>
          </Grid>

          <Grid
            item
            xs={4}
            container
            alignItems="center"
            justifyContent="flex-start"
          >
            <CustomizedSwitch
              onChangeAction={handleNumericalQuestionsModeChange}
              isDisabled={gameStarted}
            />
          </Grid>
        </Grid>
      </ThemeProvider>
    </div>
  );
}
