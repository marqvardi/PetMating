import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import store from "./Redux/store";
import { Provider } from "react-redux";
import { ToastContainer } from "react-toastify";
import "semantic-ui-css/semantic.min.css";
import "react-toastify/dist/ReactToastify.css";

ReactDOM.render(
  <BrowserRouter>
    <Provider store={store}>
      <ToastContainer position="top-right" theme="colored" />
      <App />
    </Provider>
  </BrowserRouter>,
  document.getElementById("root")
);
