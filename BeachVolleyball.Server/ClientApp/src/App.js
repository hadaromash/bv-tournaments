import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Tournaments from './components/Tournaments/AllTournaments';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Tournaments} />
      </Layout>
    );
  }
}
