import * as React from 'react';
import { Route } from 'react-router-dom';
import { ICountry } from 'src/models/worldTraveler';
import WorldTravelerActions from 'src/services/flux/actions/worldTravelerActions';
import { StoreEvents } from 'src/services/flux/stores/storeEvents';
import WorldTravelerStore from 'src/services/flux/stores/worldTravelerStore';
import Cities from './cities';
import CountryList from './countryList';

class Index extends React.Component<any, IndexState> {

  constructor(props: any) {
    super(props);
    this.state = {
      countries: []
    };
  }

  public componentWillMount() {
    this.updateCountries = this.updateCountries.bind(this);
    WorldTravelerStore.addListener(StoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public componentDidMount() {
    if (WorldTravelerStore.getCountries().length === 0)
    {
      WorldTravelerActions.loadCountries();
    } else {
      this.updateCountries();
    }
  }

  public componentWillUnmount() {
    WorldTravelerStore.removeListener(StoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public updateCountries() {
    const storeCountries = WorldTravelerStore.getCountries();
    this.setState({
      countries: storeCountries
    });
  }

  public render() {
    let cityListRoute : any;
    if (this.state.countries && this.state.countries.length > 0) {
      cityListRoute = <Route path='/traveler/:countryName' exact={true} component={Cities} />
    }
    return (
      <div className="text-center pt-4">
        <Route path='/traveler' exact={true} component={CountryList}  />
        { cityListRoute }
      </div>
    );
  }
}

export default Index;

interface IndexState {
  countries: ICountry[];
}