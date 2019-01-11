import * as React from 'react';
import { NavLink } from 'react-router-dom';
import { ISiteNavProps } from 'src/models';
import { ICountry } from 'src/models/worldTraveler';

class CountryLink extends React.Component<ICountryLinkProps, any, any> {
  
  
  public render() {
    const imageUrl = '/images/countries/' + this.props.country.name.toLowerCase() + '.png';
    let path : string = "";
    if (this.props.siteNav) {
      path = this.props.siteNav.path;
    }
    return (
      <div className="col-lg-3 col-md-4 col-sm-6 col-12 mt-3">
        <NavLink to={path}>
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

export interface ICountryLinkProps {
  country: ICountry;
  siteNav: ISiteNavProps | undefined
}
