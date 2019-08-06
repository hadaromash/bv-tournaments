import "bootstrap/dist/css/bootstrap.css";
import "./App.css";
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import { unregister } from "./registerServiceWorker";
import { TournamentsProvider } from "./Tournaments.context";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <TournamentsProvider>
      <App />
    </TournamentsProvider>
  </BrowserRouter>,
  rootElement
);

unregister();
