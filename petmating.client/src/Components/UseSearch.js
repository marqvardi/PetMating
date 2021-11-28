import { useState, useEffect } from "react";
import axios from "axios";
import { BaseURL, getToken } from "../API";

const UseSearch = (query, pageNumber) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [books, setBooks] = useState([]);
  const [hasMore, setHasMore] = useState(false);
  const token = getToken;

  useEffect(() => {
    setBooks([]);
  }, [query]);

  useEffect(() => {
    setLoading(true);
    setError(false);
    let cancel;
    axios
      .get(BaseURL + "animal", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        params: {
          search: query,
          pageIndex: pageNumber,
        },
        cancelToken: new axios.CancelToken((c) => (cancel = c)),
      })
      .then((res) => {
        setBooks(res.data.data);
        console.log(res.data);
        setHasMore(res.data.data.length > 0);

        setLoading(false);
      })
      .catch((e) => {
        if (axios.isCancel(e)) return;
        setError(true);
      });
    return () => cancel();
  }, [query, pageNumber]);

  console.log(books);
  console.log(hasMore);
  return { loading, error, books, hasMore };
};

export default UseSearch;
