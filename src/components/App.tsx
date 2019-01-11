import * as React from 'react';
import { BrowserRouter, NavLink, Route } from 'react-router-dom';
import { StoreEvents } from 'src/services/flux/stores/storeEvents';
import { ISiteNavProps } from '../models';
import WorldTravelerStore from '../services/flux/stores/worldTravelerStore';
import './App.scss';
import Developer from './developer';
import Geek from './geek';
import Home from './home';
import RoutesHelper from './routesHelper';
import Traveler from './worldtraveler';

class App extends React.Component<any, IAppState, any> {

  constructor(props : any){
    super(props);
    this.state = {
      siteNavProps: [
        { component: Home, name: "Home", parentPath: null, path: '//' },
        { component: Geek, name: "Geek", parentPath: null, path: '/geek/' },
        { component: Developer, name: "Developer", parentPath: null, path: '/developer/' },
        { component: Traveler, name: "World Traveler", parentPath: null, path: '/traveler/' }
      ],
      traveler_countryNavProps: []
    }
  }

  public componentWillMount() {
    this.updateCountries = this.updateCountries.bind(this);
    WorldTravelerStore.addListener(StoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public componentWillUnmount() {
    WorldTravelerStore.removeListener(StoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public updateCountries() {
    const countryRoutes = RoutesHelper.getCountryNavProps('/traveler/');
    this.setState({
      traveler_countryNavProps: countryRoutes
    });
  }

  public render() {
    const routes : any[] = [];
    let siteNavs : ISiteNavProps[] = [];
    siteNavs = siteNavs.concat(this.state.siteNavProps);
    if (this.state.traveler_countryNavProps) {
      siteNavs = siteNavs.concat(this.state.traveler_countryNavProps);
    }
    
    siteNavs.forEach(routePath => {
      routes.push(<Route exact={true} path={routePath.path} key={routePath.path} component={routePath.component} />);
    });

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
                { routes }
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
