import React, { useState } from "react";
import { Menu } from "semantic-ui-react";
import { getCurrentUser } from "../../Redux/UserReducer/user.selector";
import { useSelector, useDispatch } from "react-redux";
import { Link } from "react-router-dom";
import { SignOut } from "../../API/User/UserApi";
import classes from "./header.module.scss";

const HeaderComponent = () => {
  const currentUser = useSelector(getCurrentUser);
  const [activeItem, setActiveItem] = useState("home");
  const dispatch = useDispatch();

  const handleItemClick = (e, { name }) => setActiveItem(name);

  const handleSignOut = () => {
    localStorage.removeItem("tokenPetMating");
    dispatch(SignOut());
  };

  return (
    <div>
      <Menu pointing color="olive" inverted className={classes.menu}>
        <Menu.Item
          as={Link}
          name="home"
          to="/home"
          active={activeItem === "home"}
          onClick={handleItemClick}
        />

        {!currentUser && (
          <Menu.Item
            name="Login"
            as={Link}
            to="/Login"
            active={activeItem === "Login"}
            onClick={handleItemClick}
          />
        )}

        {currentUser && (
          <Menu.Item
            name="My pets"
            as={Link}
            to="/pets"
            active={activeItem === "My pets"}
            onClick={handleItemClick}
          />
        )}

        {/* <Menu.Item
          name="Teste"
          as={Link}
          to="/Teste"
          active={activeItem === "Teste"}
          onClick={handleItemClick}
        /> */}

        <Menu.Menu position="right">
          {currentUser && (
            <Menu.Item
              name="SignOut"
              as={Link}
              to="/"
              onClick={handleSignOut}
            />
          )}
        </Menu.Menu>
      </Menu>
    </div>
  );
};

export default HeaderComponent;
