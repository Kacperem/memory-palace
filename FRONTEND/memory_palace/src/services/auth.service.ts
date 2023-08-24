import axios from "axios";

const API_URL = "https://localhost:7022/api/account/";

export const register = (email: string, password: string, confirmPassword: string, roleId:number) => {
  return axios.post(API_URL + "register", {
    email,
    password,
    confirmPassword,
    roleId
  });
};

export const login = (email: string, password: string) => {
  return axios
    .post(API_URL + "login", {
      email,
      password,
    })
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