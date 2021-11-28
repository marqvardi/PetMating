import "./App.css";
import LoginForm from "./Components/Login/LoginForm.component";
import HomePage from "./Pages/Home/HomePage";
import { Router, Route, Switch } from "react-router-dom";
import history from "./util/history";
import HeaderComponent from "./Components/Header/Header.component";
import MyPetsPage from "./Pages/MyPets/myPetsPage";
import { Container } from "semantic-ui-react";
import TestePage from "./Pages/Home/TestePage";
import RegisterPage from "./Pages/Register/registerPage";

const App = () => {
  // const currentUser = useSelector(getCurrentUser);

  return (
    <div>
      <Router history={history}>
        <HeaderComponent />
        <Container>
          <Switch>
            <Route path="/" exact component={HomePage} />
            <Route path="/Teste" exact component={TestePage} />
            <Route path="/home" component={HomePage} />
            <Route path="/login" component={LoginForm} />
            <Route path="/pets" component={MyPetsPage} />
            <Route path="/register" component={RegisterPage} />
          </Switch>
        </Container>
      </Router>

      {/* {!currentUser && <LoginForm />} */}
    </div>
  );
};

export default App;
