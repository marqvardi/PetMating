import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { GetAllAnimals } from "../API/Animals/AnimalApi";
import axios from "axios";
import { BaseURL, getToken } from "../API";
import { AnimalActionTypes } from "../Redux/AnimalReducer/AnimalActionsType";

const useAnimalSearch = (query, pageNumber) => {
  const token = getToken;
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [animal, setAnimal] = useState(null);
  const [hasMore, setHasMore] = useState(false);

  useEffect(() => {
    setAnimal(null);
  }, [query]);

  useEffect(() => {
    let cancel;
    setLoading(true);
    setError(false);
    axios
      .get(BaseURL + "animal", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        params: {
          search: query ? query : "",
          pageIndex: pageNumber,
        },
        cancelToken: new axios.CancelToken((c) => (cancel = c)),
      })
      .then((animalData) => {
        setAnimal(animalData.data);
        setHasMore(animalData.data.length > 0);
        setLoading(false);
      })
      .catch((e) => {
        if (axios.isCancel(e)) return;
        setError(true);
      });

    return () => cancel();
  }, [query, pageNumber, token]);
  return { loading, error, animal, hasMore };
};

export default useAnimalSearch;
