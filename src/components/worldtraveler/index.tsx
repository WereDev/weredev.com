import * as React from 'react';
import { ICountry } from 'src/models/worldTraveler';
import WorldTravelerActions from '../../services/flux/actions/worldTravelerActions';
import { StoreEvents } from '../../services/flux/stores/storeEvents';
import WorldTravelerStore from '../../services/flux/stores/worldTravelerStore';
import RoutesHelper from '../routesHelper';
import CountryLink from './countryLink';

class Index extends React.Component<any, IndexState, any> {

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
    let sortedCollections : ICountry[] = [];
    if (this.state.countries) {
      sortedCollections = this.state.countries.sort((n1, n2) => {
        if (n1.name > n2.name) { return 1; }
        if (n1.name < n2.name) { return -1; }
        return 0;
      })
    }

    const collectionTags : any[] = [];
    sortedCollections.forEach(element => {
      const siteNav = RoutesHelper.getCountryNavProps(this.props.location.pathname).find((route) => route.name === element.name);
      collectionTags.push(<CountryLink country={element} siteNav={siteNav} key={element.key} />)
    });

    return (
      <div className="text-center pt-4">
        <h2>world traveler</h2>
        <p>
          I love to travel and along the way I've become a bit of a photographer.  I'm not particularly good though
          there are some pictures I'm proud of.  They're hosted at Flickr, but I'm making them available here too.  Enjoy!
        </p>
        <p>
          Note: This site is a work in progress so not everything is clickable yet.
        </p>
        <div className="row">{ collectionTags }</div>
        <p className="mt-4">
          Country maps courtesy of <a href='https://freevectormaps.com/' target='_blank'>Free Vector Maps</a>.
        </p>
      </div>
    );
  }
}

export default Index;

interface IndexState {
  countries: ICountry[];
}