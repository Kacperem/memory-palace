import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Footer from "./components/Footer";
import CssBaseline from "@mui/material/CssBaseline";
import Games from "./pages/Games";
import TwoDigit from "./pages/TwoDigit";
import QuizGame from "./pages/games/QuizGame";

function App() {
  return (
    <>
      <BrowserRouter>
        <CssBaseline />
        <Navbar />
        <Routes>
          <Route path="" element={<Home />} />
          <Route path="twodigit" element={<TwoDigit />} />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="games" element={<Games />} />
          <Route path="quiz-game" element={<QuizGame />} />
        </Routes>
        <Footer />
      </BrowserRouter>
    </>
  );
}

export default App;
