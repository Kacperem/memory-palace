import { Box, PaletteMode, ThemeProvider, createTheme } from "@mui/material";
import CssBaseline from "@mui/material/CssBaseline";
import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import "./App.css";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Footer from "./components/Footer";
import TwoDigit from "./pages/TwoDigit";
import QuizGame from "./pages/games/QuizGame";
import Training from "./pages/Training";
import Challenge from "./pages/Challenge";
import getLPTheme from "./components/getLPTheme";
import Navbar from "./components/Navbar";

function App() {
  const [mode, setMode] = React.useState<PaletteMode>("dark");
  const LPtheme = createTheme(getLPTheme(mode));

  const toggleColorMode = () => {
    setMode((prev) => (prev === "dark" ? "light" : "dark"));
  };

  return (
    <>
      <ThemeProvider theme={LPtheme}>
        <CssBaseline />
        <BrowserRouter>
          <Navbar mode={mode} toggleColorMode={toggleColorMode} />
          <Box sx={{ bgcolor: "background.default" }}>
            <Routes>
              <Route path="" element={<Home />} />
              <Route path="twodigit" element={<TwoDigit />} />
              <Route path="login" element={<Login />} />
              <Route path="register" element={<Register />} />
              <Route path="training" element={<Training />} />
              <Route path="quiz-game" element={<QuizGame />} />
              <Route path="challenge" element={<Challenge />} />
            </Routes>
            <Footer />
          </Box>
        </BrowserRouter>
      </ThemeProvider>
    </>
  );
}

export default App;
