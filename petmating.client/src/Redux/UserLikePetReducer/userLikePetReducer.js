import { userLikePetActionTypes } from "./userLikePetActionTypes";

const INITIAL_STATE = {
  myFavoritePets: null,
};

const userLikePetReducer = (state = INITIAL_STATE, { type, payload }) => {
  switch (type) {
    case userLikePetActionTypes.ALL_MY_FAV_PETS:
      return {
        ...state,
        myFavoritePets: payload,
      };

    default:
      return state;
  }
};

export default userLikePetReducer;
