import { AnimalActionTypes } from "./AnimalActionsType";

const INITIAL_STATE = {
  allAnimals: null,
};

const animalReducer = (state = INITIAL_STATE, { type, payload }) => {
  switch (type) {
    case AnimalActionTypes.ALL_ANIMALS:
      return {
        ...state,
        allAnimals: payload,
      };

    case AnimalActionTypes.CLEAR_ALL_ANIMALS:
      return {
        ...state,
        allAnimals: null,
      };

    default:
      return state;
  }
};

export default animalReducer;
