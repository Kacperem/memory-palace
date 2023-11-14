import { Button } from "@mui/material";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import { useNavigate } from "react-router-dom";

export default function TitlebarBelowImageList() {
  const listItemStyle = {
    marginTop: "3vw",
    marginBottom: "3vw",
    marginRight: "3vw",
    marginLeft: "3vw",
    // width: "30vw",
    //height: "70vh",
    //border: "1px solid black",
    boxShadow: "0 0 10px 1px rgba(0, 0, 0, 0.2)",
  };
  const navigate = useNavigate();
  return (
    <ImageList>
      {itemData.map((item) => (
        <ImageListItem key={item.img} style={listItemStyle}>
          <Button
            onClick={() => {
              navigate(`${item.endpoint}`); // Navigate to the specified route
            }}
          >
            <img
              //srcSet={`${item.img}?w=248&fit=crop&auto=format&dpr=2 2x`}
              // src={`${item.img}?w=248&fit=crop&auto=format`}
              srcSet={`${item.img}`}
              src={`${item.img}`}
              alt={item.title}
              loading="lazy"
            />
            <ImageListItemBar
              title={item.title}
              //subtitle={<span>by: {item.author}</span>}
              position="bottom"
            />
          </Button>
        </ImageListItem>
      ))}
    </ImageList>
  );
}

const itemData = [
  {
    img: "src/assets/QuizGame.png",
    title: "Quiz game",
    endpoint: "/quiz-game",
  },
];
