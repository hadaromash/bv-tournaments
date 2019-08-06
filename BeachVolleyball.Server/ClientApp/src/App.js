import React from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import Tournaments from "./components/Tournaments/Tournaments";
import Home from "./components/Home";

const App = () => {
  return (
    <Layout>
      <Route exact path="/" component={Home} />
      <Route path="/tournaments/:id" component={Tournaments} />
    </Layout>
  );
};

export default App;