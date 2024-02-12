import React, { useEffect } from "react";
import axios, { AxiosRequestConfig } from "axios";
import authHeader from "../services/authHeader";


const API_URL = import.meta.env.VITE_API_URL;
const API_KEY = import.meta.env.VITE_API_KEY;

const TwoDigit: React.FC = () => {
  const headers: AxiosRequestConfig["headers"] = {
    "API-KEY": API_KEY,
    ...authHeader(),
  };

  useEffect(() => {
    axios.get(API_URL + "/TwoDigitSystem/5", { headers }).then((res) => {
      console.log(res.data);
    });
  }, []);

  return <div>TwoDigit</div>;
};

export default TwoDigit;

