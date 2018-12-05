import * as React from 'react';
import {
  HashRouter,
  Route
  } from 'react-router-dom';
import './App.scss';
import * as Developer from './developer/index';
import * as Geek from './geek/index';
import * as Home from './home/index';
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
              <Route path="//" component={Home.default}/>
              <Route path="/geek/" component={Geek.default}/>
              <Route path="/developer/" component={Developer.default}/>
              <Route path="/traveler/" component={Traveler.default}/>
            </div>
          </HashRouter>
        </div>
      </div>
    );
  }
}

export default App;
