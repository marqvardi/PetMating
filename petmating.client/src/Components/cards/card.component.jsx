import React from "react";
import { Card, Icon, Image, List, Loader, Dimmer } from "semantic-ui-react";

const CardComponent = ({ loading, animal }) => {
  return (
    <div>
      {loading ? (
        <Dimmer active>
          <Loader content="Loading" />
        </Dimmer>
      ) : (
        <Card raised centered>
          <Image
            src={process.env.PUBLIC_URL + "/images/pets.jpg"}
            wrapped
            ui={false}
          />
          <Card.Content>
            <Card.Header>{animal.firstName}</Card.Header>
            <Card.Meta>
              <span className="date" style={{ color: "red" }}>
                {animal.user.address.city}
              </span>
            </Card.Meta>
            <List>
              <List.Item>
                <List.Header>Age</List.Header>
                pets age
              </List.Item>
              <List.Item>
                <List.Header>Pedrigree</List.Header>
                Pets Pedrigree
              </List.Item>
              <List.Item>
                <List.Header>Colour</List.Header>
                Pets colour
              </List.Item>
              <List.Item>
                <List.Header>Type of hair </List.Header>
                Pets type of hair
              </List.Item>
              <List.Item>
                <List.Header>Eyes Colour</List.Header>
                Pets eyes colour
              </List.Item>
            </List>
          </Card.Content>
          <Card.Content extra>
            <i>
              <Icon name="paw" size="big" />
              Pets Breed
            </i>
          </Card.Content>
        </Card>
      )}
    </div>
  );
};

export default CardComponent;
