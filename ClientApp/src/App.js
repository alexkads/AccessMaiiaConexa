import React, { Component } from 'react';
import { Layout } from './components/Layout';
import Routes from './Routes';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Routes />
      </Layout>
    );
  }
}
