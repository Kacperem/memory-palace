import React, { useState } from "react";
import { NavigateFunction, useNavigate } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

import { login } from "../services/authService";

type Props = {};

const Login: React.FC<Props> = () => {
  let navigate: NavigateFunction = useNavigate();

  const [loading, setLoading] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");

  const initialValues: {
    email: string;
    password: string;
  } = {
    email: "",
    password: "",
  };

  const validationSchema = Yup.object().shape({
    email: Yup.string().required("This field is required!"),
    password: Yup.string().required("This field is required!"),
  });

  const handleLogin = (formValue: { email: string; password: string }) => {
    const { email, password } = formValue;

    setMessage("");
    setLoading(true);

    login(email, password).then(
      () => {
        navigate("/");
        window.location.reload();
      },
      (error) => {
        const resMessage =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();

        setLoading(false);
        setMessage(resMessage);
      }
    );
  };

  return (
    <div className="flex flex-col items-center justify-center p-10">
      <h3 className="mb-8 font-extrabold text-2xl text-center">
        Log in to unlock your world of memory digits
      </h3>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleLogin}
      >
        <Form className="flex flex-col items-center justify-center  bg-gray-300 m-15 p-12 rounded-md w-[21rem]">
          <div className="mt-1">
            <label className="block font-semibold" htmlFor="email">
              Email
            </label>
            <Field
              id="email"
              className="w-full shadow-inner bg-gray-100 rounded-lg placeholder-black p-3 border-none block mt-1"
              name="email"
              type="text"
              placeholder="example@email.com"
              autoComplete="email"
            />
            <ErrorMessage
              name="email"
              type="email"
              component="div"
              className="mt-1 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
              role="alert"
            />
          </div>

          <div className="mt-4">
            <label className="block font-semibold" htmlFor="password">
              Password
            </label>
            <Field
              id="password"
              className="w-full shadow-inner bg-gray-100 rounded-lg placeholder-black p-3 border-none block mt-1"
              name="password"
              type="password"
              autoComplete="current-password"
            />
            <ErrorMessage
              name="password"
              component="div"
              className="mt-1 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
              role="alert"
            />
          </div>

          <div className="mt-4">
            <button
              type="submit"
              className="px-8 py-1 border border-transparent text-base font-medium rounded-md text-white bg-fuchsia-700 hover:bg-fuchsia-800"
              disabled={loading}
            >
              {loading && <span className="sr-only"></span>}
              <span>Login</span>
            </button>
          </div>

          {message && (
            <div className="mt-4">
              <div
                className="mt-1 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
                role="alert"
              >
                {message}
              </div>
            </div>
          )}
        </Form>
      </Formik>
    </div>
  );
};

export default Login;
