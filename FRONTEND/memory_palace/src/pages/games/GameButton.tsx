import { Button } from "@mui/material";
import { useAnimate, animate } from "framer-motion";

const randomNumberBetween = (min: number, max: number) => {
  return Math.floor(Math.random() * (max - min + 1) + min);
};

type AnimationSequence = Parameters<typeof animate>[0];
function GameButton({
  disabled,
  backgroundColor,
  svgPath,
  svgClassName,
  buttonText,
  onClick,
  borderWidth,
  borderRadius,
}) {
  const [scope, animate] = useAnimate();

  const onButtonClick = async () => {
    const animatedElements = Array.from({ length: 30 });
    const animatedElementsAnimation: AnimationSequence = animatedElements.map(
      (_, index) => [
        `.animatedElement-${index}`,
        {
          x: randomNumberBetween(-150, 150),
          y: randomNumberBetween(-150, 150),
          scale: randomNumberBetween(2, 2),
          opacity: 1,
        },
        {
          duration: 0.3,
          at: "<",
        },
      ]
    );

    const animatedElementsFadeOut: AnimationSequence = animatedElements.map(
      (_, index) => [
        `.animatedElement-${index}`,
        {
          opacity: 10,
          scale: 0,
        },
        {
          duration: 0.1,
          at: "<",
        },
      ]
    );

    const animatedElementsReset: AnimationSequence = animatedElements.map(
      (_, index) => [
        `.animatedElement-${index}`,
        {
          x: 0,
          y: 0,
        },
        {
          duration: 0.000001,
        },
      ]
    );

    animate([
      ...animatedElementsReset,
      ["button", { scale: 0.8 }, { duration: 0.1, at: "<" }],
      ["button", { scale: 1 }, { duration: 0.1 }],
      ...animatedElementsAnimation,
      [".letter", { y: 0 }, { duration: 0.000001 }],
      ...animatedElementsFadeOut,
    ]);
    onClick();
  };

  return (
    <div
      ref={scope}
      style={{
        height: "100%",
        display: "flex",
        justifyContent: "inherit",
        width: "100%",
      }}
    >
      <Button
        variant="contained"
        onClick={onButtonClick}
        disabled={disabled}
        style={{
          backgroundColor: backgroundColor,
          fontSize: "14px",
          width: "242px",
          height: "200px",
          color: "black",
          fontWeight: "bold",
          border: "2px solid grey",
          fontFamily: "Patrick Hand SC, cursive",
          padding: "1rem 1rem",
          borderWidth: borderWidth,
          borderRadius: borderRadius,
        }}
      >
        {buttonText}
        <span aria-hidden>
          {Array.from({ length: 30 }).map((_, index) => (
            <svg
              className={`absolute left-1/2 top-1/2 opacity-0 animatedElement-${index}`}
              key={index}
              viewBox="0 0 24 24"
              width="15"
              height="15"
              style={{
                zIndex: 100,
              }}
            >
              <path
                className={svgClassName}
                stroke="black"
                strokeLinecap="round"
                strokeLinejoin="round"
                d={svgPath}
              />
            </svg>
          ))}
        </span>
      </Button>
    </div>
  );
}

export default GameButton;
