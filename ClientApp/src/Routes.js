import React from 'react';
import { isAuthenticated } from "./components/auth";
import { Home } from './components/Home';
import Login from './components/Login';
import { TitulosDataGrid } from './components/TitulosDataGrid';
import { TitulosDataGridDX } from './components/TitulosDataGridDX';
import { Route, Switch, Redirect } from 'react-router-dom';


const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route
      {...rest}
      render={props =>
        isAuthenticated() ? (
          <Component {...props} />
        ) : (
          <Redirect to={{ pathname: "/Login", state: { from: props.location } }} />
        )
      }
    />
  );

const Routes = () => (
    <Switch>
        <Route exact path='/' component={Home} />
        <Route exact path='/Login' component={Login} />
        <PrivateRoute path='/titulos-datagrid' component={TitulosDataGrid} />
        <Route path='/titulos-datagriddx' component={TitulosDataGridDX} />
    </Switch>
);

export default Routes;