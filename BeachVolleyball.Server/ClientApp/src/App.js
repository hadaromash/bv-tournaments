import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Tournaments from './components/Tournaments/Tournaments';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/tournaments/:id' component={Tournaments} />
      </Layout>
    );
  }
}

const Home = () => (<div/>)
