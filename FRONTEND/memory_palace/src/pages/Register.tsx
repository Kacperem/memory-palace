import React, { useState } from "react";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

import IUser from "../types/user.type";
import { register } from "../services/authService";

const Register: React.FC = () => {
  const [successful, setSuccessful] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");

  const initialValues: IUser = {
    email: "",
    password: "",
    confirmPassword: "",
  };

  const validationSchema = Yup.object().shape({
    email: Yup.string()
      .email("This is not a valid email.")
      .required("This field is required!"),
    password: Yup.string()
      .test(
        "len",
        "The password must be between 6 and 40 characters.",
        (val: any) =>
          val && val.toString().length >= 6 && val.toString().length <= 40
      )
      .required("This field is required!"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password")], "Password must match!")
      .required("This field is required!"),
  });

  const handleRegister = (formValue: IUser) => {
    const { email, password, confirmPassword } = formValue;

    register(email, password, confirmPassword).then(
      (response) => {
        setMessage(response.data.message);
        setSuccessful(true);
      },
      (error) => {
        const resMessage =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();

        setMessage(resMessage);
        setSuccessful(false);
      }
    );
  };

  return (
    <div className="flex flex-col items-center p-10">
      <h3 className="mb-8 font-extrabold text-2xl text-center">
        Register to unlock your world of memory digits
      </h3>
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleRegister}
      >
        <Form>
          {!successful && (
            <div className="flex flex-col items-center justify-center bg-gray-300 m-15 p-12 rounded-md w-[21rem]">
              <div className="mt-1">
                <label className="block font-semibold" htmlFor="email">
                  {" "}
                  Email{" "}
                </label>
                <Field
                  id="email"
                  className="w-full shadow-inner bg-gray-100 rounded-lg placeholder-black p-3 border-none block mt-1"
                  name="email"
                  type="email"
                  placeholder="example@email.com"
                  autoComplete="email"
                />
                <ErrorMessage
                  name="email"
                  component="div"
                  className="mt-1 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
                />
              </div>
              <div className="mt-4">
                <label className="block font-semibold" htmlFor="password">
                  {" "}
                  Password{" "}
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
                />
              </div>

              <div className="mt-4">
                <label
                  className="block font-semibold"
                  htmlFor="confirmPassword"
                >
                  {" "}
                  Confirm Password{" "}
                </label>
                <Field
                  id="confirmPassword"
                  name="confirmPassword"
                  type="password"
                  className="w-full shadow-inner bg-gray-100 rounded-lg placeholder-black p-3 border-none block mt-1"
                  autoComplete="current-password"
                />
                <ErrorMessage
                  name="confirmPassword"
                  component="div"
                  className="mt-1 bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
                />
              </div>

              <div className="mt-4">
                <button
                  type="submit"
                  className="px-8 py-1 border border-transparent text-base font-medium rounded-md text-white bg-fuchsia-700 hover:bg-fuchsia-800"
                >
                  <span>Sign Up</span>
                </button>
              </div>
              {message && (
                <div className="mt-4">
                  <div
                    className={
                      successful
                        ? "bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded relative"
                        : "bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
                    }
                    role="alert"
                  >
                    {message}
                  </div>
                </div>
              )}
            </div>
          )}
        </Form>
      </Formik>
    </div>
  );
};

export default Register;
