import axios from "axios";

//const API_URL_LOCAL = import.meta.env.VITE_API_URL_LOCAL;
const API_URL = import.meta.env.VITE_API_URL;
const API_KEY = import.meta.env.VITE_API_KEY;

export const register = (
  email: string,
  password: string,
  confirmPassword: string
) => {
  return axios.post(
    API_URL + "/account/register",
    {
      email,
      password,
      confirmPassword,
    },
    {
      headers: {
        "API-KEY": API_KEY,
      },
    }
  );
};

export const login = (email: string, password: string) => {
  return axios
    .post(
      API_URL + "/account/login",
      {
        email,
        password,
      },
      {
        headers: {
          "API-KEY": API_KEY,
        },
      }
    )
    .then((response) => {
      localStorage.setItem("user", JSON.stringify(response.data));
      return response.data;
    });
};

export const logout = () => {
  localStorage.removeItem("user");
};

export const getCurrentUser = () => {
  const userStr = localStorage.getItem("user");
  if (userStr) return JSON.parse(userStr);

  return null;
};
