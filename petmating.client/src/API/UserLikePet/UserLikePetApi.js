import axios from "axios";
import { BaseURL, getToken } from "..";
import { userLikePetActionTypes } from "../../Redux/UserLikePetReducer/userLikePetActionTypes";

export const PostUserLikePet = (data) => async (dispatch) => {
  const token = getToken;

  axios
    .post(BaseURL + "userLikePet", data, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((userData) => {
      dispatch({
        type: userLikePetActionTypes.ALL_MY_FAV_PETS,
        payload: userData.data,
      });
    });
};
