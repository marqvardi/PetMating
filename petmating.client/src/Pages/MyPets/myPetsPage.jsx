import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ClearAllAnimals, GetAllAnimals } from "../../API/Animals/AnimalApi";
import CardComponent from "../../Components/cards/card.component";
import { getAllAnimals } from "../../Redux/AnimalReducer/animal.selector";
import { Grid, Image } from "semantic-ui-react";

const MyPetsPage = () => {
  const dispatch = useDispatch();
  const AllAnimals = useSelector(getAllAnimals);

  useEffect(() => {
    dispatch(GetAllAnimals());
    return () => {
      dispatch(ClearAllAnimals());
    };
  }, [dispatch]);

  return (
    <div>
      {AllAnimals &&
        AllAnimals.map((animal) => (
          <>
            <Grid celled centered stackable>
              <Grid.Column width={4}>
                <Image src="/images/wireframe/image.png" />
              </Grid.Column>
              <Grid.Column width={9}>
                <p>{animal.firstName}</p>
              </Grid.Column>
              <Grid.Column width={3}>
                <h2>{animal.user.firstName}</h2>
                <h2>{animal.user.lastName}</h2>
                <h2>{animal.user.email}</h2>
              </Grid.Column>
            </Grid>
          </>
        ))}
    </div>
  );
};

export default MyPetsPage;
