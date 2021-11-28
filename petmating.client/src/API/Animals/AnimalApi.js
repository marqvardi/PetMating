import axios from "axios";
import { AnimalActionTypes } from "../../Redux/AnimalReducer/AnimalActionsType";
import { BaseURL, getToken } from "../index";

export const GetAllAnimals = (query, pageNumber) => async (dispatch) => {
  const token = getToken;
  let cancel;
  await axios
    .get(BaseURL + "animal", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {},
      cancelToken: new axios.CancelToken((c) => (cancel = c)),
    })
    .then((animalData) => {
      dispatch({
        type: AnimalActionTypes.ALL_ANIMALS,
        payload: animalData.data.data,
      });
    })
    .catch((e) => {
      if (axios.isCancel(e)) return;
    });
  return () => cancel();
};

export const ClearAllAnimals = () => async (dispatch) => {
  dispatch({ type: AnimalActionTypes.CLEAR_ALL_ANIMALS });
};
