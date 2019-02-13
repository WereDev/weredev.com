import * as React from 'react';
import { NavLink, RouteComponentProps } from 'react-router-dom';
import { ICountry } from 'src/models/worldTraveler';

class CountryLink extends React.Component<ICountryLinkProps, any, any> {
  
  
  public render() {
    const imageUrl = '/images/countries/' + this.props.country.name.toLowerCase() + '.png';
    const countryUrl = this.props.match.path + '/' + this.props.country.name;
    return (
      <div className="col-lg-3 col-md-4 col-sm-6 col-12 mt-3">
        <NavLink to={countryUrl}>
          <div>
            <img src={imageUrl} className='border border-dark' />
            <h3 className='mt-1'>{this.props.country.name}</h3>
          </div>
        </NavLink>
      </div>
    );
  }
}

export default CountryLink;

export interface ICountryLinkProps extends RouteComponentProps {
  country: ICountry;
}
