import "bootstrap/dist/css/bootstrap.css";
import "./App.css";
import React from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import App from "./App";
import { unregister } from "./registerServiceWorker";
import { TournamentsProvider } from "./Tournaments.context";
import history from "./History";
import { AppInsights } from "applicationinsights-js";

/* Call downloadAndSetup to download full ApplicationInsights script from CDN and initialize it with instrumentation key */
AppInsights.downloadAndSetup({
  instrumentationKey: "c4a32646-2dff-4e1c-befe-08d061110d60"
});
AppInsights.trackPageView(window.location.pathname);

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

const unlisten = history.listen((location, action) => {
  AppInsights.trackPageView(window.location.pathname);
});

ReactDOM.render(
  <Router basename={baseUrl} history={history}>
    <TournamentsProvider>
      <App />
    </TournamentsProvider>
  </Router>,
  rootElement
);

unregister();
