import logo from './logo.svg';
import React, { Component } from 'react';
import Agendamento from './componentes/agendamento/agendamento';
import Erro404 from './componentes/erro404/erro404';
import { HashRouter, Switch, Route } from 'react-router-dom';
import './App.css';
import { ToastContainer } from 'react-toastify';


class App extends Component {
  render() {
    return (
      <div>
        <HashRouter>
          <div>
            <Switch>
              <Route exact={true} path='/' component={Agendamento} />
              <Route status="404" path="*" component={Erro404} />
              {/* <Route status="403" path='/sem-permissao' component={Erro403} />
              <Route status="500" path='/erro-interno' component={Erro500} />
              <Route status="404" path="*" component={Erro404} /> */}
            </Switch>
          </div>
        </HashRouter>
        <ToastContainer autoClose={5000} style={{ zIndex: 9999 }} />
      </div>
    )
  }
}

export default App;
