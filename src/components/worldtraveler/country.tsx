import * as React from 'react';
import { ICountry } from 'src/models/worldTraveler';

class Country extends React.Component<ICountryProps, any, any> {
  
  public render() {
    return (
      <div className="col-lg-3 col-md-4 col-sm-6 col-12 mt-3">
        {/* <img src={this.props.country.} /> */}
        <h3>{this.props.country.name}</h3>        
      </div>
    );
  }
}

export default Country;

export interface ICountryProps {
    country: ICountry
}