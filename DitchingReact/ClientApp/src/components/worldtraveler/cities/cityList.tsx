import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { NavLink } from 'react-router-dom';
import { ICountry, IUrlParams } from 'src/models/worldTraveler';
import WorldTravelerStore from 'src/services/flux/stores/worldTravelerStore';
// import CityLink from './cityLink';
import './cityList.scss';

class CityList extends React.Component<ICityList, any, any> {

  private country: ICountry | undefined;

  constructor(props: ICityList) {
    super(props);
  }

  public componentWillMount() {
    this.country = WorldTravelerStore.getCountries().find((c) => c.name.toLowerCase() === this.props.match.params.countryName.toLowerCase());
  }

  public render() {
    if (this.country) {

      const cities: any[] = [];
      // this.country.cities.forEach((city) => {
      //   cities.push(<CityLink {...this.props} city={city} />)
      // })

      let parentPath = this.props.match.path;
      parentPath = parentPath.substring(0, parentPath.lastIndexOf('/'));

      return (
        <div>
          <NavLink to={parentPath}>
            <h2>world traveler</h2>
          </NavLink>
          <h3>{this.country.name}</h3>
          <table className="city-list-table">
            {cities}
          </table>
        </div>
      );
    } else {
      return (
        <div>Loading...</div>
      )
    }
  }
}

export default CityList;

interface ICityList extends RouteComponentProps<IUrlParams> {
  country: ICountry | undefined;
}