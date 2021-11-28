import { combineReducers } from "redux";
import animalReducer from "./AnimalReducer/animalReducer";
import userLikePetReducer from "./UserLikePetReducer/userLikePetReducer";
import userReducer from "./UserReducer/userReducer";

const reducers = combineReducers({
  user: userReducer,
  animal: animalReducer,
  myPets: userLikePetReducer,
});

export default reducers;
