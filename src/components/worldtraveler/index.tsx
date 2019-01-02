import * as React from 'react';
import { ICountry } from 'src/models/worldTraveler';
import WorldTravelerActions from '../../services/flux/actions/worldTravelerActions';
import WorldTravelerStore from '../../services/flux/stores/worldTravelerStore';
import { WorldTravelerStoreEvents } from '../../services/flux/stores/worldTravelerStoreEvents';
import Country from './country';

class Index extends React.Component<any, IndexState, any> {

  public componentWillMount() {
    this.setState({
      countries: []
    });

    this.updateCountries = this.updateCountries.bind(this);
    WorldTravelerStore.addListener(WorldTravelerStoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public componentDidMount() {
    WorldTravelerActions.loadCountries();
  }

  public componentWillUnmount() {
    WorldTravelerStore.removeListener(WorldTravelerStoreEvents.COUNTRIES_CHANGED, this.updateCountries);
  }

  public updateCountries() {
    this.setState({
      countries: WorldTravelerStore.getCountries()
    })
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
        collectionTags.push(<Country country={element} />)
    });

    return (
      <div className="text-center pt-4">
        <h2>world traveler</h2>
        <p>
          I love to travel and along the way I've become a bit of a photographer.  I'm not particularly good<br />
          though there are some pictures I'm proud of.  They're hosted at Flickr, but I'm making them available<br />
          here too.  Enjoy!
        </p>
        <p>
          Note: This site is a work in progress so not everything is clickable yet.
        </p>
        <div className="row">{ collectionTags }</div>
      </div>
    );
  }
}

export default Index;

interface IndexState {
  countries: ICountry[];
}