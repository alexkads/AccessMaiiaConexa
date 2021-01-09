import React from 'react';

import { Home } from './components/Home';
import Login from './components/Login';
import { TitulosDataGrid } from './components/TitulosDataGrid';
import { TitulosDataGridDX } from './components/TitulosDataGridDX';
import { Route, Switch } from 'react-router-dom';
import PrivateRoute from './PrivateRoute';

const Routes = () => (
    <Switch>
        <PrivateRoute exact path='/' component={Home} />
        <Route exact path='/Login' component={Login} />
        <PrivateRoute path='/titulos-datagrid' component={TitulosDataGrid} />
        <PrivateRoute path='/titulos-datagriddx' component={TitulosDataGridDX} />
    </Switch>
);

export default Routes;