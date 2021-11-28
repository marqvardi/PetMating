import { createSelector } from "reselect";

const getAnimals = (state) => state.animal;

export const getAllAnimals = createSelector(
  [getAnimals],
  (animal) => animal.allAnimals
);
