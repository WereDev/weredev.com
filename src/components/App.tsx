import * as React from 'react';
import { BrowserRouter, NavLink, Route } from 'react-router-dom';
import { ISiteNavProps } from '../models';
import './App.scss';
import Developer from './developer';
import Geek from './geek';
import Home from './home';
import Traveler from './worldtraveler';

class App extends React.Component<any, IAppState> {
  public render() {
    return (
      <div className="app">
        <BrowserRouter>
          <div>
            <header className="app-header text-center">
                <div className="header-image">
                    <img src="/images/header.jpg" style={{ "width": "100%" }} />
                </div>
                <h1 className="app-title">
                  <NavLink to="/">weredev</NavLink> 
                </h1>
            </header>
            <div className="app-body">
              <div className="container">
                <Route path='/' component={Home} exact={true} />
                <Route path='/geek' component={Geek} />
                <Route path='/developer' component={Developer} />
                <Route path='/traveler' component={Traveler} />
              </div>
            </div>
          </div>
        </BrowserRouter>
      </div>
    );
  }
}

interface IAppState {
  siteNavProps: ISiteNavProps[],
  traveler_countryNavProps: ISiteNavProps[]
}

export default App;
