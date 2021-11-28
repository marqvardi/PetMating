import React from "react";
import { useDispatch } from "react-redux";
import { Grid, Header, Image, Message } from "semantic-ui-react";
import { Form, Field } from "react-final-form";
import classes from "./LoginForm.module.scss";
import { Login } from "../../API/User/UserApi";
import history from "../../util/history";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

const LoginForm = () => {
  const dispatch = useDispatch();

  const onSubmit = async (values) => {
    try {
      dispatch(Login(values));
      history.push("/home");
    } catch (error) {
      toast.error(error);
      console.log("weew");
    }
  };

  const required = (value) => (value ? undefined : "Required");

  let formData = {};

  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h2" color="teal" textAlign="center">
          <Image src={process.env.PUBLIC_URL + "/pets.png"} /> Log-in to your
          account
        </Header>
        <Form
          onSubmit={onSubmit}
          initialValues={formData}
          render={({ handleSubmit, form, submitting, pristine, values }) => (
            <form onSubmit={handleSubmit}>
              <Field name="email" validate={required} className={classes.input}>
                {({ input, meta }) => (
                  <div>
                    <label>Email</label>
                    <input {...input} type="text" placeholder="Email" />
                    {meta.error && meta.touched && <span>{meta.error}</span>}
                  </div>
                )}
              </Field>

              <Field name="password" validate={required}>
                {({ input, meta }) => (
                  <div>
                    <label>Password</label>
                    <input {...input} type="password" placeholder="Password" />
                    {meta.error && meta.touched && <span>{meta.error}</span>}
                  </div>
                )}
              </Field>

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

        <Message>
          <Link to="register">New to us? Sign Up</Link>
        </Message>
      </Grid.Column>
    </Grid>
  );
};

export default LoginForm;
