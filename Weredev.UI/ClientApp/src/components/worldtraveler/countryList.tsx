import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ICountry } from 'src/models/worldTraveler';
import WorldTravelerStore from 'src/services/flux/stores/worldTravelerStore';
import CountryLink from './countryLink';

class CountryList extends React.Component<RouteComponentProps, any> {

  public render() {
    let sortedCollections: ICountry[] = [];
    if (WorldTravelerStore.getCountries()) {
      sortedCollections = WorldTravelerStore.getCountries().sort((n1, n2) => {
        if (n1.name > n2.name) { return 1; }
        if (n1.name < n2.name) { return -1; }
        return 0;
      })
    }

    const collectionTags: any[] = [];
    sortedCollections.forEach(element => {
      collectionTags.push(<CountryLink {...this.props} country={element} key={element.key} />)
    });

    return (
      <div>
        <h2>world traveler</h2>
        <p>
          I love to travel and along the way I've become a bit of a photographer.  I'm not particularly good though
          there are some pictures I'm proud of.  They're hosted at Flickr, but I'm making them available here too.  Enjoy!
        </p>
        <p>
          Note: This site is a work in progress so not everything is clickable yet.
        </p>
        <div className="row">{collectionTags}</div>
        <p className="mt-4">
          Country maps courtesy of <a href='https://freevectormaps.com/' target='_blank'>Free Vector Maps</a>.
        </p>
      </div>
    );
  }
}

export default CountryList;