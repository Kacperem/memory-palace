import authHeader from "./authHeader";
import axios, { AxiosRequestConfig } from "axios";

const API_URL = import.meta.env.VITE_API_URL;
const API_KEY = import.meta.env.VITE_API_KEY;

const getAll = (id: string) => {
  const headers: AxiosRequestConfig["headers"] = {
    "API-KEY": API_KEY,
    ...authHeader(),
  };
  return axios.get(API_URL + "/TwoDigitSystem/" + id, { headers });
};

const twoDigitSystemService = {
  getAll,
};

export default twoDigitSystemService;