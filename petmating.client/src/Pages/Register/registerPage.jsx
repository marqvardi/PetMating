import React from "react";
import { Form, Field } from "react-final-form";
import { useDispatch } from "react-redux";
import { registerUser } from "../../API/User/UserApi";
import classes from "./register.module.scss";

const RegisterPage = () => {
  const dispatch = useDispatch();

  const onSubmit = async (values) => {
    const valueToSend = {
      userName: values.userName,
      firstName: values.firstName,
      lastName: values.lastName,
      email: values.email,
      password: values.password,
      address: {
        city: values.city,
        streetName: values.streetName,
        zipCode: values.zipCode,
      },
    };

    // console.log(valueToSend);
    dispatch(registerUser(valueToSend));
  };

  const required = (value) => (value ? undefined : "Required");

  let formData = {};

  const renderInput = ({
    input,
    label,
    placeholder,
    typeHere,
    meta: { error, touched },
  }) => (
    <div className="field">
      <label>{label}</label>
      <input
        {...input}
        placeholder={placeholder}
        type={typeHere}
        autoComplete="off"
      />
      <div style={{ color: "red" }}>{touched ? error : ""}</div>
    </div>
  );

  return (
    <div className={classes.main}>
      <Form
        onSubmit={onSubmit}
        initialValues={formData}
        render={({ handleSubmit, form, submitting, pristine, values }) => (
          <form onSubmit={handleSubmit}>
            <h3>User details</h3>
            <Field
              name="userName"
              component={renderInput}
              label="Username"
              typeHere="text"
              validate={required}
              placeholder="Username"
            />

            <Field
              name="firstName"
              component={renderInput}
              label="First Name"
              typeHere="text"
              validate={required}
              placeholder="First name"
            />

            <Field
              name="lastName"
              component={renderInput}
              label="Last Name"
              typeHere="text"
              validate={required}
              placeholder="Last name"
            />

            <Field
              name="email"
              component={renderInput}
              label="Email"
              typeHere="email"
              validate={required}
              placeholder="Email"
            />
            <h3> Address</h3>

            <Field
              name="streetName"
              component={renderInput}
              label="Address"
              typeHere="text"
              validate={required}
              placeholder="Address"
            />

            <Field
              name="zipCode"
              component={renderInput}
              label="Zip code"
              typeHere="text"
              validate={required}
              placeholder="Zip Code"
            />

            <Field
              name="city"
              component={renderInput}
              label="City"
              typeHere="text"
              validate={required}
              placeholder="City"
            />

            <h3>Password</h3>
            <Field
              name="password"
              component={renderInput}
              label="Password"
              typeHere="password"
              validate={required}
              placeholder="Pass here please"
            />

            {/* <Field
              name="age"
              validate={composeValidators(required, mustBeNumber, minValue(18))}
            >
              {({ input, meta }) => (
                <div>
                  <label>Age</label>
                  <input {...input} type="text" placeholder="Age" />
                  {meta.error && meta.touched && <span>{meta.error}</span>}
                </div>
              )}
            </Field> */}
            <div className={classes.buttons}>
              <button type="submit" disabled={submitting}>
                Submit
              </button>
              <button
                type="button"
                onClick={form.reset}
                disabled={submitting || pristine}
              >
                Reset
              </button>
            </div>
          </form>
        )}
      />
    </div>
  );
};

export default RegisterPage;
