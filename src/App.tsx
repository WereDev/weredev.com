import * as React from 'react';
import {
  HashRouter,
  NavLink,
  Route
  } from 'react-router-dom';
import './App.css';
import * as Developer from './developer/index';
import * as Geek from './geek/index';
import * as Traveler from './worldtraveler/index';

class App extends React.Component {
  public render() {
    return (
      <div className="app">
        <header className="app-header">
            <div className="header-image">
                <img src="./images/header.jpg" style={{ "width": "100%" }} />
            </div>
            <h1 className="app-title">weredev</h1>
        </header>
        <div className="app-body">
          <HashRouter>
            <div>
              <div className="app-body-col">
                <h1 className="col-title">geek</h1>
                <p>
                  I play games.  I roll dice.<br />
                  And sometimes create things to help.
                </p>
                <p>
                  <NavLink to="/geek">Would you like to know more?</NavLink>
                </p>
              </div>
              <div className="app-body-col">
                <h1 className="col-title">developer</h1>
                <p>
                  I write software for paychecks.<br />
                  And sometimes because I want stuff.
                </p>
                <p>
                  <NavLink to="/developer">Would you like to know more?</NavLink>
                </p>
              </div>
              <div className="app-body-col">
                <h1 className="col-title">world traveler</h1>
                <p>
                  Work to live, don't live to work.<br />
                  And make sure there's pictures.
                </p>
                <p>
                  <NavLink to="/traveler">Would you like to see more?</NavLink>
                </p>
              </div>
              <Route path="/geek" component={Geek.default}/>
              <Route path="/developer" component={Developer.default}/>
              <Route path="/traveler" component={Traveler.default}/>
            </div>
          </HashRouter>
        </div>
      </div>
    );
  }
}

export default App;
