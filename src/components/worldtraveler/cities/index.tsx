import * as React from 'react';
import { Route, RouteComponentProps } from 'react-router-dom';
import { ICountry } from 'src/models/worldTraveler';
import CityList from './cityList';


class Index extends React.Component<IndexProps, any> {
    public render() {
        return (
            <div>
                <Route path='/traveler/:countryName' exact={true} component={CityList} />
            </div>
        );
    }
}

export default Index;

interface IndexProps extends RouteComponentProps {
    countries: ICountry[];
}