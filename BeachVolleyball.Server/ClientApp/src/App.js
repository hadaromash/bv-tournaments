import React from "react";
import { Route, Switch, Redirect } from "react-router";
import { Layout } from "./components/Layout";
import Tournaments from "./components/Tournaments/Tournaments";
import Home from "./components/Home/Home";

const App = () => {
  return (
    <Layout>
      <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/tournaments/:id/:categoryId" component={Tournaments} />
        <Redirect to="/" />
      </Switch>
    </Layout>
  );
};

export default App;
