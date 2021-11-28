import axios from "axios";
import { BaseURL, getToken } from "../index";
import { userActionTypes } from "../../Redux/UserReducer/userActionsTypes";
import { toast } from "react-toastify";
import history from "../../util/history";

export const GetAllUsers = () => async (dispatch) => {
  const token = getToken;

  axios
    .get(BaseURL + "animal", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((userData) => {
      dispatch({
        type: userActionTypes.CURRENT_USER,
        payload: userData.data,
      });
    });
};

export const registerUser = (values) => async (dispatch) => {
  const token = getToken;

  axios
    .post(BaseURL + "auth/register", values, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((newUser) => {
      dispatch(Login(newUser.data));
      toast.success("User created");
      history.push("home");
    })
    .catch((error) => {
      toast.error(error.response.data);
      console.log(error.response.data);
    });
};

export const Login = (values) => async (dispatch) => {
  axios
    .post(BaseURL + "auth/login", values)
    .then((result) => {
      localStorage.setItem("tokenPetMating", result.data.token);
      dispatch({
        type: userActionTypes.CURRENT_USER,
        payload: result.data.user,
      });
    })
    .catch((error) => {
      console.log("the error has occured: " + error);
      toast.error("" + error);
    });
};

export const SignOut = () => async (dispatch) => {
  dispatch({ type: userActionTypes.SIGN_OUT_USER, payload: null });
};
