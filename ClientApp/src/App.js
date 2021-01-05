import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { TitulosDataGrid } from './components/TitulosDataGrid';
import { TitulosDataGridDX } from './components/TitulosDataGridDX';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/titulos-datagrid' component={TitulosDataGrid} />
        <Route path='/titulos-datagriddx' component={TitulosDataGridDX} />
      </Layout>
    );
  }
}
