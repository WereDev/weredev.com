import * as React from 'react';
import {
  HashRouter,
  NavLink,
  Route
  } from 'react-router-dom';
import './App.scss';
import Developer from './developer/index';
import Geek from './geek/index';
import Home from './home/index';
import Traveler from './worldtraveler/index';

class App extends React.Component {
  public render() {
    return (
      <div className="app">
        <HashRouter>
          <div>
            <header className="app-header text-center">
                <div className="header-image">
                    <img src="./images/header.jpg" style={{ "width": "100%" }} />
                </div>
                <h1 className="app-title">
                  <NavLink to="/">weredev</NavLink> 
                </h1>
            </header>
            <div className="app-body">
              
                <div className="container">
                  <Route path="//" component={Home}/>
                  <Route path="/geek/" component={Geek}/>
                  <Route path="/developer/" component={Developer}/>
                  <Route path="/traveler/" component={Traveler}/>
                </div>
            </div>
          </div>
        </HashRouter>
      </div>
    );
  }
}

export default App;
