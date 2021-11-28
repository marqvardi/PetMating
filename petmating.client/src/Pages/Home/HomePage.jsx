import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { getAllAnimals } from "../../Redux/AnimalReducer/animal.selector";
import { useSelector } from "react-redux";
import CardComponent from "../../Components/cards/card.component";
import { AnimalActionTypes } from "../../Redux/AnimalReducer/AnimalActionsType";
import { GetAllAnimals } from "../../API/Animals/AnimalApi";
import { Button, Grid } from "semantic-ui-react";
import classes from "./homepage.module.scss";
import { getCurrentUser } from "../../Redux/UserReducer/user.selector";

const HomePage = () => {
  const dispatch = useDispatch();
  const data = useSelector(getAllAnimals);
  const [loading, setLoading] = useState(true);
  const currentUser = useSelector(getCurrentUser);

  useEffect(() => {
    dispatch(GetAllAnimals());
    setLoading(false);
    return () => {
      dispatch({ type: AnimalActionTypes.CLEAR_ALL_ANIMALS });
    };
  }, [dispatch]);

  const handleClickLike = (id) => {
    console.log("Like", id);
    console.log("User", currentUser.id);
  };

  const handleClickPass = () => {
    console.log("pass");
  };

  return (
    <div className={classes.main}>
      <>
        <Grid columns={3} stackable>
          {data &&
            data.map((animal) => (
              <Grid.Column key={animal.id} className={classes.column1}>
                <CardComponent loading={loading} animal={animal} />
                {!currentUser && (
                  <p
                    style={{
                      textAlign: "center",
                      marginTop: "15px",
                      color: "red",
                    }}
                  >
                    Please login to select
                  </p>
                )}
                <div className={classes.buttons}>
                  <Button
                    disabled={!currentUser}
                    content="Like"
                    color="blue"
                    onClick={() => handleClickLike(animal.id)}
                  />
                  <Button
                    disabled={!currentUser}
                    content="Pass"
                    color="red"
                    onClick={() => handleClickPass(animal.id)}
                  />
                </div>
              </Grid.Column>
            ))}
        </Grid>
      </>
    </div>
  );
};

export default HomePage;
