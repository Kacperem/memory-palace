import { createTheme, ThemeProvider } from "@mui/material/styles";

import React, { useEffect, useState } from "react";
import { Slider, Typography } from "@mui/material";
const defaultTheme = createTheme();
export default function Challenge() {
  const [sliderValues, setSliderValue] = React.useState<number[]>([0, 10]);
  const [numOfNumbers, setNumOfNumbers] = useState(0);
  const [generatedNumbers, setGeneratedNumbers] = useState<string[]>([]);
  const [userInputsNumbers, setUserInputsNumbers] = useState<string[]>([]);
  const [showResultsNumbers, setShowResultsNumbers] = useState<string[]>([]);
  const [time, setTime] = useState(0);
  enum ChallengeState {
    NOT_STARTED,
    STARTED,
    USER_PROVIDES_VALUES,
    SHOW_RESULTS,
  }
  const [challengeState, setChallengeState] = useState(
    ChallengeState.NOT_STARTED
  );
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
  const generateNumbers = () => {
    const minRange = sliderValues[0];
    const maxRange = sliderValues[1];
    const count = numOfNumbers;
    const numbers = [];

    for (let i = 0; i < count; i++) {
      const randomNumber = Math.floor(
        Math.random() * (maxRange - minRange + 1) + minRange
      );
      //  format "00" or "01" for numbers < 10
      const formattedNumber =
        randomNumber < 10 ? `0${randomNumber}` : `${randomNumber}`;
      numbers.push(formattedNumber);
    }

    setGeneratedNumbers(numbers);
  };

  useEffect(() => {
    const timer = setInterval(() => {
      if (time > 0) {
        setTime(time - 1);
      } else {
        challengeStatesHandler();
      }

      clearInterval(timer);
    }, 1000);
    return () => {
      clearInterval(timer);
    };
  }, [time]);

  function challengeStart() {
    generateNumbers();
    setTimeAccordingtoNumOfNumbers();
    setChallengeState(ChallengeState.STARTED);

    const initialUserInputs = Array.from({ length: numOfNumbers }, () => "");
    setUserInputsNumbers(initialUserInputs);
  }
  function challengeUserProvidesValues() {
    setChallengeState(ChallengeState.USER_PROVIDES_VALUES);
    setTimeAccordingtoNumOfNumbers();
  }
  function challengeShowResults() {
    setTime(0);
    setShowResultsNumbers(userInputsNumbers);
    setChallengeState(ChallengeState.SHOW_RESULTS);
  }
  function challengeStatesHandler() {
    if (challengeState === ChallengeState.STARTED) {
      challengeUserProvidesValues();
    } else if (challengeState === ChallengeState.USER_PROVIDES_VALUES) {
      challengeShowResults();
    }
  }
  function setTimeAccordingtoNumOfNumbers() {
    if (numOfNumbers <= 10) {
      setTime(30);
    } else if (numOfNumbers <= 20) {
      setTime(60);
    } else if (numOfNumbers <= 30) {
      setTime(90);
    } else if (numOfNumbers <= 40) {
      setTime(120);
    } else if (numOfNumbers <= 50) {
      setTime(150);
    } else if (numOfNumbers <= 60) {
      setTime(180);
    } else if (numOfNumbers <= 70) {
      setTime(210);
    } else if (numOfNumbers <= 80) {
      setTime(240);
    } else if (numOfNumbers <= 90) {
      setTime(270);
    } else if (numOfNumbers <= 100) {
      setTime(300);
    }
  }
  function onClickNextAction() {
    challengeStatesHandler();
  }
  const handleUserInput = (
    event: React.ChangeEvent<HTMLInputElement>,
    index: number
  ) => {
    const updatedInputs = [...userInputsNumbers];
    updatedInputs[index] = event.target.value;
    setUserInputsNumbers(updatedInputs);
  };
  const handleInputChange = (event) => {
    const inputNumber = event.target.value;
    setNumOfNumbers(inputNumber);
  };
  function onShowResultButtonClickAction(index: number) {
    const updatedShowResultsNumbers = [...showResultsNumbers];
    const currentValue = updatedShowResultsNumbers[index];

    if (userInputsNumbers[index] === generatedNumbers[index]) {
      updatedShowResultsNumbers[index] = currentValue;
    } else if (
      userInputsNumbers[index] !== generatedNumbers[index] &&
      currentValue === userInputsNumbers[index]
    ) {
      updatedShowResultsNumbers[index] = generatedNumbers[index];
    } else if (
      userInputsNumbers[index] !== generatedNumbers[index] &&
      currentValue === generatedNumbers[index]
    ) {
      updatedShowResultsNumbers[index] = userInputsNumbers[index];
    }

    setShowResultsNumbers(updatedShowResultsNumbers);
  }

  return (
    <ThemeProvider theme={defaultTheme}>
      <div style={{ marginTop: "20px" }}>
        <Typography
          variant="h4"
          align="center"
          fontWeight={"bold"}
          fontFamily={"monospace"}
        >
          Challenge yourself!
        </Typography>
      </div>

      <div
        style={{
          marginTop: "10px",
          marginBottom: "10px",
          marginLeft: "10%",
          marginRight: "10%",
        }}
      >
        <div style={{ display: "flex", alignItems: "center" }}>
          <Typography variant="h6" fontWeight={"bold"} fontFamily={"monospace"}>
            Select a range of numbers:
          </Typography>
          <input
            type="text"
            className="border rounded-lg bg-purple-300 text-black placeholder-purple-600 border-black border-opacity-100 border-y-2 border-x-2"
            style={{
              margin: "10px",
              width: "50px",
              height: "40px",
              fontSize: "20px",
              fontFamily: "monospace",
              fontWeight: "bold",
              color: "black",
              textAlign: "center",
              marginLeft: "auto",
            }}
            disabled={true}
            value={time + "s"}
          />
        </div>
        <Slider
          getAriaLabel={() => "Minimum distance shift"}
          value={sliderValues}
          onChange={handleSliderChange}
          valueLabelDisplay="auto"
          disableSwap
          valueLabelFormat={formatSliderValue}
          min={0}
          max={99}
          style={{ color: "purple", width: "100%" }}
        />
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-start",
          }}
        >
          <Typography
            variant="h6"
            fontWeight={"bold"}
            fontFamily={"monospace"}
            style={{ width: "auto" }}
          >
            Choose how many numbers:
          </Typography>
          <div
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "flex-start",
            }}
          >
            <input
              type="text"
              className="border rounded-lg bg-purple-300 text-black placeholder-purple-600 border-black border-opacity-100 border-y-2 border-x-2 ;"
              style={{
                margin: "10px",
                width: "40px",
                height: "40px",
                fontSize: "20px",
                fontFamily: "monospace",
                fontWeight: "bold",
                color: "black",
                textAlign: "center",
              }}
              onChange={handleInputChange}
              value={numOfNumbers}
              onInput={(e) => {
                e.preventDefault();
                const inputElement = e.target as HTMLInputElement;
                let value = inputElement.value;

                value = value.replace(/[^0-9]/g, "");

                if (value.length > 2) {
                  value = value.slice(0, 2);
                }
                inputElement.value = value;
              }}
            />
          </div>
          <button
            className="border rounded-lg bg-purple-900 text-black placeholder-purple-900 border-black border-opacity-100 border-y-2 border-x-2 transform active:scale-x-75 active:scale-y-75 transition-transform mx-5 flex;"
            style={{
              margin: "10px",
              width: "100px",
              height: "40px",
              fontSize: "20px",
              fontFamily: "monospace",
              fontWeight: "bold",
              color: "white",
            }}
            onClick={challengeStart}
          >
            Generate
          </button>
          <button
            className="border rounded-lg bg-purple-900 text-black placeholder-purple-900 border-black border-opacity-100 border-y-2 border-x-2 transform active:scale-x-75 active:scale-y-75 transition-transform mx-5 flex;"
            style={{
              margin: "10px",
              width: "100px",
              height: "40px",
              fontSize: "20px",
              fontFamily: "monospace",
              fontWeight: "bold",
              color: "white",
            }}
            onClick={() => onClickNextAction()}
          >
            Next
          </button>
        </div>

        <div>
          {challengeState === ChallengeState.STARTED &&
            generatedNumbers.length > 0 && (
              <div>
                {generatedNumbers.map((number, index) => (
                  <input
                    type="text"
                    className={`border rounded-lg  text-black placeholder-purple-900 border-black border-opacity-100 border-y-2 border-x-2}`}
                    style={{
                      margin: "10px",
                      width: "50px",
                      height: "40px",
                      fontSize: "20px",
                      fontFamily: "monospace",
                      fontWeight: "bold",
                      color: "black",
                      textAlign: "center",
                    }}
                    pattern="[0-9]{0,2}"
                    value={number}
                    disabled={true}
                  />
                ))}
              </div>
            )}

          {challengeState === ChallengeState.USER_PROVIDES_VALUES &&
            generatedNumbers.length > 0 && (
              <div>
                {generatedNumbers.map((number, index) => (
                  <input
                    type="text"
                    className={`border rounded-lg  text-black placeholder-purple-900 border-black border-opacity-100 border-y-2 border-x-2}`}
                    onChange={(event) => handleUserInput(event, index)}
                    style={{
                      margin: "10px",
                      width: "50px",
                      height: "40px",
                      fontSize: "20px",
                      fontFamily: "monospace",
                      fontWeight: "bold",
                      color: "black",
                      textAlign: "center",
                    }}
                    onInput={(e) => {
                      e.preventDefault();
                      const inputElement = e.target as HTMLInputElement;
                      let value = inputElement.value;

                      value = value.replace(/[^0-9]/g, "");

                      if (value.length > 2) {
                        value = value.slice(0, 2);
                      }

                      inputElement.value = value;
                    }}
                  />
                ))}
              </div>
            )}
          {challengeState === ChallengeState.SHOW_RESULTS &&
            showResultsNumbers.length > 0 && (
              <div
                style={{
                  display: "flex",
                  flexWrap: "wrap",
                  alignContent: "center",
                  width: "100%",
                }}
              >
                {showResultsNumbers.map((number, index) => (
                  <button
                    className={`border rounded-lg  text-black placeholder-purple-900 border-black border-opacity-100 border-y-2 border-x-2 ${
                      number === generatedNumbers[index]
                        ? "bg-green-300"
                        : "bg-red-300"
                    }`}
                    style={{
                      margin: "10px",
                      width: "50px",
                      height: "40px",
                      fontSize: "20px",
                      fontFamily: "monospace",
                      fontWeight: "bold",
                      color: "black",
                      textAlign: "center",
                    }}
                    disabled={
                      userInputsNumbers[index] === generatedNumbers[index]
                    }
                    onClick={() => onShowResultButtonClickAction(index)}
                  >
                    {showResultsNumbers[index]}
                  </button>
                ))}
              </div>
            )}
        </div>
      </div>
    </ThemeProvider>
  );
}
